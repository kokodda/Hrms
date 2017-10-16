using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeeAttendance
    {
        public long Id { get; set; }
        public long? PayrollId { get; set; }
        public long EmployeeId { get; set; }
        public int PayrollComponentTypeId { get; set; }
        public int RequiredMinutes { get; set; }
        public int TotalMinutes { get; set; }
        public int FactorValue { get; set; }

        public virtual Payroll Payroll { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual PayrollComponentType PayrollComponentType { get; set; }
    }
}
