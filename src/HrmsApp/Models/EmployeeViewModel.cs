using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrmsModel.Models;

namespace HrmsApp.Models
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public string EmployeeName { get; set; }
        public string OthEmployeeName { get; set; }
        public long OrgUnitId { get; set; }
        public string OrgUnitName { get; set; }
        public string OrgUnitTypeName { get; set; }
        public string OrgUnitTypeCode { get; set; }
        public long EmploymentId { get; set; }
        public bool IsHead { get; set; }
        public bool IsActing { get; set; }
        public string EmploymentTypeCode { get; set; }
        public string EmploymentTypeName { get; set; }
        public DateTime DateInPosition { get; set; }
        public long? PositionId { get; set; }
        public string PositionName { get; set; }
        public string StandardTitleCode { get; set; }
        public string StandardTitleName { get; set; }
        public int? GradeGroupId { get; set; }
        public string GradeGroupCode { get; set; }
        public int? JobGradeId { get; set; }
        public string JobGradeCode { get; set; }
        public int? SalaryStepId { get; set; }
        public string SalaryStepCode { get; set; }
        public int? SalaryScaleTypeId { get; set; }
        public string SalaryScaleTypeName { get; set; }
        public int BasicSalary { get; set; }
        public bool IsAttendRequired { get; set; }
        public bool IsOverTimeAllowed { get; set; }
    }
}
