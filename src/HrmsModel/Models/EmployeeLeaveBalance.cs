using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeeLeaveBalance
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int AnnualEntitlement { get; set; }
        public int BalanceDays { get; set; }
        public int BalanceHours { get; set; }
        public int CarryForward { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}
