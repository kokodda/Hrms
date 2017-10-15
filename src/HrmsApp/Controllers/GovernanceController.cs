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

        public async Task<IActionResult> GradeGroupDetails(int id)
        {
            List<string> model = new List<string>();
            List<int> Ids = new List<int>();

            //
            var x = _context.AllowancePolicies.Include(b => b.AllowanceType).OrderBy(b => b.AllowanceType.SortOrder);
            var xGroup = await x.Where(b => b.GradeGroupId == id).ToListAsync();
            Ids = xGroup.Select(b => b.AllowanceTypeId).ToList();
            var xCompany = await x.Where(b => b.IsCompanyPolicy && !Ids.Contains(b.AllowanceTypeId)).ToListAsync();
            model.Add("Title|Allowances Policy|");
            foreach(var xCompany1 in xCompany)
                model.Add(xCompany1.AllowanceType.Name + "|" + xCompany1.Amount + (xCompany1.isPercentage ? "% of Basic Salary" : null) + "|Company Level");
            foreach (var xGroup1 in xGroup)
                model.Add(xGroup1.AllowanceType.Name + "|" + xGroup1.Amount + (xGroup1.isPercentage ? "% of Basic Salary|" : "|"));

            //
            var y = _context.LeavePolicies.Include(b => b.LeaveType).OrderBy(b => b.LeaveType.SortOrder);
            var yGroup = await y.Where(b => b.GradeGroupId == id).ToListAsync();
            Ids = yGroup.Select(b => b.LeaveTypeId).ToList();
            var yCompany = await y.Where(b => b.IsCompanyPolicy && !Ids.Contains(b.LeaveTypeId)).ToListAsync();
            model.Add("Title|Leave Policy|");
            foreach (var yCompany1 in yCompany)
                model.Add(yCompany1.LeaveType.Name + "|" + yCompany1.TotalDays + " days|Company Level");
            foreach (var yGroup1 in yGroup)
                model.Add(yGroup1.LeaveType.Name + "|" + yGroup1.TotalDays + " days|");

            //
            var z = _context.Competencies.Include(b => b.CompetencySubCategory).ThenInclude(b => b.CompetencyCategory)
                                .OrderBy(b => b.CompetencySubCategory.CompetencyCategory.SortOrder).ThenBy(b => b.CompetencySubCategory.SortOrder);
            var zGroup = await z.Where(b => b.GradeGroupId == id).ToListAsync();
            Ids = zGroup.Select(b => b.CompetencySubCategoryId).ToList();
            var zCompany = await z.Where(b => b.IsCompanyPolicy && !Ids.Contains(b.CompetencySubCategoryId)).OrderBy(b => b.CompetencySubCategory.CompetencyCategory.SortOrder)
                                .ThenBy(b => b.CompetencySubCategory.SortOrder).ToListAsync();
            model.Add("Title|Competencies|");
            foreach (var zCompany1 in zCompany)
                model.Add(zCompany1.CompetencySubCategory.CompetencyCategory.Name + " -> " + zCompany1.CompetencySubCategory.Name + "|" + zCompany1.Requirements + "|Company Level");
            foreach (var zGroup1 in zGroup)
                model.Add(zGroup1.CompetencySubCategory.CompetencyCategory.Name + " -> " + zGroup1.CompetencySubCategory.Name + "|" + zGroup1.Requirements + "|");

            return PartialView("_GradeGroupDetails", model);
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

        public async Task<IActionResult> JobGradeDetails(int id)
        {
            List<string> model = new List<string>();
            List<int> Ids = new List<int>();
            int gradeGroupId = _context.JobGrades.SingleOrDefault(b => b.Id == id).GradeGroupId;
            //
            var x = _context.AllowancePolicies.Include(b => b.AllowanceType).OrderBy(b => b.AllowanceType.SortOrder);
            var xGrade = await x.Where(b => b.JobGradeId == id).ToListAsync();
            Ids = xGrade.Select(b => b.AllowanceTypeId).ToList();
            var xCompany = await x.Where(b => b.IsCompanyPolicy && !Ids.Contains(b.AllowanceTypeId)).ToListAsync();
            var xGroup = await x.Where(b => b.GradeGroupId == gradeGroupId && !Ids.Contains(b.AllowanceTypeId)).ToListAsync();
            model.Add("Title|Allowances Policy|");
            foreach (var xCompany1 in xCompany)
                model.Add(xCompany1.AllowanceType.Name + "|" + xCompany1.Amount + (xCompany1.isPercentage ? "% of Basic Salary" : null) + "|Company Level");
            foreach (var xGroup1 in xGroup)
                model.Add(xGroup1.AllowanceType.Name + "|" + xGroup1.Amount + (xGroup1.isPercentage ? "% of Basic Salary" : null) + "|Grade Group Level");
            foreach (var xGrade1 in xGrade)
                model.Add(xGrade1.AllowanceType.Name + "|" + xGrade1.Amount + (xGrade1.isPercentage ? "% of Basic Salary|" : "|"));

            //
            var y = _context.LeavePolicies.Include(b => b.LeaveType).OrderBy(b => b.LeaveType.SortOrder);
            var yGrade = await y.Where(b => b.JobGradeId == id).ToListAsync();
            Ids = yGrade.Select(b => b.LeaveTypeId).ToList();
            var yCompany = await y.Where(b => b.IsCompanyPolicy && !Ids.Contains(b.LeaveTypeId)).ToListAsync();
            var yGroup = await y.Where(b => b.GradeGroupId == gradeGroupId && !Ids.Contains(b.LeaveTypeId)).ToListAsync();
            model.Add("Title|Leave Policy|");
            foreach (var yCompany1 in yCompany)
                model.Add(yCompany1.LeaveType.Name + "|" + yCompany1.TotalDays + " days|Company Level");
            foreach (var yGroup1 in yGroup)
                model.Add(yGroup1.LeaveType.Name + "|" + yGroup1.TotalDays + " days|Grade Group Level");
            foreach (var yGrade1 in yGrade)
                model.Add(yGrade1.LeaveType.Name + "|" + yGrade1.TotalDays + " days|");

            //
            var z = _context.Competencies.Include(b => b.CompetencySubCategory).ThenInclude(b => b.CompetencyCategory)
                                .OrderBy(b => b.CompetencySubCategory.CompetencyCategory.SortOrder).ThenBy(b => b.CompetencySubCategory.SortOrder);
            var zGrade = await z.Where(b => b.JobGradeId == id).ToListAsync();
            Ids = zGrade.Select(b => b.CompetencySubCategoryId).ToList();
            var zCompany = await z.Where(b => b.IsCompanyPolicy && !Ids.Contains(b.CompetencySubCategoryId)).ToListAsync();
            var zGroup = await z.Where(b => b.GradeGroupId == gradeGroupId && !Ids.Contains(b.CompetencySubCategoryId)).ToListAsync();
            model.Add("Title|Competencies|");
            foreach (var zCompany1 in zCompany)
                model.Add(zCompany1.CompetencySubCategory.CompetencyCategory.Name + " -> " + zCompany1.CompetencySubCategory.Name + "|" + zCompany1.Requirements + "|Company Level");
            foreach (var zGroup1 in zGroup)
                model.Add(zGroup1.CompetencySubCategory.CompetencyCategory.Name + " -> " + zGroup1.CompetencySubCategory.Name + "|" + zGroup1.Requirements + "|Grade Group Level");
            foreach (var zGrade1 in zGrade)
                model.Add(zGrade1.CompetencySubCategory.CompetencyCategory.Name + " -> " + zGrade1.CompetencySubCategory.Name + "|" + zGrade1.Requirements + "|");

            return PartialView("_JobGradeDetails", model);
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