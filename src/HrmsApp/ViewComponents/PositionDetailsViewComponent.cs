using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HrmsApp.Models;
using HrmsModel.Models;
using HrmsModel.Data;
using Microsoft.AspNetCore.Mvc;
using HrmsApp.Utils;

namespace HrmsApp.ViewComponents
{
    public class PositionDetailsViewComponent : ViewComponent
    {
        private readonly HrmsDbContext _context;
        public PositionDetailsViewComponent(HrmsDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id, bool isHead)
        {
            List<string> model = new List<string>();
            if (isHead)
            {
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
                string budget;
                if (ou.JobGradeId.HasValue)
                {
                    var scale = await _context.SalaryScales.Include(b => b.SalaryScaleType).FirstOrDefaultAsync(b => b.SalaryScaleType.SysCode == "COMPAT");
                    if (scale != null)
                    {
                        var salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).OrderBy(b => b.SortOrder).ToListAsync();
                        int minSalary = GeneralClasses.GetBasicSalary(scale.SalaryScaleType.Id, ou.JobGradeId.Value, salaryStepsList.First().Id, _context);
                        minSalary += GeneralClasses.GetTotalAllowances(ou.JobGradeId.Value, minSalary, _context);
                        int maxSalary = GeneralClasses.GetBasicSalary(scale.SalaryScaleType.Id, ou.JobGradeId.Value, salaryStepsList.Last().Id, _context);
                        maxSalary += GeneralClasses.GetTotalAllowances(ou.JobGradeId.Value, maxSalary, _context);
                        budget = "Budget|" + minSalary.ToString() + " - " + maxSalary.ToString() + " " + scale.CurrencyCode;
                    }
                    else
                        budget = "Budget|Cannot be Estimated - Salary Scale is not defined.";
                }
                else
                    budget = "Budget|Cannot be Estimated - Position is not Graded.";
                model.Add(budget);
                model.Add("Created On|" + ou.CreatedDate.ToString("dd-MM-yyyy"));
                model.Add("Last Updated|" + ou.LastUpdated.ToString("dd-MM-yyyy"));
                model.Add("Updated By|" + ou.UpdatedBy);

                ViewBag.positionName = ou.HeadPositionName;
            }
            else
            {
                var pos = await _context.Positions.Include(b => b.OrgUnit).SingleOrDefaultAsync(b => b.Id == id);

                string reportingTo = pos.OrgUnit.HeadPositionName;
                string lineManager = "NA";
                if (pos.ReportingToOrgUnitId.HasValue)
                    reportingTo = _context.OrgUnits.SingleOrDefault(b => b.Id == pos.ReportingToOrgUnitId).HeadPositionName;
                if (pos.LineManagerOrgUnitId.HasValue)
                    lineManager = _context.OrgUnits.SingleOrDefault(b => b.Id == pos.LineManagerOrgUnitId).HeadPositionName;
                model.Add("Reporting To|" + reportingTo);
                model.Add("Line Manager|" + lineManager);
                model.Add("Job Weight|" + (pos.JobWeight.HasValue ? pos.JobWeight.Value.ToString() : "NG"));
                model.Add("Attendance|" + (pos.IsAttendRequired ? "Required" : "Not Required"));
                model.Add("Overtime|" + (pos.IsOverTimeAllowed ? "Allowed" : "Not Allowed"));
                string budget;
                if (pos.JobGradeId.HasValue)
                {
                    var scale = await _context.SalaryScales.Include(b => b.SalaryScaleType).FirstOrDefaultAsync(b => b.SalaryScaleType.SysCode == "COMPAT");
                    if (scale != null)
                    {
                        var salaryStepsList = await _context.SalarySteps.Where(b => b.IsActive).OrderBy(b => b.SortOrder).ToListAsync();
                        int minSalary = GeneralClasses.GetBasicSalary(scale.SalaryScaleType.Id, pos.JobGradeId.Value, salaryStepsList.First().Id, _context);
                        minSalary += GeneralClasses.GetTotalAllowances(pos.JobGradeId.Value, minSalary, _context);
                        int maxSalary = GeneralClasses.GetBasicSalary(scale.SalaryScaleType.Id, pos.JobGradeId.Value, salaryStepsList.Last().Id, _context);
                        maxSalary += GeneralClasses.GetTotalAllowances(pos.JobGradeId.Value, maxSalary, _context);
                        maxSalary += GeneralClasses.GetTotalAllowances(pos.JobGradeId.Value, maxSalary, _context);
                        budget = "Budget|" + minSalary.ToString() + " - " + maxSalary.ToString() + " " + scale.CurrencyCode;
                    }
                    else
                        budget = "Budget|Cannot be Estimated - Salary Scale is not defined.";
                }
                else
                    budget = "Budget|Cannot be Estimated - Position is not Graded.";
                model.Add(budget);
                model.Add("Created On|" + pos.CreatedDate.ToString("dd-MM-yyyy"));
                model.Add("Last Updated|" + pos.LastUpdated.ToString("dd-MM-yyyy"));
                model.Add("Updated By|" + pos.UpdatedBy);

                ViewBag.positionName = pos.Name;
            }
            return View(model);
        }
    }
}
