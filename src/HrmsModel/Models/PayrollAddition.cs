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
        public int GradeGroupId { get; set; }
        public int JobGradeId { get; set; }

        public virtual Payroll Payroll { get; set; }
        public virtual PayrollComponentType PayrollComponentType { get; set; }
    }
}
