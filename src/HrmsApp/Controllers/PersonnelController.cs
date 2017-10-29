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
using Microsoft.AspNetCore.Hosting;

namespace HrmsApp.Controllers
{
    public class PersonnelController : Controller
    {
        private readonly HrmsDbContext _context;
        private readonly ILookupServices _lookup;
        private readonly IHostingEnvironment _environment;

        public PersonnelController(HrmsDbContext context, ILookupServices lookup, IHostingEnvironment environment)
        {
            _context = context;
            _lookup = lookup;
            _environment = environment;
        }

        //generic functions
        private int GetBasicSalary(int salaryScaleTypeId, int jobGradeId, int salaryStepId)
        {
            int basicSalary = _context.SalaryScales.SingleOrDefault(b => b.SalaryScaleTypeId == salaryScaleTypeId && b.IsActive).MinSalary;
            var jobGrade = _context.JobGrades.SingleOrDefault(b => b.Id == jobGradeId);
            var salaryStep = _context.SalarySteps.SingleOrDefault(b => b.Id == salaryStepId);
            var gradesList = _context.JobGrades.Where(b => b.SortOrder >= jobGrade.SortOrder && b.IsActive).OrderByDescending(b => b.SortOrder).ToList();
            foreach (var g in gradesList)
            {
                if (g.SalaryIncrPctg != 0)
                    basicSalary += (basicSalary * g.SalaryIncrPctg / 100);
            }
            basicSalary = basicSalary * salaryStep.SalaryIncrPctg / 100;
            return basicSalary;
        }
        private List<string> GetAllowances(int jobGradeId, int basicSalary)
        {
            List<string> remunerationList = new List<string>();
            List<int> Ids = new List<int>();
            var allowancesList = _context.AllowancePolicies.Include(b => b.AllowanceType).Where(b => b.IsActive).ToList();
            //grade allowances
            var gradeAllowances = allowancesList.Where(b => b.JobGradeId == jobGradeId);
            Ids.AddRange(gradeAllowances.Select(b => b.AllowanceTypeId));
            foreach (var a in gradeAllowances)
            {
                if (a.isPercentage)
                    remunerationList.Add(a.AllowanceTypeId + "|" + a.AllowanceType.Name + "|" + (basicSalary * a.Amount / 100));
                else
                    remunerationList.Add(a.AllowanceTypeId + "|" + a.AllowanceType.Name + "|" + (basicSalary + a.Amount));
            }
            int gradeGroupId = _context.JobGrades.SingleOrDefault(b => b.Id == jobGradeId).GradeGroupId;
            var groupAllowances = allowancesList.Where(b => b.GradeGroupId == gradeGroupId && !Ids.Contains(b.AllowanceTypeId));
            Ids.AddRange(groupAllowances.Select(b => b.AllowanceTypeId));
            foreach (var a in groupAllowances)
            {
                if (a.isPercentage)
                    remunerationList.Add(a.AllowanceTypeId + "|" + a.AllowanceType.Name + "|" + (basicSalary * a.Amount / 100));
                else
                    remunerationList.Add(a.AllowanceTypeId + "|" + a.AllowanceType.Name + "|" + (basicSalary + a.Amount));
            }
            var companyAllowances = allowancesList.Where(b => b.IsCompanyPolicy && !Ids.Contains(b.AllowanceTypeId));
            foreach (var a in companyAllowances)
            {
                if (a.isPercentage)
                    remunerationList.Add(a.AllowanceTypeId + "|" + a.AllowanceType.Name + "|" + (basicSalary * a.Amount / 100));
                else
                    remunerationList.Add(a.AllowanceTypeId + "|" + a.AllowanceType.Name + "|" + (basicSalary + a.Amount));
            }
            return remunerationList;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.orgUnitTypesList = _lookup.GetLookupItems<OrgUnitType>();
            ViewBag.orgUnitsList = await _context.OrgUnits.Include(b => b.OrgUnitType)
                        .Where(b => b.IsActive).OrderBy(b => b.OrgUnitType.SortOrder).ThenBy(b => b.SortOrder).ToListAsync();
            ViewBag.nationalitiesList = await _context.Nationalities.Where(b => b.IsActive).ToListAsync();
            ViewBag.governoratesList = await _context.Governorates.Where(b => b.IsActive).ToListAsync();
            ViewBag.standardTitlesList = _lookup.GetLookupItems<StandardTitleType>();
            ViewBag.gradeGroupsList = await _context.GradeGroups.Where(b => b.IsActive).ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).ToListAsync();

            var emp = _context.Employments.Where(b => b.IsActive);
            ViewBag.headsCnt = await emp.Where(b => b.IsHead).CountAsync();
            ViewBag.assignedCnt = await emp.Where(b => b.IsHead || b.PositionId.HasValue).CountAsync();
            ViewBag.unassignedCnt = await emp.Where(b => !b.IsHead && !b.PositionId.HasValue).CountAsync();
            ViewBag.actingCnt = await emp.Where(b => b.IsActing).CountAsync();
            ViewBag.probationCnt = await emp.Where(b => b.IsProbation).CountAsync();
            ViewBag.birthdayCnt = await _context.Employees.Where(b => b.BirthDate.DayOfYear == DateTime.Today.Date.DayOfYear).CountAsync();
            ViewBag.archiveCnt = await _context.Employees.Where(b => !b.IsActive).CountAsync();
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
                case "Assigned":
                    return ViewComponent("EmployeesList", new { assigned = "assigned" });
                case "Unassigned":
                    return ViewComponent("EmployeesList", new { assigned = "unassigned" });
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
            var e = await _context.Employments.Include(b => b.EmploymentType).Include(b => b.Employee).ThenInclude(b => b.Nationality)
                                        .Include(b => b.Employee).ThenInclude(b => b.Governorate)
                                        .Include(b => b.JobGrade).ThenInclude(b => b.GradeGroup).Include(b => b.SalaryStep)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.OrgUnitType)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.JobGrade).ThenInclude(b => b.GradeGroup)
                                        .Include(b => b.OrgUnit)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.StandardTitleType)
                                        .Include(b => b.Position).ThenInclude(b => b.JobGrade).ThenInclude(b => b.GradeGroup)
                                        .Include(b => b.Position)
                                        .Include(b => b.Position).ThenInclude(b => b.StandardTitleType)
                                        .Include(b => b.SalaryScaleType)
                                        .Where(b => b.Employee.Id == id && b.IsActive).ToListAsync();
            var emp = e.FirstOrDefault();
            if (e.Count() > 1)
                emp = e.SingleOrDefault(b => !b.IsActing);
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
                if (emp.JobGradeId.HasValue)
                {
                    model.GradeGroupId = emp.JobGrade.GradeGroupId;
                    model.GradeGroupCode = emp.JobGrade.GradeGroup.Code;
                    model.JobGradeId = emp.JobGradeId;
                    model.JobGradeCode = emp.JobGrade.Code;
                }
                else if (emp.OrgUnit.JobGradeId.HasValue)
                {
                    model.GradeGroupId = emp.OrgUnit.JobGrade.GradeGroupId;
                    model.GradeGroupCode = emp.OrgUnit.JobGrade.GradeGroup.Code;
                    model.JobGradeId = emp.OrgUnit.JobGradeId;
                    model.JobGradeCode = emp.OrgUnit.JobGrade.Code;
                }
                if (emp.SalaryStepId.HasValue)
                {
                    model.SalaryStepId = emp.SalaryStepId;
                    model.SalaryStepCode = emp.SalaryStep.Code;
                }
            }
            else
            {
                model.PositionId = emp.Position.Id;
                model.PositionName = emp.Position.Name;
                if (emp.Position.StandardTitleTypeId.HasValue)
                {
                    model.StandardTitleCode = emp.Position.StandardTitleType.SysCode;
                    model.StandardTitleName = emp.Position.StandardTitleType.Name;
                }
                if (emp.JobGradeId.HasValue)
                {
                    model.GradeGroupId = emp.JobGrade.GradeGroupId;
                    model.GradeGroupCode = emp.JobGrade.GradeGroup.Code;
                    model.JobGradeId = emp.JobGradeId;
                    model.JobGradeCode = emp.JobGrade.Code;
                }
                else if (emp.Position.JobGradeId.HasValue)
                {
                    model.GradeGroupId = emp.Position.JobGrade.GradeGroupId;
                    model.GradeGroupCode = emp.Position.JobGrade.GradeGroup.Code;
                    model.JobGradeId = emp.Position.JobGradeId;
                    model.JobGradeCode = emp.Position.JobGrade.Code;
                }
                if (emp.SalaryStepId.HasValue)
                {
                    model.SalaryStepId = emp.SalaryStepId;
                    model.SalaryStepCode = emp.SalaryStep.Code;
                }
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
            model.IsPayrollExcluded = emp.IsPayrollExcluded;
            model.IsAttendRequired = emp.IsAttendRequired;
            model.IsOverTimeAllowed = emp.IsOverTimeAllowed;
            model.IsProbation = emp.IsProbation;
            ViewBag.bank = emp.Employee.BankId.HasValue ? _lookup.GetLookupItems<Bank>().SingleOrDefault(b => b.Id == emp.Employee.BankId).Name : null;
            return View("EmployeeIndex", model);
        }

        public async Task<IActionResult> EditPersonalData(long id)
        {
            var model = await _context.Employees.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.nationalitiesList = await _context.Nationalities.Where(b => b.IsActive).ToListAsync();
            ViewBag.banksList = _lookup.GetLookupItems<Bank>();
            return PartialView("_EditPersonalData", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(Employee item)
        {
            var model = await _context.Employees.Include(b => b.Governorate).Include(b => b.Nationality).SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            model.LastUpdated = DateTime.Now.Date;
            model.UpdatedBy = "user";

            await _context.SaveChangesAsync();
            return PartialView("_PersonalData", model);
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

        public async Task<IActionResult> EditFamilyMember(long id)
        {
            var model = await _context.EmployeeFamilies.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.familyMemberTypesList = _lookup.GetLookupItems<FamilyMemberType>();
            return PartialView("_EditFamilyMember", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFamilyMember(EmployeeFamily item)
        {
            var model = await _context.EmployeeFamilies.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("FamilyList", new { id = model.EmployeeId });
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

        public async Task<IActionResult> EditQualification(long id)
        {
            var model = _context.EmployeeQualifications.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.qualificationTypesList = _lookup.GetLookupItems<QualificationType>();
            ViewBag.competencyAreaTypesList = _lookup.GetLookupItems<CompetencyAreaType>();
            ViewBag.competencySubCategoriesList = await _context.CompetencySubCategories.Include(b => b.CompetencyCategory)
                                    .Where(b => b.IsActive).OrderBy(b => b.CompetencyCategory.SortOrder).ThenBy(b => b.SortOrder).ToListAsync();
            return PartialView("_EditQualification", await model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQualification(EmployeeQualification item)
        {
            var model = await _context.EmployeeQualifications.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("QualificationsList", new { id = model.EmployeeId });
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

        public async Task<IActionResult> EditLanguage(long id)
        {
            var model = _context.EmployeeLanguages.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.languageTypesList = _lookup.GetLookupItems<LanguageType>();
            return PartialView("_EditLanguage", await model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLanguage(EmployeeLanguage item)
        {
            var model = await _context.EmployeeLanguages.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("LanguagesList", new { id = model.EmployeeId });
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

        public async Task<IActionResult> EditDocument(long id)
        {
            var model = _context.EmployeeDocuments.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.documentTypesList = _lookup.GetLookupItems<DocumentType>();
            return PartialView("_EditDocument", await model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDocument(EmployeeDocument item)
        {
            var model = await _context.EmployeeDocuments.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("DocumentsList", new { id = model.EmployeeId });
        }

        //remuneration
        public async Task<IActionResult> Remuneration(long id)
        {
            var x = await _context.Employments.FirstOrDefaultAsync(b => b.EmployeeId == id && b.IsActive && !b.IsActing);
            
            if(x.JobGradeId.HasValue)
            {
                List<string> remunerationList = new List<string>();
                int basicSalary;
                if (x.SalaryScaleTypeId.HasValue && x.SalaryStepId.HasValue)
                    basicSalary = GetBasicSalary(x.SalaryScaleTypeId.Value, x.JobGradeId.Value, x.SalaryStepId.Value);
                else
                    basicSalary = x.BasicSalary;
                remunerationList = GetAllowances(x.JobGradeId.Value, basicSalary);
                ViewBag.basicSalary = basicSalary;
                ViewBag.remunerationList = remunerationList;
                ViewBag.isGoverned = true;
                return PartialView("_Remuneration");
            }
            else
            {
                var model = _context.Remunerations.Include(b => b.Employment).Include(b => b.AllowanceType)
                                    .Where(b => b.EmployeeId == id && b.IsActive).OrderBy(b => b.AllowanceType.SortOrder);
                ViewBag.basicSalary = x.BasicSalary;
                ViewBag.employmentId = x.Id;
                ViewBag.isGoverned = false;
                return PartialView("_Remuneration", await model.ToListAsync());
            }
        }

        public async Task<IActionResult> RemunerationHistory(long id)
        {
            var model = _context.Remunerations.Include(b => b.AllowanceType).Include(b => b.Employment).ThenInclude(b => b.OrgUnit)
                                .Include(b => b.Employment).ThenInclude(b => b.Position)
                                .Where(b => b.EmployeeId == id).OrderByDescending(b => b.FromDate);
            return PartialView("_RemunerationHistory", await model.ToListAsync());
        }

        public IActionResult AddAllowance()
        {
            ViewBag.allowanceTypesList = _lookup.GetLookupItems<AllowanceType>();
            return PartialView("_AddAllowance");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAllowance(Remuneration item)
        {
            var emp = await _context.Employments.SingleOrDefaultAsync(b => b.Id == item.EmploymentId);
            item.BasicSalary = emp.BasicSalary;
            item.LastUpdated = DateTime.Now.Date;
            item.UpdatedBy = "user";
            await _context.Remunerations.AddAsync(item);

            //deactivate same allowance if any
            var x = await _context.Remunerations.FirstOrDefaultAsync(b => b.EmployeeId == item.EmployeeId && b.EmploymentId == item.EmploymentId && b.AllowanceTypeId == item.AllowanceTypeId);
            if (x != null)
            {
                x.IsActive = false;
                x.LastUpdated = DateTime.Now.Date;
                x.UpdatedBy = "user";
            }
                
            await _context.SaveChangesAsync();
            return RedirectToAction("Remuneration", new { id = item.EmployeeId });
        }

        //career
        public async Task<IActionResult> EmploymentsList(long id)
        {
            var model = _context.Employments.Include(b => b.OrgUnit).Include(b => b.Position).Include(b => b.EmploymentType)
                                .Include(b => b.SalaryScaleType).Include(b => b.JobGrade).Include(b => b.SalaryStep)
                                .Where(b => b.EmployeeId == id && b.IsActive);
            return PartialView("_EmploymentsList", await model.ToListAsync());
        }

        public async Task<IActionResult> CareerHistory(long id)
        {
            var model = _context.EmployeePromotions.Include(b => b.PromotionType).Include(b => b.Employment).ThenInclude(b => b.OrgUnit)
                                            .Include(b => b.Employment).ThenInclude(b => b.Position).Include(b => b.JobGrade)
                                            .Where(b => b.Employment.EmployeeId == id && !b.IsCancelled && b.IsApproved && b.PromotionType.SysCode != "SAL_INCR").OrderByDescending(b => b.EffectiveFromDate);
            return PartialView("_CareerHistory", await model.ToListAsync());
        }

        public async Task<IActionResult> AddEmployment(long id)
        {
            //id is the selected-current employment id
            ViewBag.employeeId = _context.Employments.SingleOrDefault(b => b.Id == id).EmployeeId;
            ViewBag.orgUnitsList = await _context.OrgUnits.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryScaleTypesList = _lookup.GetLookupItems<SalaryScaleType>();
            ViewBag.employmentTypesList = _lookup.GetLookupItems<EmploymentType>();
            ViewBag.positionsList = await _context.Positions.Where(b => b.TotalVacant != 0).ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).ToListAsync();
            return PartialView("_AddEmployment");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployment(Employment item, long oldEmploymentId)
        {
            item.IsActive = true;
            _context.Employments.Add(item);

            //deactivate old employment
            _context.Employments.SingleOrDefault(b => b.Id == oldEmploymentId).IsActive = false;

            //add transfer type promotion
            EmployeePromotion promo = new EmployeePromotion();
            promo.EmploymentId = item.Id;
            promo.PromotionTypeId = _lookup.GetLookupItems<PromotionType>().SingleOrDefault(b => b.SysCode == "TRANS").Id;
            promo.BasicSalary = item.BasicSalary;
            promo.CreatedDate = DateTime.Now.Date;
            promo.Details = item.Details;
            promo.EffectiveFromDate = item.FromDate;
            promo.IsActive = true;
            promo.IsApproved = false;
            promo.JobGradeId = item.JobGradeId;
            promo.LastUpdated = DateTime.Now.Date;
            promo.SalaryStepId = item.SalaryStepId;
            promo.UpdatedBy = "user";
            _context.EmployeePromotions.Add(promo);

            await _context.SaveChangesAsync();
            return RedirectToAction("EmploymentsList", new { id = item.EmployeeId });
        }

        public async Task<IActionResult> EditEmployment(long id)
        {
            var model = await _context.Employments.SingleOrDefaultAsync(b => b.Id == id);
            ViewBag.salaryScaleTypesList = _lookup.GetLookupItems<SalaryScaleType>();
            ViewBag.employmentTypesList = _lookup.GetLookupItems<EmploymentType>();
            ViewBag.positionsList = await _context.Positions.Where(b => b.OrgUnitId == model.OrgUnitId).ToListAsync();
            ViewBag.jobGradesList = await _context.JobGrades.Where(b => b.IsActive).ToListAsync();
            ViewBag.salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).ToListAsync();
            return PartialView("_EditEmployment", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployment(Employment item)
        {
            var model = await _context.Employments.SingleOrDefaultAsync(b => b.Id == item.Id);
            await TryUpdateModelAsync(model);

            //update promotion if employment still in initial state
            int cnt = await _context.EmployeePromotions.Where(b => b.EmploymentId == item.Id).CountAsync();
            if(cnt == 1)
            {
                var promo = await _context.EmployeePromotions.SingleOrDefaultAsync(b => b.EmploymentId == item.Id);
                promo.JobGradeId = item.JobGradeId;
                promo.SalaryStepId = item.SalaryStepId;
                promo.BasicSalary = item.BasicSalary;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("EmploymentsList", new { id = model.EmployeeId });
        }

        public async Task<IActionResult> PromotionsList(long id)
        {
            var model = _context.EmployeePromotions.Include(b => b.PromotionType).Include(b => b.Employment).ThenInclude(b => b.OrgUnit)
                                            .Include(b => b.Employment).ThenInclude(b => b.Position)
                                            .Include(b => b.JobGrade).Include(b => b.SalaryStep)
                                            .Where(b => b.Employment.EmployeeId == id && !b.IsCancelled && !b.IsApproved).OrderByDescending(b => b.EffectiveFromDate);
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
            item.IsApproved = false;
            _context.EmployeePromotions.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("PromotionsList", new { id = model.EmployeeId });
        }

        public async Task<IActionResult> ApprovePromotion(long id)
        {
            var model = await _context.EmployeePromotions.Include(b => b.Employment).SingleOrDefaultAsync(b => b.Id == id);
            model.IsApproved = true;
            
            //update employment by the approved promotion
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

        [HttpPost]
        public async Task<IActionResult> FileUpload(ICollection<IFormFile> files, long id, int docTypeId)
        {
            //id is the employee document id
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName;
                    string docType = _lookup.GetLookupItems<DocumentType>().SingleOrDefault(b => b.Id == docTypeId).SysCode;
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var uploadsFolder = Path.Combine(_environment.ContentRootPath, @"wwwroot\files\Personnel");
                            DirectoryInfo di = new DirectoryInfo(Path.GetFullPath(uploadsFolder));
                            fileName = docType + "__" + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploadsFolder, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            //update document with file name
                            _context.EmployeeDocuments.SingleOrDefault(b => b.Id == id).Url = fileName;
                            await _context.SaveChangesAsync();
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