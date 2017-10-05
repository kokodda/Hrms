using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HrmsModel.Models;
using HrmsModel.Data;
using HrmsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;

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
            ViewBag.orgUnitTypesList = _lookup.GetLookupItems<OrgUnitType>();
            ViewBag.orgUnitsList = _context.OrgUnits.Include(b => b.OrgUnitType)
                        .Where(b => b.IsActive).OrderBy(b => b.OrgUnitType.SortOrder).ThenBy(b => b.SortOrder).ToList();
            ViewBag.nationalitiesList = _context.Nationalities.Where(b => b.IsActive).ToList();
            ViewBag.governoratesList = _context.Governorates.Where(b => b.IsActive).ToList();
            ViewBag.standardTitlesList = _lookup.GetLookupItems<StandardTitleType>();
            ViewBag.gradeGroupsList = _context.GradeGroups.Where(b => b.IsActive).ToList();
            ViewBag.jobGradesList = _context.JobGrades.Where(b => b.IsActive).ToList();
            return View();
        }

        //employee
        public IActionResult EmployeesList(string filterType, long? id)
        {
            int x;
            switch (filterType)
            {
                case "ouType":
                    string tCode = _lookup.GetLookupItems<OrgUnitType>().SingleOrDefault(b => b.Id == id).SysCode;
                    return ViewComponent("EmployeesList", new { orgUnitTypeCode = tCode });
                case "ou":
                    return ViewComponent("EmployeesList", new { orgUnitId = id });
                case "nationality":
                    x = (int)id;
                    return ViewComponent("EmployeesList", new { nationalityId = x });
                case "region":
                    x = (int)id;
                    return ViewComponent("EmployeesList", new { governorateId = x });
                case "title":
                    x = (int)id;
                    return ViewComponent("EmployeesList", new { titleId = x });
                case "gradeGroup":
                    x = (int)id;
                    return ViewComponent("EmployeesList", new { gradeGroupId = x });
                case "grade":
                    x = (int)id;
                    return ViewComponent("EmployeesList", new { jobGradeId = x });
                case "Probation":
                    return ViewComponent("EmployeesList", new { isProbation = true });
                case "Acting":
                    return ViewComponent("EmployeesList", new { isActing = true });
                case "Archive":
                    return ViewComponent("EmployeesList", new { isActive = false });
                case "Birthday":
                    return ViewComponent("EmployeesList", new { isBirthday = true });
                default:
                    return ViewComponent("EmployeesList", new { isHead = true });
            }
        }

        public IActionResult FindEmployee(string employeeUid, string employeeName)
        {
            return ViewComponent("EmployeesList", new { employeeUid = employeeUid, employeeName = employeeName });
        }

        public async Task<IActionResult> EmployeeIndex(long id)
        {
            EmployeeViewModel model = new EmployeeViewModel();
            var emp = await _context.Employments.Include(b => b.EmploymentType).Include(b => b.Employee).ThenInclude(b => b.Nationality)
                                        .Include(b => b.Employee).ThenInclude(b => b.Governorate)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.OrgUnitType)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.JobGrade).ThenInclude(b => b.GradeGroup)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.SalaryStep)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.StandardTitleType)
                                        .Include(b => b.Position).ThenInclude(b => b.JobGrade).ThenInclude(b => b.GradeGroup)
                                        .Include(b => b.Position).ThenInclude(b => b.SalaryStep)
                                        .Include(b => b.Position).ThenInclude(b => b.StandardTitleType)
                                        .Include(b => b.SalaryScaleType)
                                        .SingleOrDefaultAsync(b => b.Employee.Id == id);
            model.Employee = emp.Employee;
            model.EmployeeName = emp.Employee.FirstName + " " + emp.Employee.FatherName + " " + emp.Employee.FamilyName;
            model.OthEmployeeName = emp.Employee.OthFirstName + " " + emp.Employee.OthFatherName + " " + emp.Employee.OthFamilyName;
            model.OrgUnitId = emp.OrgUnitId;
            model.OrgUnitName = emp.OrgUnit.Name;
            model.OrgUnitTypeCode = emp.OrgUnit.OrgUnitType.SysCode;
            model.OrgUnitTypeName = emp.OrgUnit.OrgUnitType.Name;
            model.EmploymentId = emp.Id;
            model.IsHead = emp.IsHead;
            model.DateInPosition = emp.FromDate;
            model.EmploymentTypeCode = emp.EmploymentType.SysCode;
            model.EmploymentTypeName = emp.EmploymentType.Name;
            if (emp.IsHead)
            {
                model.PositionId = null;
                model.PositionName = emp.OrgUnit.HeadPositionName;
                if (emp.OrgUnit.StandardTitleTypeId.HasValue)
                {
                    model.StandardTitleCode = emp.OrgUnit.StandardTitleType.SysCode;
                    model.StandardTitleName = emp.OrgUnit.StandardTitleType.Name;
                }
                if (emp.OrgUnit.JobGradeId.HasValue)
                {
                    model.GradeGroupId = emp.OrgUnit.JobGrade.GradeGroupId;
                    model.GradeGroupCode = emp.OrgUnit.JobGrade.GradeGroup.Code;
                    model.JobGradeId = emp.OrgUnit.JobGradeId;
                    model.JobGradeCode = emp.OrgUnit.JobGrade.Code;
                }
                if (emp.OrgUnit.SalaryStepId.HasValue)
                {
                    model.SalaryStepId = emp.OrgUnit.SalaryStepId;
                    model.SalaryStepCode = emp.OrgUnit.SalaryStep.Code;
                }
            }
            else if (!emp.IsHead && emp.PositionId.HasValue)
            {
                model.PositionId = emp.Position.Id;
                model.PositionName = emp.Position.Name;
                if (emp.Position.StandardTitleTypeId.HasValue)
                {
                    model.StandardTitleCode = emp.Position.StandardTitleType.SysCode;
                    model.StandardTitleName = emp.Position.StandardTitleType.Name;
                }
                if (emp.Position.JobGradeId.HasValue)
                {
                    model.GradeGroupId = emp.Position.JobGrade.GradeGroupId;
                    model.GradeGroupCode = emp.Position.JobGrade.GradeGroup.Code;
                    model.JobGradeId = emp.Position.JobGradeId;
                    model.JobGradeCode = emp.Position.JobGrade.Code;
                }
                if (emp.Position.SalaryStepId.HasValue)
                {
                    model.SalaryStepId = emp.Position.SalaryStepId;
                    model.SalaryStepCode = emp.Position.SalaryStep.Code;
                }
            }
            else
            {
                model.PositionId = null;
                model.PositionName = emp.JobName;
            }
            if (emp.SalaryScaleTypeId.HasValue)
            {
                model.SalaryScaleTypeId = emp.SalaryScaleTypeId;
                model.SalaryScaleTypeName = emp.SalaryScaleType.Name;
                model.BasicSalary = 0;
            }
            else
                model.BasicSalary = emp.BasicSalary;
            model.IsActing = emp.IsActing;
            model.IsAttendRequired = emp.IsAttendRequired;
            model.IsOverTimeAllowed = emp.IsOverTimeAllowed;

            return View("EmployeeIndex", model);
        }

        public async Task<IActionResult> EditPersonalData(long id)
        {
            var model = await _context.Employees.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.nationalitiesList = await _context.Nationalities.Where(b => b.IsActive).ToListAsync();
            return PartialView("_EditPersonalData", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(Employee item)
        {
            var model = await _context.Employees.SingleOrDefaultAsync(b => b.Id == item.Id);
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            await TryUpdateModelAsync(model);

            await _context.SaveChangesAsync();
            return Content("Success", "text/html");
        }

        public async Task<IActionResult> EditContacts(long id)
        {
            var model = await _context.Employees.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.governoratesList = await _context.Governorates.Where(b => b.IsActive).ToListAsync();
            return PartialView("_EditContacts", model);
        }

        public async Task<IActionResult> EditBrief(long id)
        {
            var model = await _context.Employees.SingleOrDefaultAsync(b => b.Id == id);
            return PartialView("_EditBrief", model);
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
        public async Task<IActionResult> EmploymentsList(long id)
        {
            var model = _context.Employments.Include(b => b.OrgUnit).Include(b => b.Position).Include(b => b.EmploymentType)
                                .Include(b => b.SalaryScaleType).Include(b => b.SalaryStep)
                                .Where(b => b.EmployeeId == id && b.IsActive);
            return PartialView("_EmploymentsList", await model.ToListAsync());
        }

        public async Task<IActionResult> EditEmployment(long id)
        {
            var model = await _context.Employments.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.salaryScaleTypesList = _lookup.GetLookupItems<SalaryScaleType>();
            ViewBag.employmentTypesList = _lookup.GetLookupItems<EmploymentType>();
            ViewBag.positionsList = await _context.Positions.Where(b => b.OrgUnitId == model.OrgUnitId).ToListAsync();
            return PartialView("_EditEmployment", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployment(Employment item)
        {
            var model = await _context.Employments.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("EmploymentsList", new { id = model.EmployeeId });
        }

        public async Task<IActionResult> PromotionsList(long id)
        {
            var model = _context.EmployeePromotions.Include(b => b.PromotionType).Include(b => b.Employment).ThenInclude(b => b.OrgUnit)
                                            .Include(b => b.Employment).ThenInclude(b => b.Position)
                                            .Include(b => b.JobGrade).Include(b => b.SalaryStep)
                                            .Where(b => b.Employment.EmployeeId == id && !b.IsCancelled).OrderByDescending(b => b.EffectiveFromDate);
            return PartialView("_PromotionsList", await model.ToListAsync());
        }

        public async Task<IActionResult> AddPromotion(long id)
        {
            var model = await _context.Employments.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.basicSalary = model.BasicSalary;
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).ToListAsync();
            return PartialView("_AddPromotion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPromotion(EmployeePromotion item)
        {
            var model = await _context.Employments.SingleOrDefaultAsync(b => b.Id == item.EmploymentId);
            
            if (item.JobGradeId != model.JobGradeId)
                item.PromotionTypeId = _lookup.GetLookupItems<PromotionType>().SingleOrDefault(b => b.SysCode == "PROMO").Id;
            else
                item.PromotionTypeId = _lookup.GetLookupItems<PromotionType>().SingleOrDefault(b => b.SysCode == "SAL_INCR").Id;
            if (item.SalaryIncreaseValue != 0)
                item.BasicSalary = model.BasicSalary + (item.IsIncreasePercentage ? item.BasicSalary * item.SalaryIncreaseValue / 100 : item.SalaryIncreaseValue);
            item.CreatedDate = DateTime.Now.Date;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            _context.EmployeePromotions.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("PromotionsList", new { id = model.EmployeeId });
        }

        public async Task<IActionResult> ApprovePromotion(long id)
        {
            var model = await _context.EmployeePromotions.Include(b => b.Employment).SingleOrDefaultAsync(b => b.Id == id);
            model.IsApproved = true;
            model.Employment.JobGradeId = model.JobGradeId;
            model.Employment.SalaryStepId = model.SalaryStepId;
            model.Employment.BasicSalary = model.BasicSalary;

            await _context.SaveChangesAsync();

            return RedirectToAction("PromotionsList", new { id = model.Employment.EmployeeId });
        }

        public async Task<IActionResult> CancelPromotion(long id)
        {
            var model = await _context.EmployeePromotions.Include(b => b.Employment).SingleOrDefaultAsync(b => b.Id == id);
            model.IsCancelled = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("PromotionsList", new { id = model.Employment.EmployeeId });
        }

        //groups
        public async Task<IActionResult> GroupsList(long id)
        {
            var model = _context.EmployeeGroups.Include(b => b.GenericSubGroup).ThenInclude(b => b.GenericGroup)
                                .Where(b => b.EmployeeId == id && b.GenericSubGroup.IsActive).OrderBy(b => b.JoinDate);
            return PartialView("_GroupsList", await model.ToListAsync());
        }

        //uploads
        [HttpPost]
        public async Task<IActionResult> photoUpload(ICollection<IFormFile> files, long id)
        {
            var model = await _context.Employees.SingleOrDefaultAsync(b => b.Id == id);
            if (ModelState.IsValid)
            {
                try
                {
                    var file = files.FirstOrDefault();
                    string photoName = file.FileName;
                    byte[] photoFile;
                    using (MemoryStream str = new MemoryStream())
                    {
                        await file.OpenReadStream().CopyToAsync(str);
                        photoFile = str.ToArray();
                    }
                    model.Photo = photoFile;
                    await _context.SaveChangesAsync();
                    return Json(Convert.ToBase64String(photoFile));
                }
                catch
                {
                    return Json("Failed to Upload Photo! Please try again.");
                }
            }
            return Json("Failed to Upload Photo! Please try again.");
        }
    }
}