using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeeLeave
    {
        public long Id { get; set; }
        public string AppForm { get; set; }
        public long EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ThruDate { get; set; }
        public DateTime? ActualFromDate { get; set; }
        public DateTime? ActualThruDate { get; set; }
        public string Details { get; set; }
        public bool IsApproved { get; set; }
        public bool IsEndorsed { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}
