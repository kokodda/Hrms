using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class LeaveType : ILookup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public string SysCode { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<LeavePolicy> LeavePolicies { get; set; }
        public virtual ICollection<EmployeeLeave> EmployeeLeaves { get; set; }
        public virtual ICollection<EmployeeLeaveBalance> EmployeeLeaveBalances { get; set; }
    }
}
