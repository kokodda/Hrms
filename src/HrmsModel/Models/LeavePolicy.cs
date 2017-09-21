using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class LeavePolicy
    {
        public long Id { get; set; }
        public int LeaveTypeId { get; set; }
        public int? GradeGroupId { get; set; }
        public int? JobGradeId { get; set; }
        public bool IsCompanyPolicy { get; set; }
        public int TotalDays { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual LeaveType LeaveType { get; set; }
        public virtual GradeGroup GradeGroup { get; set; }
        public virtual JobGrade JobGrade { get; set; }
    }
}
