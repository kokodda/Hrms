using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HrmsModel.Models;
using HrmsModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace HrmsApp.Controllers
{
    public class RecruitmentController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly ILookupServices _lookup;

        public RecruitmentController(HrmsDbContext context,
                                    IHostingEnvironment environment,
                                    ILookupServices lookup)
        {
            _context = context;
            _environment = environment;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            ViewBag.orgUnitTypesList = _lookup.GetLookupItems<OrgUnitType>();
            return View();
        }

        public async Task<IActionResult> OrgUnitsList()
        {
            var model = _context.OrgUnits.Include(b => b.OrgUnitType).Where(b => b.IsActive).OrderBy(b => b.OrgUnitType.SortOrder).ThenBy(b => b.SortOrder);
            return PartialView("_OrgUnitsList", await model.ToListAsync());
        }

        public async Task<IActionResult> HeadsList()
        {
            var model = _context.OrgUnits.Include(b => b.JobGrade).Include(b => b.SalaryStep).Include(b => b.StandardTitleType).Include(b => b.OrgUnitType)
                                        .Where(b => b.IsVacant && b.IsActive)
                                        .OrderBy(b => b.OrgUnitType.SortOrder).ThenBy(b => b.SortOrder);
            return PartialView("_HeadsList", await model.ToListAsync());
        }

        public async Task<IActionResult> PositionsList()
        {
            var model = _context.Positions.Include(b => b.JobGrade).Include(b => b.SalaryStep).Include(b => b.StandardTitleType)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.OrgUnitType).Include(b => b.Employments)
                                        .Where(b => b.TotalVacant != 0 && b.OrgUnit.IsActive && b.IsActive)
                                        .OrderBy(b => b.OrgUnit.OrgUnitType.SortOrder).ThenBy(b => b.OrgUnit.SortOrder);
            return PartialView("_PositionsList", await model.ToListAsync());
        }

        public async Task<IActionResult> CandidatesList()
        {
            var model = _context.Candidates.Include(b => b.OrgUnit).ThenInclude(b => b.OrgUnitType).OrderBy(b => b.OrgUnit.OrgUnitType.SortOrder).ThenBy(b => b.OrgUnit.SortOrder);
            return PartialView("_CandidatesList", await model.ToListAsync());
        }

        //new employee
        public async Task<IActionResult> AddEmployee(long buId, long? posId, bool isHead = false)
        {
            var x = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == buId);
            ViewBag.orgUnitName = x.Name;
            ViewBag.isUnassigned = false;
            if (posId.HasValue && !isHead)
                ViewBag.positionName = _context.Positions.SingleOrDefault(b => b.Id == posId).Name;
            else if (!posId.HasValue && isHead)
                ViewBag.positionName = x.HeadPositionName;
            else
            {
                ViewBag.positionName = "Employment with Unassigned Position";
                ViewBag.isUnassigned = true;
            }
            ViewBag.positionId = posId;
            ViewBag.isHead = isHead.ToString();
            ViewBag.orgUnitId = buId;
            ViewBag.nationalitiesList = await _context.Nationalities.Where(b => b.IsActive).ToListAsync();
            ViewBag.governoratesList = await _context.Governorates.Where(b => b.IsActive).ToListAsync();
            ViewBag.employmentTypesList = _lookup.GetLookupItems<EmploymentType>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(Employee item, long orgUnitId, int employmentTypeId, string jobName, string othJobName, DateTime? dateInPosition, long? positionId, string isHead = "false", bool isProbation = false)
        {
            item.IsActive = true;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.Employees.Add(item);

            var ou = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == orgUnitId);
            
            Employment pos = new Employment();
            pos.OrgUnitId = orgUnitId;
            pos.EmployeeId = item.Id;
            pos.IsHead = isHead == "false" ? false : true;
            pos.PositionId = positionId;
            if(positionId.HasValue)
            {
                var p = await _context.Positions.SingleOrDefaultAsync(b => b.Id == positionId);
                pos.IsAttendRequired = p.IsAttendRequired;
                pos.IsOverTimeAllowed = p.IsOverTimeAllowed;
            }
            pos.EmploymentTypeId = employmentTypeId;
            pos.JobName = jobName;
            pos.OthJobName = othJobName;
            pos.JobGradeId = ou.JobGradeId;
            pos.FromDate = dateInPosition.HasValue ? dateInPosition.Value : DateTime.Now.Date;
            pos.IsActive = true;
            pos.IsProbation = isProbation;
            _context.Employments.Add(pos);

            EmployeePromotion prom = new EmployeePromotion();
            prom.EmploymentId = pos.Id;
            prom.PromotionTypeId = _lookup.GetLookupItems<PromotionType>().SingleOrDefault(b => b.SysCode == "INIT").Id;
            prom.JobGradeId = ou.JobGradeId;
            prom.CreatedDate = DateTime.Now.Date;
            prom.EffectiveFromDate = dateInPosition.HasValue ? dateInPosition.Value : DateTime.Now.Date;
            prom.IsApproved = true;
            prom.LastUpdated = DateTime.Now.Date;
            prom.UpdatedBy = "user";
            _context.EmployeePromotions.Add(prom);


            //add leave entitlements
            int annualTypeId = _lookup.GetLookupItems<LeaveType>().SingleOrDefault(b => b.SysCode == "ANNUAL").Id;
            int sickTypeId = _lookup.GetLookupItems<LeaveType>().SingleOrDefault(b => b.SysCode == "SICK").Id;
            int entitle = 0;
            if (ou.JobGradeId.HasValue)
            {
                int grpId = _context.JobGrades.SingleOrDefault(b => b.Id == ou.JobGradeId).Id;
                var lv = _context.LeavePolicies.SingleOrDefault(b => b.LeaveTypeId == annualTypeId && (b.JobGradeId == ou.JobGradeId || b.GradeGroupId == grpId));
                if (lv != null)
                    entitle = lv.TotalDays;
            }
            _context.EmployeeLeaveBalances.Add(new EmployeeLeaveBalance { EmployeeId = item.Id, LeaveTypeId = annualTypeId, AnnualEntitlement = entitle });
            entitle = 0;
            if (ou.JobGradeId.HasValue)
            {
                int grpId = _context.JobGrades.SingleOrDefault(b => b.Id == ou.JobGradeId).Id;
                var lv = _context.LeavePolicies.SingleOrDefault(b => b.LeaveTypeId == sickTypeId && (b.JobGradeId == ou.JobGradeId || b.GradeGroupId == grpId));
                if (lv != null)
                    entitle = lv.TotalDays;
            }
            _context.EmployeeLeaveBalances.Add(new EmployeeLeaveBalance { EmployeeId = item.Id, LeaveTypeId = sickTypeId, AnnualEntitlement = entitle });

            //update vacancies
            if (isHead == "true")
                ou.IsVacant = false;
            if(positionId.HasValue)
            {
                var p = await _context.Positions.SingleOrDefaultAsync(b => b.Id == positionId);
                if (p.TotalVacant > 0)
                    p.TotalVacant--;
            }
            await _context.SaveChangesAsync();
            return Content("Employee Successfully Added.", "text/html");
        }
    }
}