using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HrmsModel.Models;
using HrmsModel.Data;
using Microsoft.EntityFrameworkCore;

namespace HrmsApp.Controllers
{
    public class GovernanceController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;

        public GovernanceController(HrmsDbContext context, ILookupServices lookup)
        {
            _context = context;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            return View();
        }

        //grades
        public async Task<IActionResult> GradeGroupsList()
        {
            var model = _context.GradeGroups.OrderBy(b => b.SortOrder);
            return PartialView("_GradeGroupsList", await model.ToListAsync());
        }

        public IActionResult AddGradeGroup()
        {
            return PartialView("_AddGradeGroup");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGradeGroup(GradeGroup item)
        {
            int cnt = await _context.GradeGroups.CountAsync();
            item.Code = "G" + (cnt + 1).ToString();
            item.SortOrder = (cnt + 1) * 10;
            _context.GradeGroups.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("GradeGroupsList");
        }

        public async Task<IActionResult> EditGradeGroup(int id)
        {
            var model = _context.GradeGroups.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditGradeGroup", await model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGradeGroup(GradeGroup item)
        {
            var model = await _context.GradeGroups.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("GradeGroupsList");
        }

        public async Task<IActionResult> JobGradesList()
        {
            var model = _context.JobGrades.Include(b => b.GradeGroup).OrderBy(b => b.GradeGroup.SortOrder).ThenBy(b => b.SortOrder);
            return PartialView("_JobGradesList", await model.ToListAsync());
        }

        public IActionResult AddJobGrade()
        {
            ViewBag.gradeGroupsList = _context.GradeGroups.Where(b => b.IsActive).OrderBy(b => b.SortOrder).ToList();
            return PartialView("_AddJobGrade");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJobGrade(JobGrade item)
        {
            int cnt = await _context.JobGrades.CountAsync();
            int maxGrade = int.Parse(_context.JobGrades.OrderByDescending(b => b.SortOrder).First().Code);
            item.Code = (maxGrade + 1).ToString();
            item.SortOrder = (cnt + 1) * 10;
            _context.JobGrades.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("JobGradesList");
        }

        public async Task<IActionResult> EditJobGrade(int id)
        {
            var model = _context.JobGrades.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.gradeGroupsList = await _context.GradeGroups.Where(b => b.IsActive).OrderBy(b => b.SortOrder).ToListAsync();
            return PartialView("_EditJobGrade", await model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJobGrade(JobGrade item)
        {
            var model = await _context.JobGrades.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("JobGradesList");
        }

        public async Task<IActionResult> StandardTitleTypesList()
        {
            var model = _context.StandardTitleTypes.OrderBy(b => b.SortOrder);
            return PartialView("_StandardTitleTypesList", await model.ToListAsync());
        }

        public IActionResult AddStandardTitleType()
        {
            return PartialView("_AddStandardTitleType");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStandardTitleType(StandardTitleType item)
        {
            int cnt = await _context.StandardTitleTypes.CountAsync();
            item.SortOrder = (cnt + 1) * 10;
            _context.StandardTitleTypes.Add(item);
            await _context.SaveChangesAsync();
            _lookup.Refresh<StandardTitleType>();

            return RedirectToAction("StandardTitleTypesList");
        }

        public async Task<IActionResult> EditStandardTitleType(int id)
        {
            var model = _context.StandardTitleTypes.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditStandardTitleType", await model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStandardTitleType(StandardTitleType item)
        {
            var model = await _context.StandardTitleTypes.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();
            _lookup.Refresh<StandardTitleType>();

            return RedirectToAction("StandardTitleTypesList");
        }

        //competencies
        public async Task<IActionResult> CompetencyCategoriesList()
        {
            var model = _context.CompetencyCategories.OrderBy(b => b.SortOrder);
            return PartialView("_CompetencyCategoriesList", await model.ToListAsync());
        }

        public IActionResult AddCompetencyCategory()
        {
            return PartialView("_AddCompetencyCategory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompetencyCategory(CompetencyCategory item)
        {
            _context.CompetencyCategories.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("CompetencyCategoriesList");
        }

        public async Task<IActionResult> ToggleCompetencyCategory(int id, bool status)
        {
            _context.CompetencyCategories.SingleOrDefault(b => b.Id == id).IsActive = status;
            await _context.SaveChangesAsync();
            return RedirectToAction("CompetencyCategoriesList");
        }

        public IActionResult AddCompetencySubCategory()
        {
            ViewBag.competencyCategoriesList = _context.CompetencyCategories.Where(b => b.IsActive).OrderBy(b => b.SortOrder).ToList();
            return PartialView("_AddCompetencySubCategory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompetencySubCategory(CompetencySubCategory item)
        {
            _context.CompetencySubCategories.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("CompetencySubCategoriesList");
        }

        public async Task<IActionResult> ToggleCompetencySubCategory(int id, bool status)
        {
            _context.CompetencySubCategories.SingleOrDefault(b => b.Id == id).IsActive = status;
            await _context.SaveChangesAsync();
            return RedirectToAction("CompetencySubCategoriesList");
        }

        public async Task<IActionResult> CompetencySubCategoriesList()
        {
            var model = _context.CompetencySubCategories.Include(b => b.CompetencyCategory).OrderBy(b => b.CompetencyCategory.SortOrder).ThenBy(b => b.SortOrder);
            return PartialView("_CompetencySubCategoriesList", await model.ToListAsync());
        }

        public async Task<IActionResult> CompetenciesList(string level)
        {
            var model = _context.Competencies.Include(b => b.GradeGroup).Include(b => b.JobGrade)
                                        .Include(b => b.CompetencySubCategory).ThenInclude(b => b.CompetencyCategory)
                                        .Where(b => (level == "ORG" && b.IsCompanyPolicy)
                                        || (level == "GRADE_GROUP" && b.GradeGroupId.HasValue)
                                        || (level == "JOB_GRADE" && b.JobGradeId.HasValue));
            ViewBag.level = level;
            return PartialView("_CompetenciesList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddCompetency(string level)
        {
            ViewBag.gradeGroupsList = await _context.GradeGroups.ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.ToListAsync();
            ViewBag.competenciesList = await _context.CompetencySubCategories.ToListAsync();
            ViewBag.level = level;
            return PartialView("_AddCompetency");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompetency(Competency item, string level)
        {
            if (level == "ORG")
                item.IsCompanyPolicy = true;
            item.CreatedDate = DateTime.Now.Date;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.Competencies.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("CompetenciesList", new { level = level });
        }

        public async Task<IActionResult> EditCompetency(long id)
        {
            var model = await _context.Competencies.Include(b => b.CompetencySubCategory).ThenInclude(b => b.CompetencyCategory)
                                        .SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.gradeGroupsList = await _context.GradeGroups.ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.ToListAsync();
            return PartialView("_EditCompetency", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCompetency(Competency item)
        {
            var model = await _context.Competencies.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";
            await _context.SaveChangesAsync();

            string level;
            if (item.IsCompanyPolicy)
                level = "ORG";
            else if (item.GradeGroupId.HasValue)
                level = "GRADE_GROUP";
            else
                level = "JOB_GRADE";

            return RedirectToAction("CompetenciesList", new { level = level });
        }

        //leave
        public async Task<IActionResult> LeavePoliciesList(string level)
        {
            var model = _context.LeavePolicies.Include(b => b.GradeGroup).Include(b => b.JobGrade).Include(b => b.LeaveType)
                                        .Where(b => (level == "ORG" && b.IsCompanyPolicy)
                                        || (level == "GRADE_GROUP" && b.GradeGroupId.HasValue)
                                        || (level == "JOB_GRADE" && b.JobGradeId.HasValue));
            ViewBag.level = level;
            return PartialView("_LeavePoliciesList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddLeavePolicy(string level)
        {
            ViewBag.gradeGroupsList = await _context.GradeGroups.ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.ToListAsync();
            ViewBag.leaveTypesList = _lookup.GetLookupItems<LeaveType>();
            ViewBag.level = level;
            return PartialView("_AddLeavePolicy");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLeavePolicy(LeavePolicy item, string level)
        {
            if (level == "ORG")
                item.IsCompanyPolicy = true;
            item.CreatedDate = DateTime.Now.Date;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.LeavePolicies.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("LeavePoliciesList", new { level = level });
        }

        public async Task<IActionResult> EditLeavePolicy(long id)
        {
            var model = await _context.LeavePolicies.Include(b => b.LeaveType).SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.gradeGroupsList = await _context.GradeGroups.ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.ToListAsync();
            return PartialView("_EditLeavePolicy", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLeavePolicy(LeavePolicy item)
        {
            var model = await _context.LeavePolicies.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";
            await _context.SaveChangesAsync();

            string level;
            if (item.IsCompanyPolicy)
                level = "ORG";
            else if (item.GradeGroupId.HasValue)
                level = "GRADE_GROUP";
            else
                level = "JOB_GRADE";

            return RedirectToAction("LeavePoliciesList", new { level = level });
        }

        //allowances
        public async Task<IActionResult> AllowancePoliciesList(string level)
        {
            var model = _context.AllowancePolicies.Include(b => b.GradeGroup).Include(b => b.JobGrade).Include(b => b.AllowanceType)
                                        .Where(b => (level == "ORG" && b.IsCompanyPolicy)
                                        || (level == "GRADE_GROUP" && b.GradeGroupId.HasValue)
                                        || (level == "JOB_GRADE" && b.JobGradeId.HasValue));
            ViewBag.level = level;
            return PartialView("_AllowancePoliciesList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddAllowancePolicy(string level)
        {
            ViewBag.gradeGroupsList = await _context.GradeGroups.ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.ToListAsync();
            ViewBag.allowanceTypesList = _lookup.GetLookupItems<AllowanceType>();
            ViewBag.level = level;
            return PartialView("_AddAllowancePolicy");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAllowancePolicy(AllowancePolicy item, string level)
        {
            if (level == "ORG")
                item.IsCompanyPolicy = true;
            item.CreatedDate = DateTime.Now.Date;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.AllowancePolicies.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("AllowancePoliciesList", new { level = level });
        }

        public async Task<IActionResult> EditAllowancePolicy(long id)
        {
            var model = await _context.AllowancePolicies.Include(b => b.AllowanceType).SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.gradeGroupsList = await _context.GradeGroups.ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.ToListAsync();
            return PartialView("_EditAllowancePolicy", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllowancePolicy(AllowancePolicy item)
        {
            var model = await _context.AllowancePolicies.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";
            await _context.SaveChangesAsync();

            string level;
            if (item.IsCompanyPolicy)
                level = "ORG";
            else if (item.GradeGroupId.HasValue)
                level = "GRADE_GROUP";
            else
                level = "JOB_GRADE";

            return RedirectToAction("AllowancePoliciesList", new { level = level });
        }

        //salaries
        public async Task<IActionResult> SalaryStepsList()
        {
            var model = _context.SalarySteps.OrderBy(b => b.SortOrder);
            return PartialView("_SalaryStepsList", await model.ToListAsync());
        }

        public IActionResult AddSalaryStep()
        {
            return PartialView("_AddSalaryStep");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSalaryStep(SalaryStep item)
        {
            int cnt = await _context.SalarySteps.CountAsync();
            item.Code = "s" + cnt.ToString();
            item.SortOrder = (cnt + 1) * 10;
            _context.SalarySteps.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("SalaryStepsList");
        }

        public async Task<IActionResult> EditSalaryStep(int id)
        {
            var model = await _context.SalarySteps.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditSalaryStep", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSalaryStep(SalaryStep item)
        {
            var model = await _context.SalarySteps.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("SalaryStepsList");
        }

        public async Task<IActionResult> SalaryScalesList()
        {
            var model = _context.SalaryScales.Include(b => b.FromJobGrade).Include(b => b.ThruJobGrade).Include(b => b.SalaryScaleType);
            return PartialView("_SalaryScalesList", await model.Take(33).OrderByDescending(b => b.IsActive).ThenBy(b => b.FromDate).ToListAsync());
        }

        public async Task<IActionResult> AddSalaryScale()
        {
            ViewBag.jobGradesList = await _context.JobGrades.ToListAsync();
            ViewBag.salaryScaleTypesList = _lookup.GetLookupItems<SalaryScaleType>();
            return PartialView("_AddSalaryScale");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSalaryScale(SalaryScale item)
        {
            var x = await _context.SalaryScales.FirstOrDefaultAsync(b => b.SalaryScaleTypeId == item.SalaryScaleTypeId && b.IsActive);
            if (x != null)
            {
                x.ThruDate = item.FromDate.AddDays(-1);
                x.IsActive = false;
            }
            item.UpdatedBy = "user";
            _context.SalaryScales.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("SalaryScalesList");
        }

        public async Task<IActionResult> PreviewSalaryScale(long id)
        {
            var ss = await _context.SalaryScales.SingleOrDefaultAsync(b => b.Id == id);
            int startGradeSrl = _context.JobGrades.SingleOrDefault(b => b.Id == ss.ThruJobGradeId).SortOrder;
            int endGradeSrl = _context.JobGrades.SingleOrDefault(b => b.Id == ss.FromJobGradeId).SortOrder;
            int tmp;
            if(startGradeSrl > endGradeSrl) //swap min and max values
            {
                tmp = startGradeSrl;
                startGradeSrl = endGradeSrl;
                endGradeSrl = tmp;
            }
            var gradesList = await _context.JobGrades.Include(b => b.GradeGroup)
                                        .Where(b => b.SortOrder >= startGradeSrl && b.SortOrder <= endGradeSrl)
                                        .OrderByDescending(b => b.SortOrder).ToListAsync();
            var stepsList = await _context.SalarySteps.Where(b => b.IsActive).OrderBy(b => b.SortOrder).ToListAsync();

            List<string> model = new List<string>();
            string hdr = " | ";
            int minSalary = ss.MinSalary;
            foreach (var x in stepsList)
                hdr = hdr + "|" + x.Code;
            model.Add(hdr);
            string r;
            foreach (var y in gradesList)
            {
                r = y.GradeGroup.Code + "|" + y.Code;
                minSalary += minSalary * y.SalaryIncrPctg / 100;
                foreach (var x in stepsList)
                    r = r + "|" + (minSalary * x.SalaryIncrPctg / 100).ToString();
                model.Add(r);
            }
            model.Add(hdr);
            ViewBag.salaryScaleName = ss.Name + " (" + ss.CurrencyCode + ")";
            return PartialView("_PreviewSalaryScale", model);
        }
    }
}