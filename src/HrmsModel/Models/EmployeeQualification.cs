using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeeQualification
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int QualificationTypeId { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public int CompetencyAreaTypeId { get; set; }
        public int? CompetencySubCategoryId { get; set; }
        public DateTime? ObtainedDate { get; set; }
        public int? TotalYears { get; set; }
        public bool IsPlanned { get; set; }

        public virtual QualificationType QualificationType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual CompetencyAreaType CompetencyAreaType { get; set; }
        public virtual CompetencySubCategory CompetencySubCategory { get; set; }
    }
}
