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
    public class PositionsController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly ILookupServices _lookup;

        public PositionsController(HrmsDbContext context,
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

        //org units heads
        public async Task<IActionResult> HeadsList(int? id)
        {
            var model = _context.OrgUnits.Include(b => b.JobGrade).Include(b => b.SalaryStep).Include(b => b.StandardTitleType).Include(b => b.OrgUnitType)
                                        .Where(b => (!id.HasValue || b.OrgUnitTypeId == id) && b.IsActive)
                                        .OrderBy(b => b.OrgUnitType.SortOrder).ThenBy(b => b.SortOrder);
            return PartialView("_HeadsList", await model.ToListAsync());
        }

        public async Task<IActionResult> EditHead(long id)
        {
            var model = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.orgUnitsList = await _context.OrgUnits.ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).OrderBy(b => b.SortOrder).ToListAsync();
            ViewBag.salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).ToListAsync();
            ViewBag.standardTitlesList = _lookup.GetLookupItems<StandardTitleType>();
            return PartialView("_EditHead", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHead(OrgUnit item, int? ouTypeId)
        {
            var model = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";
            await _context.SaveChangesAsync();

            return RedirectToAction("HeadsList", new { id = ouTypeId });
        }

        public async Task<IActionResult> HeadDetails(long id)
        {
            List<string> model = new List<string>();
            var ou = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == id);

            //direct subordinates
            int cnt1 = await _context.OrgUnits.Where(b => b.Code.StartsWith(ou.Code) && b.Code.Length == ou.Code.Length + 3 && b.IsActive).CountAsync();
            int cnt2 = await _context.Positions.Include(b => b.OrgUnit).Where(b => b.OrgUnit.Code == ou.Code && b.IsActive).SumAsync(b => b.Capacity);
            model.Add("Direct Subordidates|" + (cnt1 + cnt2).ToString());
            //indirect subordinates
            int cnt3 = await _context.OrgUnits.Where(b => b.Code.StartsWith(ou.Code) && b.Code.Length > ou.Code.Length + 3 && b.IsActive).CountAsync();
            int cnt4 = await _context.Positions.Include(b => b.OrgUnit).Where(b => b.OrgUnit.Code.StartsWith(ou.Code) && b.OrgUnit.Code.Length > ou.Code.Length && b.IsActive).SumAsync(b => b.Capacity);
            model.Add("Indirect Subordidates|" + (cnt3 + cnt4).ToString());

            string reportingTo = "NA";
            string lineManager = "NA";
            if (ou.Code.Length > 3)
            {
                string parentCode = ou.Code.Substring(0, ou.Code.Length - 3);
                reportingTo = _context.OrgUnits.SingleOrDefault(b => b.Code == parentCode).HeadPositionName;
                if (ou.LineManagerOrgUnitId.HasValue)
                    lineManager = _context.OrgUnits.SingleOrDefault(b => b.Id == ou.LineManagerOrgUnitId.Value).HeadPositionName;
            }
            model.Add("Reporting To|" + reportingTo);
            model.Add("Line Manager|" + lineManager);
            model.Add("Job Weight|" + (ou.JobWeight.HasValue ? ou.JobWeight.Value.ToString() : "NG"));
            model.Add("Attendance|" + (ou.IsAttendRequired ? "Required" : "Not Required"));
            model.Add("Overtime|" + (ou.IsOverTimeAllowed ? "Allowed" : "Not Allowed"));
            model.Add("Created On|" + ou.CreatedDate.ToString("dd-MM-yyyy"));
            model.Add("Last Updated|" + ou.LastUpdated.ToString("dd-MM-yyyy"));
            model.Add("Updated By|" + ou.UpdatedBy);

            ViewBag.positionName = ou.HeadPositionName;
            return PartialView("_Details", model);
        }

        public async Task<IActionResult> UpdateHeadJD(long id, string fileName, int? ouTypeId)
        {
            var model = await _context.OrgUnits.SingleOrDefaultAsync(b => b.Id == id);
            model.JobDescription = fileName;
            await _context.SaveChangesAsync();
            return RedirectToAction("HeadsList", new { id = ouTypeId });
        }

        //positions
        public async Task<IActionResult> PositionsList(int? id)
        {
            //id is the org unit type id used for filter
            var model = _context.Positions.Include(b => b.JobGrade).Include(b => b.SalaryStep).Include(b => b.StandardTitleType)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.OrgUnitType).Include(b => b.Employments)
                                        .Where(b => (!id.HasValue || b.OrgUnit.OrgUnitTypeId == id) && b.OrgUnit.IsActive && b.IsActive)
                                        .OrderBy(b => b.OrgUnit.OrgUnitType.SortOrder).ThenBy(b => b.OrgUnit.SortOrder);
            return PartialView("_PositionsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddPosition(int? id)
        {
            //id is the org unit type id
            ViewBag.orgUnitsList = await _context.OrgUnits.Where(b => !id.HasValue || b.OrgUnitTypeId == id).ToListAsync();
            ViewBag.standardTitlesList = _lookup.GetLookupItems<StandardTitleType>();
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).ToListAsync();
            return PartialView("_AddPosition");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPosition(Position item, int? ouTypeId)
        {
            item.CreatedDate = DateTime.Now.Date;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.Positions.Add(item);

            await _context.SaveChangesAsync();
            return RedirectToAction("PositionsList", new { id = ouTypeId });
        }

        public async Task<IActionResult> EditPosition(long id)
        {
            var model = await _context.Positions.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.orgUnitsList = await _context.OrgUnits.ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).OrderBy(b => b.SortOrder).ToListAsync();
            ViewBag.salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).ToListAsync();
            ViewBag.standardTitlesList = _lookup.GetLookupItems<StandardTitleType>();
            return PartialView("_EditPosition", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPosition(Position item, int? ouTypeId)
        {
            var model = await _context.Positions.Include(b => b.OrgUnit).SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";
            await _context.SaveChangesAsync();

            return RedirectToAction("PositionsList", new { id = ouTypeId });
        }

        public async Task<IActionResult> PositionDetails(long id)
        {
            List<string> model = new List<string>();
            var pos = await _context.Positions.Include(b => b.OrgUnit).SingleOrDefaultAsync(b => b.Id == id);
            
            string reportingTo = pos.OrgUnit.HeadPositionName;
            string lineManager = "NA";
            if (pos.ReportingToOrgUnitId.HasValue)
                reportingTo = _context.OrgUnits.SingleOrDefault(b => b.Id == pos.ReportingToOrgUnitId).HeadPositionName;
            if(pos.LineManagerOrgUnitId.HasValue)
                lineManager = _context.OrgUnits.SingleOrDefault(b => b.Id == pos.LineManagerOrgUnitId).HeadPositionName;
            model.Add("Reporting To|" + reportingTo);
            model.Add("Line Manager|" + lineManager);
            model.Add("Job Weight|" + (pos.JobWeight.HasValue ? pos.JobWeight.Value.ToString() : "NG"));
            model.Add("Attendance|" + (pos.IsAttendRequired ? "Required" : "Not Required"));
            model.Add("Overtime|" + (pos.IsOverTimeAllowed ? "Allowed" : "Not Allowed"));
            model.Add("Created On|" + pos.CreatedDate.ToString("dd-MM-yyyy"));
            model.Add("Last Updated|" + pos.LastUpdated.ToString("dd-MM-yyyy"));
            model.Add("Updated By|" + pos.UpdatedBy);

            ViewBag.positionName = pos.Name;
            return PartialView("_Details", model);
        }

        public async Task<IActionResult> UpdatePositionJD(long id, string fileName, int? ouTypeId)
        {
            var model = await _context.Positions.SingleOrDefaultAsync(b => b.Id == id);
            model.JobDescription = fileName;
            await _context.SaveChangesAsync();
            return RedirectToAction("PositionsList", new { id = ouTypeId });
        }

        /// <summary>
        /// ////////////////
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
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
                            var uploadsFolder = Path.Combine(_environment.ContentRootPath, @"wwwroot\files\JDs");
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