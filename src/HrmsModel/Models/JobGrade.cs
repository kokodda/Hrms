using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class JobGrade
    {
        public int Id { get; set; }
        public int GradeGroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public int SortOrder { get; set; }
        public int SalaryIncrPctg { get; set; }
        public bool IsActive { get; set; }

        public virtual GradeGroup GradeGroup { get; set; }
        public virtual ICollection<OrgUnit> OrgUnits { get; set; }
        public virtual ICollection<Competency> Competencies { get; set; }
        public virtual ICollection<LeavePolicy> LeavePolicies { get; set; }
        public virtual ICollection<AllowancePolicy> AllowancePolicies { get; set; }
        public virtual ICollection<SalaryScale> FromGradeSalaryScales { get; set; }
        public virtual ICollection<SalaryScale> ThruGradeSalaryScales { get; set; }
        public virtual ICollection<Employment> Employments { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }
    }
}
