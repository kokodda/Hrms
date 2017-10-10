using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HrmsApp.Models;
using HrmsModel.Models;
using HrmsModel.Data;
using Microsoft.AspNetCore.Mvc;

namespace HrmsApp.ViewComponents
{
    public class EmployeesListViewComponent : ViewComponent
    {
        private readonly HrmsDbContext _context;
        public EmployeesListViewComponent(HrmsDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string orgUnitTypeCode, string employeeUid, string employeeName, int? jobGradeId, int? gradeGroupId, int? titleId, int? governorateId = null, string assigned = "all", bool isHead = false, bool isActive = true, bool isProbation = false, bool isActing = false, bool isBirthday = false, long? orgUnitId = null, int? nationalityId = null)
        {
            List<EmployeeViewModel> model = new List<EmployeeViewModel>();
            var empList = await _context.Employments.Include(b => b.EmploymentType).Include(b => b.Employee).Include(b => b.Position)
                                        .Include(b => b.OrgUnit).ThenInclude(b => b.OrgUnitType)
                                        .Include(b => b.JobGrade).Include(b => b.SalaryStep).Include(b => b.SalaryScaleType)
                                        .Where(b => b.IsActive == isActive
                                            && (string.IsNullOrEmpty(orgUnitTypeCode) || b.OrgUnit.OrgUnitType.SysCode == orgUnitTypeCode)
                                            && (!orgUnitId.HasValue || b.OrgUnitId == orgUnitId)
                                            && (!nationalityId.HasValue || b.Employee.NationalityId == nationalityId)
                                            && (!governorateId.HasValue || b.Employee.GovernorateId == governorateId)
                                            && (!gradeGroupId.HasValue || b.JobGrade.GradeGroupId == gradeGroupId)
                                            && (!jobGradeId.HasValue || b.JobGrade.Id == jobGradeId)
                                            && (!titleId.HasValue || b.PositionId.HasValue || b.IsHead)
                                            && (!isHead || b.IsHead) && (!isProbation || b.IsProbation) && (!isActing || b.IsActing)
                                            && (assigned == "all" || (assigned == "assigned" && (b.PositionId.HasValue || b.IsHead)) || (assigned == "unassigned" && (!b.PositionId.HasValue && !b.IsHead)))
                                            && (!isBirthday || (b.Employee.BirthDate.DayOfYear == DateTime.Today.Date.DayOfYear))
                                            && (string.IsNullOrEmpty(employeeName) || b.Employee.FirstName.Contains(employeeName) || b.Employee.FamilyName.Contains(employeeName) || b.Employee.OthFirstName.Contains(employeeName) || b.Employee.OthFamilyName.Contains(employeeName))
                                            && (string.IsNullOrEmpty(employeeUid) || b.Employee.EmpUid == employeeUid)
                                            )
                                        .OrderBy(b => b.OrgUnit.OrgUnitType.SortOrder)
                                        .ToListAsync();
            var gradeGroupsList = await _context.GradeGroups.Select(b => new { b.Id, b.Code }).ToListAsync();
            var jobGradesList = await _context.JobGrades.Select(b => new { b.Id, b.Code }).ToListAsync();
            var titlesList = await _context.StandardTitleTypes.Select(b => new { b.Id, b.Name, b.SysCode }).ToListAsync();

            if (titleId.HasValue)
            {
                var empList1 = empList.Where(b => b.Position != null).ToList();
                var empList2 = empList.Where(b => b.IsHead).ToList();
                empList.Clear();
                empList.AddRange(empList1.Where(b => b.Position.StandardTitleTypeId == titleId));
                empList.AddRange(empList2.Where(b => b.OrgUnit.StandardTitleTypeId == titleId));
            }

            foreach (var emp in empList)
            {
                EmployeeViewModel item = new EmployeeViewModel();
                item.Employee = emp.Employee;
                item.EmployeeName = emp.Employee.FirstName + " " + emp.Employee.FatherName + " " + emp.Employee.FamilyName;
                item.OthEmployeeName = emp.Employee.OthFirstName + " " + emp.Employee.OthFatherName + " " + emp.Employee.OthFamilyName;
                item.OrgUnitId = emp.OrgUnitId;
                item.OrgUnitName = emp.OrgUnit.Name;
                item.OrgUnitTypeCode = emp.OrgUnit.OrgUnitType.SysCode;
                item.OrgUnitTypeName = emp.OrgUnit.OrgUnitType.Name;
                item.EmploymentId = emp.Id;
                item.IsHead = emp.IsHead;
                item.DateInPosition = emp.FromDate;
                item.EmploymentTypeCode = emp.EmploymentType.SysCode;
                item.EmploymentTypeName = emp.EmploymentType.Name;
                item.JobGradeId = emp.JobGradeId;
                if (emp.JobGradeId.HasValue)
                {
                    item.JobGradeCode = jobGradesList.Find(b => b.Id == emp.JobGradeId.Value).Code;
                    item.GradeGroupId = emp.JobGrade.GradeGroupId;
                    item.GradeGroupCode = gradeGroupsList.Find(b => b.Id == emp.JobGrade.GradeGroupId).Code;
                }
                item.SalaryStepId = emp.SalaryStepId;
                if (emp.SalaryStepId.HasValue)
                    item.SalaryStepCode = emp.SalaryStep.Code;
                item.SalaryScaleTypeId = emp.SalaryScaleTypeId;
                if (emp.SalaryScaleTypeId.HasValue)
                    item.SalaryScaleTypeName = emp.SalaryScaleType.Name;
                if (emp.IsHead)
                {
                    item.PositionId = null;
                    item.PositionName = emp.OrgUnit.HeadPositionName;
                    if (emp.OrgUnit.StandardTitleTypeId.HasValue)
                    {
                        item.StandardTitleCode = titlesList.Find(b => b.Id == emp.OrgUnit.StandardTitleTypeId).SysCode;
                        item.StandardTitleName = titlesList.Find(b => b.Id == emp.OrgUnit.StandardTitleTypeId).Name;
                    }
                }
                else if (!emp.IsHead && emp.PositionId.HasValue)
                {
                    item.PositionId = emp.Position.Id;
                    item.PositionName = emp.Position.Name;
                    if (emp.Position.StandardTitleTypeId.HasValue)
                    {
                        item.StandardTitleCode = titlesList.Find(b => b.Id == emp.Position.StandardTitleTypeId).SysCode;
                        item.StandardTitleName = titlesList.Find(b => b.Id == emp.Position.StandardTitleTypeId).Name;
                    }
                }
                else
                {
                    item.PositionId = null;
                    item.PositionName = emp.JobName;
                }
                if (emp.SalaryScaleTypeId.HasValue)
                {
                    item.SalaryScaleTypeId = emp.SalaryScaleTypeId;
                    item.SalaryScaleTypeName = emp.SalaryScaleType.Name;
                    item.BasicSalary = 0;
                }
                else
                    item.BasicSalary = emp.BasicSalary;
                item.IsActing = emp.IsActing;
                item.IsAttendRequired = emp.IsAttendRequired;
                item.IsOverTimeAllowed = emp.IsOverTimeAllowed;
                item.IsProbation = emp.IsProbation;
                model.Add(item);
            }
            return View(model);
        }

    }
}
