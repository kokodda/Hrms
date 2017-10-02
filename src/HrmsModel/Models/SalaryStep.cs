using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class SalaryStep
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public int SortOrder { get; set; }
        public int SalaryIncrPctg { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Employment> Employments { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<OrgUnit> OrgUnits { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }
    }
}
