using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class GradeGroup
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<JobGrade> JobGrades { get; set; }
        public virtual ICollection<Competency> Competencies { get; set; }
        public virtual ICollection<LeavePolicy> LeavePolicies { get; set; }
        public virtual ICollection<AllowancePolicy> AllowancePolicies { get; set; }
        public virtual ICollection<PayrollAddition> PayrollAdditions { get; set; }
    }
}
