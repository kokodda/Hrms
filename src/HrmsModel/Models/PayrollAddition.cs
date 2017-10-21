using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class PayrollAddition
    {
        public long Id { get; set; }
        public long PayrollId { get; set; }
        public int PayrollComponentTypeId { get; set; }
        public int FactorPercent { get; set; }
        public bool IsCompanyLevel { get; set; }
        public int? GradeGroupId { get; set; }
        public int? FromJobGradeId { get; set; }
        public int? ThruJobGradeId { get; set; }
        public long? EmployeeId { get; set; }
        public string Narration { get; set; }

        public virtual Payroll Payroll { get; set; }
        public virtual PayrollComponentType PayrollComponentType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual GradeGroup GradeGroup { get; set; }
        public virtual JobGrade FromJobGrade { get; set; }
        public virtual JobGrade ThruJobGrade { get; set; }
    }
}
