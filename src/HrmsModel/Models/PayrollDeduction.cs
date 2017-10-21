using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class PayrollDeduction
    {
        public long Id { get; set; }
        public long PayrollId { get; set; }
        public long EmployeeId { get; set; }
        public int PayrollComponentTypeId { get; set; }
        public int NbrDays { get; set; }
        public int Value { get; set; }
        public bool IsPercentage { get; set; }
        public string Narration { get; set; }

        public virtual Payroll Payroll { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual PayrollComponentType PayrollComponentType { get; set; }
    }
}
