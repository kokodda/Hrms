using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class CompetencySubCategory
    {
        public int Id { get; set; }
        public int CompetencyCategoryId { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual CompetencyCategory CompetencyCategory { get; set; }
        public virtual ICollection<Competency> Competencies { get; set; }
        public virtual ICollection<EmployeeQualification> EmployeeQualifications { get; set; }
    }
}
