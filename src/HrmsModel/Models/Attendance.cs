using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Attendance
    {
        public long Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public bool IsFullMonth { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
        public bool IsEndorsed { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
        public virtual ICollection<Payroll> Payrolls { get; set; }
    }
}
