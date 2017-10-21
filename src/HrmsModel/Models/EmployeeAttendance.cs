using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeeAttendance
    {
        public long Id { get; set; }
        public long AttendanceId { get; set; }
        public long EmployeeId { get; set; }
        public int PayrollComponentTypeId { get; set; }
        public int RequiredMinutes { get; set; }
        public int TotalMinutes { get; set; }
        public int CompliancePercentage { get; set; }
        public int FactorValue { get; set; }
        public int AttendanceActionTypeId { get; set; }
        public int NbrDays { get; set; }

        public virtual Attendance Attendance { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual PayrollComponentType PayrollComponentType { get; set; }
        public virtual AttendanceActionType AttendanceActionType { get; set; }
    }
}
