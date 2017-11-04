using HrmsModel.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsApp.Utils
{
    public static class GeneralClasses
    {
        //generic functions
        public static int GetBasicSalary(int salaryScaleTypeId, int jobGradeId, int salaryStepId, HrmsDbContext _context)
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

        public static List<string> GetAllowances(int jobGradeId, int basicSalary, HrmsDbContext _context)
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

        public static int GetTotalAllowances(int jobGradeId, int basicSalary, HrmsDbContext _context)
        {
            int totalAllowances = 0;
            List<int> Ids = new List<int>();
            var allowancesList = _context.AllowancePolicies.Include(b => b.AllowanceType).Where(b => b.IsActive).ToList();
            //grade allowances
            var gradeAllowances = allowancesList.Where(b => b.JobGradeId == jobGradeId);
            Ids.AddRange(gradeAllowances.Select(b => b.AllowanceTypeId));
            foreach (var a in gradeAllowances)
            {
                if (a.isPercentage)
                    totalAllowances += (basicSalary * a.Amount / 100);
                else
                    totalAllowances += (basicSalary + a.Amount);
            }
            int gradeGroupId = _context.JobGrades.SingleOrDefault(b => b.Id == jobGradeId).GradeGroupId;
            var groupAllowances = allowancesList.Where(b => b.GradeGroupId == gradeGroupId && !Ids.Contains(b.AllowanceTypeId));
            Ids.AddRange(groupAllowances.Select(b => b.AllowanceTypeId));
            foreach (var a in groupAllowances)
            {
                if (a.isPercentage)
                    totalAllowances += (basicSalary * a.Amount / 100);
                else
                    totalAllowances += (basicSalary + a.Amount);
            }
            var companyAllowances = allowancesList.Where(b => b.IsCompanyPolicy && !Ids.Contains(b.AllowanceTypeId));
            foreach (var a in companyAllowances)
            {
                if (a.isPercentage)
                    totalAllowances += (basicSalary * a.Amount / 100);
                else
                    totalAllowances += (basicSalary + a.Amount);
            }
            return totalAllowances;
        }
    }
}
