using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Remuneration
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long EmploymentId { get; set; }
        public int BasicSalary { get; set; }
        public int? AllowanceTypeId { get; set; }
        public int AllowanceValue { get; set; }
        public bool IsPercentage { get; set; }
        public DateTime FromDate { get; set; }
        public string Narration { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employment Employment { get; set; }
        public virtual AllowanceType AllowanceType { get; set; }
    }
}
