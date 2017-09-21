using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeeSalary
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int TotalSalary { get; set; }
        public DateTime FromDate { get; set; }
        public string Details { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
