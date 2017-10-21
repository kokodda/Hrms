using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class PayrollComponentType : ILookup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public string SysCode { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<PayrollEmployee> PayrollEmployees { get; set; }
        public virtual ICollection<PayrollAddition> PayrollAdditions { get; set; }
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
        public virtual ICollection<PayrollDeduction> PayrollDeductions { get; set; }
    }
}
