using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsApp.Models
{
    public class EmployeeAttendanceViewModel
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int RequiredMinutes { get; set; }
        public int AttendanceMinutes { get; set; }
        public int Overtime1Minutes { get; set; }
        public int Overtime2Minutes { get; set; }
        public int WeekendMinutes { get; set; }
        public int CompliancePercentage { get; set; }
        public int AttendanceActionTypeId { get; set; }
        public string AttendanceActionTypeName { get; set; }
    }
}
