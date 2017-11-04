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
using Newtonsoft.Json;

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

        public IActionResult Index(string list = "Vacancies")
        {
            ViewBag.list = list;
            ViewBag.orgUnitTypesList = _lookup.GetLookupItems<OrgUnitType>();
            return View();
        }

        public async Task<IActionResult> HeadsList()
        {
            var model = _context.OrgUnits.Include(b => b.JobGrade).Include(b => b.StandardTitleType).Include(b => b.OrgUnitType)
                                        .Where(b => b.IsVacant && b.IsActive)
                                        .OrderBy(b => b.OrgUnitType.SortOrder).ThenBy(b => b.SortOrder);
            return PartialView("_HeadsList", await model.ToListAsync());
        }

        public async Task<IActionResult> PositionsList()
        {
            var model = _context.Positions.Include(b => b.JobGrade).Include(b => b.StandardTitleType)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.OrgUnitType).Include(b => b.Employments)
                                        .Where(b => b.TotalVacant != 0 && b.OrgUnit.IsActive && b.IsActive)
                                        .OrderBy(b => b.OrgUnit.OrgUnitType.SortOrder).ThenBy(b => b.OrgUnit.SortOrder);
            return PartialView("_PositionsList", await model.ToListAsync());
        }

        public IActionResult PositionDetails(long id, bool isHead)
        {
            return ViewComponent("PositionDetails", new { id = id, isHead = isHead });
        }

        public async Task<IActionResult> CandidatesList()
        {
            var model = _context.Candidates.Include(b => b.OrgUnit).ThenInclude(b => b.OrgUnitType).Where(b => !b.IsApproved)
                                .OrderBy(b => b.OrgUnit.OrgUnitType.SortOrder).ThenBy(b => b.OrgUnit.SortOrder);
            return PartialView("_CandidatesList", await model.ToListAsync());
        }

        //void to create employee from AddEmployee or HireCandidate actions
        private async void CreateEmployee(Employee item, long orgUnitId, int employmentTypeId, DateTime? dateInPosition, long? positionId, int? jobGradeId, int? salaryStepId, int? salaryScaleTypeId, int basicSalary = 0, bool isHead = false, bool isProbation = false)
        {
            if (string.IsNullOrEmpty(item.EmpUid))
                item.EmpUid = (int.Parse(_context.Employees.Max(b => b.EmpUid)) + 1).ToString();
            item.IsActive = true;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.Employees.Add(item);

            

            Employment pos = new Employment();
            pos.OrgUnitId = orgUnitId;
            pos.EmployeeId = item.Id;
            pos.IsHead = isHead;
            pos.PositionId = positionId;
            if (positionId.HasValue)
            {
                var p = await _context.Positions.SingleOrDefaultAsync(b => b.Id == positionId);
                pos.IsAttendRequired = p.IsAttendRequired;
                pos.IsOverTimeAllowed = p.IsOverTimeAllowed;
            }
            pos.EmploymentTypeId = employmentTypeId;
            pos.JobGradeId = jobGradeId;
            pos.SalaryStepId = salaryStepId;
            pos.SalaryScaleTypeId = salaryScaleTypeId;
            pos.BasicSalary = basicSalary;
            pos.FromDate = dateInPosition.HasValue ? dateInPosition.Value : DateTime.Now.Date;
            pos.IsActive = true;
            pos.IsProbation = isProbation;
            _context.Employments.Add(pos);

            //add initial hiring as promotion
            EmployeePromotion prom = new EmployeePromotion();
            prom.EmploymentId = pos.Id;
            prom.PromotionTypeId = _lookup.GetLookupItems<PromotionType>().SingleOrDefault(b => b.SysCode == "INIT").Id;
            prom.JobGradeId = jobGradeId;
            prom.SalaryStepId = salaryStepId;
            prom.BasicSalary = basicSalary;
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
            if (jobGradeId.HasValue)
            {
                int grpId = _context.JobGrades.SingleOrDefault(b => b.Id == jobGradeId).Id;
                var lv = _context.LeavePolicies.SingleOrDefault(b => b.LeaveTypeId == annualTypeId && (b.JobGradeId == jobGradeId || b.GradeGroupId == grpId));
                if (lv != null)
                    entitle = lv.TotalDays;
            }
            _context.EmployeeLeaveBalances.Add(new EmployeeLeaveBalance { EmployeeId = item.Id, LeaveTypeId = annualTypeId, AnnualEntitlement = entitle });
            entitle = 0;
            if (jobGradeId.HasValue)
            {
                int grpId = _context.JobGrades.SingleOrDefault(b => b.Id == jobGradeId).Id;
                var lv = _context.LeavePolicies.SingleOrDefault(b => b.LeaveTypeId == sickTypeId && (b.JobGradeId == jobGradeId || b.GradeGroupId == grpId));
                if (lv != null)
                    entitle = lv.TotalDays;
            }
            _context.EmployeeLeaveBalances.Add(new EmployeeLeaveBalance { EmployeeId = item.Id, LeaveTypeId = sickTypeId, AnnualEntitlement = entitle });

            //update vacancies
            if (isHead)
                _context.OrgUnits.SingleOrDefault(b => b.Id == orgUnitId).IsVacant = false;
            else
            {
                var p = await _context.Positions.SingleOrDefaultAsync(b => b.Id == positionId);
                if (p.TotalVacant > 0)
                    p.TotalVacant--;
            }
            await _context.SaveChangesAsync();
        }

        //new employee
        public async Task<IActionResult> AddEmployee(long buId, long? posId, bool isHead = false)
        {
            var ou = await _context.OrgUnits.Include(b => b.JobGrade).SingleOrDefaultAsync(b => b.Id == buId);
            ViewBag.orgUnitName = ou.Name;
            if (isHead)
            {
                ViewBag.positionName = ou.HeadPositionName;
                if(ou.JobGradeId.HasValue)
                {
                    ViewBag.isGraded = true;
                    ViewBag.jobGrade = "Job is Graded as: " + ou.JobGrade.Code;
                    ViewBag.jobGradeId = ou.JobGradeId;
                }
                else
                {
                    ViewBag.isGraded = false;
                    ViewBag.jobGrade = "Job is NOT Graded";
                    ViewBag.jobGradeId = null;
                }
            }
            else
            {
                var pos = await _context.Positions.Include(b => b.JobGrade).SingleOrDefaultAsync(b => b.Id == posId);
                ViewBag.positionName = pos.Name;
                if (pos.JobGradeId.HasValue)
                {
                    ViewBag.isGraded = true;
                    ViewBag.jobGrade = "Job is Graded as: " + pos.JobGrade.Code;
                    ViewBag.jobGradeId = pos.JobGradeId;
                }
                else
                {
                    ViewBag.isGraded = false;
                    ViewBag.jobGrade = "Job is NOT Graded";
                    ViewBag.jobGradeId = null;
                }
            }
            ViewBag.positionId = posId;
            ViewBag.isHead = isHead.ToString();
            ViewBag.orgUnitId = buId;
            ViewBag.nationalitiesList = await _context.Nationalities.Where(b => b.IsActive).ToListAsync();
            ViewBag.governoratesList = await _context.Governorates.Where(b => b.IsActive).ToListAsync();
            ViewBag.employmentTypesList = _lookup.GetLookupItems<EmploymentType>();
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryScaleTypesList = _lookup.GetLookupItems<SalaryScaleType>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(Employee item, long orgUnitId, int employmentTypeId, DateTime? dateInPosition, long? positionId, int? jobGradeId, int? salaryStepId, int? salaryScaleTypeId, int basicSalary = 0, string isHead = "false", bool isProbation = false)
        {
            bool isHead1 = isHead == "false" ? false : true;
            CreateEmployee(item, orgUnitId, employmentTypeId,  dateInPosition, positionId,  jobGradeId,  salaryStepId, salaryScaleTypeId, basicSalary, isHead1, isProbation);
            return Content("Employee Successfully Added.", "text/html");
        }

        //new candidate
        public async Task<IActionResult> AddCandidate(long buId, long? posId, bool isHead = false)
        {
            var x = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == buId);
            ViewBag.orgUnitName = x.Name;
            if (isHead)
                ViewBag.positionName = x.HeadPositionName;
            else
                ViewBag.positionName = _context.Positions.SingleOrDefault(b => b.Id == posId).Name; 
            ViewBag.positionId = posId;
            ViewBag.isHead = isHead;
            ViewBag.orgUnitId = buId;
            ViewBag.nationalitiesList = await _context.Nationalities.Where(b => b.IsActive).ToListAsync();
            ViewBag.governoratesList = await _context.Governorates.Where(b => b.IsActive).ToListAsync();
            ViewBag.employmentTypesList = _lookup.GetLookupItems<EmploymentType>();
            ViewBag.qualificationTypesList = _lookup.GetLookupItems<QualificationType>().Where(b => b.SysCode != "DOMAIN_EXPERIENCE" && b.SysCode != "OTHER_EXPERIENCE");
            ViewBag.experienceTypesList = _lookup.GetLookupItems<QualificationType>().Where(b => b.SysCode == "DOMAIN_EXPERIENCE" || b.SysCode == "OTHER_EXPERIENCE");
            ViewBag.competencyAreaTypesList = _lookup.GetLookupItems<CompetencyAreaType>();
            return View();
        }

        public async Task<IActionResult> EditCandidate(long id)
        {
            var model = await _context.Candidates.Include(b => b.OrgUnit).Include(b => b.Position).SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.nationalitiesList = await _context.Nationalities.Where(b => b.IsActive).ToListAsync();
            ViewBag.governoratesList = await _context.Governorates.Where(b => b.IsActive).ToListAsync();
            ViewBag.employmentTypesList = _lookup.GetLookupItems<EmploymentType>();
            ViewBag.qualificationTypesList = _lookup.GetLookupItems<QualificationType>().Where(b => b.SysCode != "DOMAIN_EXPERIENCE" && b.SysCode != "OTHER_EXPERIENCE");
            ViewBag.experienceTypesList = _lookup.GetLookupItems<QualificationType>().Where(b => b.SysCode == "DOMAIN_EXPERIENCE" || b.SysCode == "OTHER_EXPERIENCE");
            ViewBag.competencyAreaTypesList = _lookup.GetLookupItems<CompetencyAreaType>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCandidate([FromBody] dynamic data)
        {
            string appForm = JsonConvert.SerializeObject(data);
            string submit = data.SubmitType.ToString();
            string msg = "Failed! Unable to Save Candidate Information. Please Correct ALL Error then re-apply.";

            if (data.Id != "0")
            {
                long id = long.Parse(data.Id.ToString());
                var model = await _context.Candidates.SingleOrDefaultAsync(b => b.Id == id);
                await TryUpdateModelAsync(model);
                if (submit == "submit")
                {
                    model.IsSubmitted = true;
                    model.SubmittedDate = DateTime.Now.Date;
                }
                model.AppForm = appForm;
                model.LastUpdated = DateTime.Now.Date;
                model.UpdatedBy = "user";
            }
            else
            {
                Candidate item = new Candidate();
                item.AppForm = appForm;
                item.OrgUnitId = data.OrgUnitId;
                if (data.PositionId != "")
                    item.PositionId = long.Parse(data.PositionId.ToString());
                item.IsHead = data.IsHead;
                item.FirstName = data.FirstName;
                item.FamilyName = data.FamilyName;
                item.FatherName = data.FatherName;
                item.MotherName = data.MotherName;
                item.OthFirstName = data.OthFirstName;
                item.OthFamilyName = data.OthFamilyName;
                item.OthFatherName = data.OthFatherName;
                item.OthMotherName = data.OthMotherName;
                item.BirthDate = data.BirthDate;
                item.IsMale = data.IsMale;
                item.IsMilitaryExempted = data.IsMilitaryExempted;
                item.MaritalStatus = data.MaritalStatus;
                item.NationalityId = data.NationalityId;
                item.GovernorateId = data.GovernorateId;
                item.Phone = data.Phone;
                item.HomePhone1 = data.HomePhone1;
                item.HomePhone2 = data.HomePhone2;
                item.Email = data.Email;
                item.Address = data.Address;
                item.PermenantAddress = data.PermenantAddress;
                item.FinalScore = data.FinalScore;
                if (submit == "submit")
                {
                    item.IsSubmitted = true;
                    item.SubmittedDate = DateTime.Now.Date;
                }
                item.LastUpdated = DateTime.Now.Date;
                item.UpdatedBy = "user";
                _context.Candidates.Add(item);
            }

            await _context.SaveChangesAsync();
            if (submit == "submit")
                msg = "Candidate is Successfully Submitted.";
            else
                msg = "Candidate is Successfully Saved.";
            return Json(msg);
        }

        public async Task<IActionResult> RemoveCandidate(long id)
        {
            var item = await _context.Candidates.SingleOrDefaultAsync(b => b.Id == id);
            _context.Candidates.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("CandidatesList");
        }

        public async Task<IActionResult> HireCandidate(long id)
        {
            var model = await _context.Candidates.SingleOrDefaultAsync(b => b.Id == id);
            var ou = await _context.OrgUnits.Include(b => b.JobGrade).SingleOrDefaultAsync(b => b.Id == model.OrgUnitId);
            bool isHead = model.IsHead;
            
            ViewBag.orgUnitName = ou.Name;
            if (isHead)
            {
                ViewBag.positionName = ou.HeadPositionName;
                if (ou.JobGradeId.HasValue)
                {
                    ViewBag.isGraded = true;
                    ViewBag.jobGrade = "Job is Graded as: " + ou.JobGrade.Code;
                    ViewBag.jobGradeId = ou.JobGradeId;
                }
                else
                {
                    ViewBag.isGraded = false;
                    ViewBag.jobGrade = "Job is NOT Graded";
                    ViewBag.jobGradeId = null;
                }
            }
            else
            {
                var pos = await _context.Positions.Include(b => b.JobGrade).SingleOrDefaultAsync(b => b.Id == model.PositionId);
                ViewBag.positionName = pos.Name;
                if (pos.JobGradeId.HasValue)
                {
                    ViewBag.isGraded = true;
                    ViewBag.jobGrade = "Job is Graded as: " + pos.JobGrade.Code;
                    ViewBag.jobGradeId = pos.JobGradeId;
                }
                else
                {
                    ViewBag.isGraded = false;
                    ViewBag.jobGrade = "Job is NOT Graded";
                    ViewBag.jobGradeId = null;
                }
            }
            ViewBag.employmentTypesList = _lookup.GetLookupItems<EmploymentType>();
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryScaleTypesList = _lookup.GetLookupItems<SalaryScaleType>();
            return PartialView("_HireCandidate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HireCandidate(Candidate item, int employmentTypeId, int? jobGradeId, int? salaryStepId, int? salaryScaleTypeId, int basicSalary = 0, string isHead = "false", bool isProbation = false)
        {
            var model = await _context.Candidates.SingleOrDefaultAsync(b => b.Id == item.Id);
            Employee emp = new Employee();
            emp.FirstName = model.FirstName;
            emp.FatherName = model.FatherName;
            emp.FamilyName = model.FamilyName;
            emp.MotherName = model.MotherName;
            emp.OthFirstName = model.OthFirstName;
            emp.OthFatherName = model.OthFatherName;
            emp.OthFamilyName = model.OthFamilyName;
            emp.OthMotherName = model.OthMotherName;
            emp.IsMale = model.IsMale;
            emp.MaritalStatus = model.MaritalStatus;
            emp.IsMilitaryExempted = model.IsMilitaryExempted;
            emp.BirthDate = model.BirthDate;
            emp.NationalityId = model.NationalityId;
            emp.Phone = model.Phone;
            emp.HomePhone1 = model.HomePhone1;
            emp.HomePhone2 = model.HomePhone2;
            emp.GovernorateId = model.GovernorateId;
            emp.Address = model.Address;
            emp.PermenantAddress = model.PermenantAddress;
            emp.Email = model.Email;
            emp.JoinDate = DateTime.Now.Date;
            emp.AppForm = model.AppForm;

            //CreateEmployee(emp, model.OrgUnitId, employmentTypeId, null, model.PositionId, jobGradeId, salaryStepId, salaryScaleTypeId, basicSalary, model.IsHead, isProbation);

            //add qualifications
            dynamic json = JsonConvert.DeserializeObject(model.AppForm);
            foreach(var x in json.QualificationsList)
            {
                if (!string.IsNullOrEmpty(x.type.Value))
                {
                    EmployeeQualification eq = new EmployeeQualification();
                    eq.EmployeeId = emp.Id;
                    eq.QualificationTypeId = int.Parse(x.type.ToString());
                    eq.CompetencyAreaTypeId = int.Parse(x.competency.ToString());
                    eq.ObtainedDate = DateTime.Parse(x.obtainedDate.ToString());
                    eq.Name = x.name.ToString();
                    _context.EmployeeQualifications.Add(eq);
                }
            }
            int othCompetencyAreaId = _lookup.GetLookupItems<CompetencyAreaType>().SingleOrDefault(b => b.SysCode == "OTHER").Id;
            int domainCompetencyAreaId = _lookup.GetLookupItems<CompetencyAreaType>().SingleOrDefault(b => b.SysCode == "DOMAIN").Id;
            foreach (var x in json.ExperiencesList)
            {
                if (!string.IsNullOrEmpty(x.type.Value))
                {
                    EmployeeQualification eq = new EmployeeQualification();
                    eq.EmployeeId = emp.Id;
                    eq.QualificationTypeId = int.Parse(x.type.ToString());
                    string qualificationCode = _lookup.GetLookupItems<QualificationType>().SingleOrDefault(b => b.Id == eq.QualificationTypeId).SysCode;
                    if (qualificationCode == "OTHER_EXPERIENCE")
                        eq.CompetencyAreaTypeId = othCompetencyAreaId;
                    else
                        eq.CompetencyAreaTypeId = domainCompetencyAreaId;
                    eq.Name = x.details.ToString();
                    eq.TotalYears = int.Parse(x.totalYears.ToString());
                    _context.EmployeeQualifications.Add(eq);
                }
            }

            //update candidate
            //item.IsApproved = true;
            //await _context.SaveChangesAsync();
            return RedirectToAction("CandidatesList");
        }
    }
}