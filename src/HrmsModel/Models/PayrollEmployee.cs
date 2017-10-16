using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class PayrollEmployee
    {
        public long Id { get; set; }
        public long PayrollId { get; set; }
        public long EmployeeId { get; set; }
        public long EmploymentId { get; set; }
        public int PayrollComponentTypeId { get; set; }
        public int Amount { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ThruDate { get; set; }

        public virtual Payroll Payroll { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employment Employment { get; set; }
        public virtual PayrollComponentType PayrollComponentType { get; set; }
    }
}
