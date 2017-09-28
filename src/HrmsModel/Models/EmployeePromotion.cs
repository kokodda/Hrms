using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeePromotion
    {
        public long Id { get; set; }
        public int PromotionTypeId { get; set; }
        public long EmploymentId { get; set; }
        public int? JobGradeId { get; set; }
        public int? SalaryStepId { get; set; }
        public int? SalaryScaleTypeId { get; set; }
        public int BasicSalary { get; set; }
        public int SalaryIncreaseValue { get; set; }
        public bool IsIncreasePercentage { get; set; }
        public DateTime EffectiveFromDate { get; set; }
        public string Details { get; set; }
        public string Remarks { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Employment Employment { get; set; }
        public virtual SalaryScaleType SalaryScaleType { get; set; }
        public virtual SalaryStep SalaryStep { get; set; }
        public virtual JobGrade JobGrade { get; set; }
        public virtual PromotionType PromotionType { get; set; }
    }
}
