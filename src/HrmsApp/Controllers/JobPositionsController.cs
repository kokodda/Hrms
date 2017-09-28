﻿using System;
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
    public class JobPositionsController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly ILookupServices _lookup;

        public JobPositionsController(HrmsDbContext context,
                                    IHostingEnvironment environment,
                                    ILookupServices lookup)
        {
            _context = context;
            _environment = environment;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> JobPositionsList()
        {
            var model = _context.OrgUnits.Include(b => b.JobGrade).Include(b => b.StandardTitleType).Include(b => b.OrgUnitType)
                                        .Where(b => b.IsActive).OrderBy(b => b.OrgUnitType.SortOrder).ThenBy(b => b.SortOrder);
            return PartialView("_JobPositionsList", await model.ToListAsync());
        }

        public async Task<IActionResult> Edit(long id)
        {
            var model = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).OrderBy(b => b.SortOrder).ToListAsync();
            ViewBag.standardTitleTypesList = _lookup.GetLookupItems<StandardTitleType>();
            return PartialView("_Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrgUnit item)
        {
            var model = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";
            await _context.SaveChangesAsync();

            return RedirectToAction("JobPositionsList");
        }

        public async Task<IActionResult> Details(long id)
        {
            var model = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == id);
            string reportingTo = "NA";
            string lineManager = "NA";
            if (model.Code.Length > 3)
            {
                string parentCode = model.Code.Substring(0, model.Code.Length - 3);
                reportingTo = _context.OrgUnits.SingleOrDefault(b => b.Code == parentCode).HeadPositionName;
                if (model.LineManagerOrgUnitId.HasValue)
                    lineManager = _context.OrgUnits.SingleOrDefault(b => b.Id == model.LineManagerOrgUnitId.Value).HeadPositionName;
            }
            ViewBag.reportingTo = reportingTo;
            ViewBag.lineManager = lineManager;

            return PartialView("_Details", model);
        }

        public async Task<IActionResult> PersonnelList(long id)
        {
            var ou = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == id);
            var pos = await _context.Employments.Include(b => b.Employee).SingleOrDefaultAsync(b => b.OrgUnitId == id && !b.IsActing && b.IsActive);
            if (pos != null)
                ViewBag.headedBy = pos.Employee.FirstName + " " + pos.Employee.FamilyName;
            else
                ViewBag.headedBy = "NA";

            List<string> model = new List<string>();
            var x = await _context.Employments.Include(b => b.OrgUnit).Include(b => b.Employee)
                            .Where(b => b.OrgUnit.Code.StartsWith(ou.Code) && b.OrgUnit.Code.Length == ou.Code.Length + 3 && b.IsActive)
                            .Select(b => new { pName = b.Employee.FirstName + " " + b.Employee.FamilyName + " - " + b.OrgUnit.HeadPositionName }).Distinct().ToListAsync();
            foreach (var x1 in x)
                model.Add(x1.pName);

            int cnt = await _context.Employments.Include(b => b.OrgUnit).Include(b => b.Employee)
                            .Where(b => b.OrgUnit.Code.StartsWith(ou.Code) && b.OrgUnit.Code.Length > ou.Code.Length + 3 && b.IsActive)
                            .Distinct().CountAsync();
            ViewBag.indirectSubordinates = cnt.ToString();
            return PartialView("_PersonnelList", model);
        }

        public async Task<IActionResult> CandidatesList(long id)
        {
            var model = _context.Candidates.Where(b => b.OrgUnitId == id);
            return PartialView("_CandidatesList", await model.ToListAsync());
        }

        //new employee or candidate
        public async Task<IActionResult> AddEmployee(long ouId)
        {
            var x = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == ouId);
            ViewBag.orgUnitName = x.Name;
            ViewBag.positionName = x.HeadPositionName;
            ViewBag.orgUnitId = ouId;
            ViewBag.nationalitiesList = await _context.Nationalities.Where(b => b.IsActive).ToListAsync();
            ViewBag.governoratesList = await _context.Governorates.Where(b => b.IsActive).ToListAsync();
            ViewBag.employeeTypesList = _lookup.GetLookupItems<EmploymentType>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(Employee item, long orgUnitId, string jobName, string othJobName, int? employeeTypeId, DateTime fromDate, bool isProbation = false)
        {
            item.IsActive = true;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.Employees.Add(item);

            var ou = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == orgUnitId);
            Employment pos = new Employment();
            pos.OrgUnitId = orgUnitId;
            pos.EmployeeId = item.Id;
            if (!employeeTypeId.HasValue)
                pos.EmploymentTypeId = _lookup.GetLookupItems<EmploymentType>().SingleOrDefault(b => b.SysCode == "FULL_TIME").Id;
            pos.JobName = jobName;
            pos.OthJobName = othJobName;
            pos.JobGradeId = ou.JobGradeId;
            pos.FromDate = fromDate;
            pos.IsActive = true;
            pos.IsProbation = isProbation;
            _context.Employments.Add(pos);

            EmployeePromotion prom = new EmployeePromotion();
            prom.EmploymentId = pos.Id;
            prom.JobGradeId = ou.JobGradeId;
            prom.CreatedDate = DateTime.Now.Date;
            prom.LastUpdated = DateTime.Now.Date;
            prom.UpdatedBy = "user";
            _context.EmployeePromotions.Add(prom);


            //add leave entitlements
            int annualTypeId = _lookup.GetLookupItems<LeaveType>().SingleOrDefault(b => b.SysCode == "ANNUAL").Id;
            int sickTypeId = _lookup.GetLookupItems<LeaveType>().SingleOrDefault(b => b.SysCode == "SICK").Id;
            _context.EmployeeLeaveBalances.Add(new EmployeeLeaveBalance { EmployeeId = item.Id, LeaveTypeId = annualTypeId, AnnualEntitlement = 0 });
            _context.EmployeeLeaveBalances.Add(new EmployeeLeaveBalance { EmployeeId = item.Id, LeaveTypeId = sickTypeId, AnnualEntitlement = 0 });

            await _context.SaveChangesAsync();
            return Content("Employee Successfully Added.", "text/html");
        }

        [HttpPost]
        public async Task<IActionResult> fileUpload(ICollection<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName;
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "\\JDs");
                            DirectoryInfo di = new DirectoryInfo(Path.GetFullPath(uploadsFolder));

                            using (var fileStream = new FileStream(Path.Combine(uploadsFolder, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                fileName = file.FileName;
                            }
                        }
                    }
                    return Json("Successfull");
                }
                catch
                {
                    return Json("Upload Failed! Please Try Again Later.");
                }
            }
            return Json("Upload Failed! Please Try Again Later.");
        }
    }
}