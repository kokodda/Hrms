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
    public class PersonnelController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;

        public PersonnelController(HrmsDbContext context, ILookupServices lookup)
        {
            _context = context;
            _lookup = lookup;
        }

        public IActionResult Index()
        {
            ViewBag.orgUnitsList = _context.OrgUnits.Include(b => b.OrgUnitType)
                        .Where(b => b.IsActive).OrderBy(b => b.OrgUnitType.SortOrder).ThenBy(b => b.SortOrder).ToList();
            ViewBag.nationalitiesList = _context.Nationalities.Where(b => b.IsActive).ToList();
            ViewBag.governoratesList = _context.Governorates.Where(b => b.IsActive).ToList();
            return View();
        }

        //employee
        public async Task<IActionResult> EmployeesList(long? ouId)
        {
            var model = _context.Employees.Include(b => b.Employments).ThenInclude(b => b.OrgUnit).Include(b => b.Governorate).Include(b => b.Nationality)
                                .Where(b => !ouId.HasValue || b.Employments.FirstOrDefault(c => c.IsActive && !c.IsActing).OrgUnitId == ouId);
            return PartialView("_EmployeesList", await model.ToListAsync());
        }

        public async Task<IActionResult> EmployeeIndex(long id)
        {
            var model = _context.Employees.Include(b => b.Employments).ThenInclude(b => b.OrgUnit)
                                .Include(b => b.Employments).ThenInclude(b => b.JobGrade)
                                .Include(b => b.Employments).ThenInclude(b => b.SalaryScaleType)
                                .Include(b => b.Employments).ThenInclude(b => b.SalaryStep)
                                .Include(b => b.Employments).ThenInclude(b => b.EmploymentType)
                                .Include(b => b.Nationality).Include(b => b.Governorate)
                                .SingleOrDefaultAsync(b => b.Id == id);
            return View("EmployeeIndex", await model);
        }

        //leave
        public async Task<IActionResult> LeaveBalancesList(long id)
        {
            var model = _context.EmployeeLeaveBalances.Include(b => b.LeaveType).Where(b => b.EmployeeId == id);
            return PartialView("_LeaveBalancesList", await model.ToListAsync());
        }

        public async Task<IActionResult> LeavesList(long id)
        {
            var model = _context.EmployeeLeaves.Include(b => b.LeaveType).Where(b => b.EmployeeId == id && b.ThruDate.Year == DateTime.Now.Year)
                                                .OrderByDescending(b => b.FromDate);
            return PartialView("_LeavesList", await model.ToListAsync());
        }

        public IActionResult AddLeave()
        {
            ViewBag.leaveTypesList = _lookup.GetLookupItems<LeaveType>();
            return PartialView("_AddLeave");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLeave(EmployeeLeave item)
        {
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.EmployeeLeaves.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("LeavesList", item.EmployeeId);
        }

        //family
        public async Task<IActionResult> FamilyList(long id)
        {
            var model = _context.EmployeeFamilies.Include(b => b.FamilyMemberType).Where(b => b.EmployeeId == id).OrderBy(b => b.FamilyMemberType.SortOrder);
            return PartialView("_FamilyList", await model.ToListAsync());
        }

        public IActionResult AddFamilyMember()
        {
            ViewBag.familyMemberTypesList = _lookup.GetLookupItems<FamilyMemberType>();
            return PartialView("_AddFamilyMember");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFamilyMember(EmployeeFamily item)
        {
            _context.EmployeeFamilies.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("FamilyList", new { id = item.EmployeeId });
        }

        //qualifications
        public async Task<IActionResult> QualificationsList(long id)
        {
            var model = _context.EmployeeQualifications.Include(b => b.QualificationType).Include(b => b.CompetencyAreaType).Include(b => b.CompetencySubCategory).ThenInclude(b => b.CompetencyCategory)
                                            .Where(b => b.EmployeeId == id).OrderBy(b => b.QualificationType.SortOrder);
            return PartialView("_QualificationsList", await model.ToListAsync());
        }

        public IActionResult AddQualification()
        {
            ViewBag.qualificationTypesList = _lookup.GetLookupItems<QualificationType>();
            ViewBag.competencyAreaTypesList = _lookup.GetLookupItems<CompetencyAreaType>();
            ViewBag.competencySubCategoriesList = _context.CompetencySubCategories.Include(b => b.CompetencyCategory)
                                    .Where(b => b.IsActive).OrderBy(b => b.CompetencyCategory.SortOrder).ThenBy(b => b.SortOrder).ToList();
            return PartialView("_AddQualification");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQualification(EmployeeQualification item)
        {
            _context.EmployeeQualifications.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("QualificationsList", new { id = item.EmployeeId });
        }

        //languages
        public async Task<IActionResult> LanguagesList(long id)
        {
            var model = _context.EmployeeLanguages.Include(b => b.LanguageType).Where(b => b.EmployeeId == id).OrderBy(b => b.LanguageType.SortOrder);
            return PartialView("_LanguagesList", await model.ToListAsync());
        }

        public IActionResult AddLanguage()
        {
            ViewBag.languageTypesList = _lookup.GetLookupItems<LanguageType>();
            return PartialView("_AddLanguage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLanguage(EmployeeLanguage item)
        {
            _context.EmployeeLanguages.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("LanguagesList", new { id = item.EmployeeId });
        }

        //documents
        public async Task<IActionResult> DocumentsList(long id)
        {
            var model = _context.EmployeeDocuments.Include(b => b.DocumentType).Where(b => b.EmployeeId == id && b.IsActive);
            return PartialView("_DocumentsList", await model.ToListAsync());
        }

        public IActionResult AddDocument()
        {
            ViewBag.documentTypesList = _lookup.GetLookupItems<DocumentType>();
            return PartialView("_AddDocument");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDocument(EmployeeDocument item)
        {
            _context.EmployeeDocuments.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("DocumentsList", new { id = item.EmployeeId });
        }

        //career
        public async Task<IActionResult> PromotionsList(long id)
        {
            var model = _context.EmployeePromotions.Include(b => b.Employment).ThenInclude(b => b.OrgUnit)
                                            .Include(b => b.JobGrade).Include(b => b.SalaryStep).Include(b => b.SalaryScaleType)
                                            .Where(b => b.Employment.EmployeeId == id && !b.IsCancelled).OrderByDescending(b => b.EffectiveFromDate);
            return PartialView("_PromotionsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddPromotion()
        {
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryScaleTypesList = _lookup.GetLookupItems<SalaryScaleType>();
            return PartialView("_AddPromotion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPromotion(EmployeePromotion item)
        {
            item.CreatedDate = DateTime.Now.Date;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.EmployeePromotions.Add(item);
            await _context.SaveChangesAsync();

            var pos = await _context.Employments.SingleOrDefaultAsync(b => b.Id == item.EmploymentId); 
            return RedirectToAction("PromotionsList", new { id = pos.EmployeeId });
        }

        //groups
        public async Task<IActionResult> GroupsList(long id)
        {
            var model = _context.EmployeeGroups.Include(b => b.GenericSubGroup).ThenInclude(b => b.GenericGroup)
                                .Where(b => b.EmployeeId == id && b.GenericSubGroup.IsActive).OrderBy(b => b.JoinDate);
            return PartialView("_GroupsList", await model.ToListAsync());
        }
    }
}