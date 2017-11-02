using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class SalaryScale
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public int SalaryScaleTypeId { get; set; }
        public long? CompanyId { get; set; }
        public string Description { get; set; }
        public int FromJobGradeId { get; set; }
        public int ThruJobGradeId { get; set; }
        public int MinSalary { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual Company Company { get; set; }
        public virtual SalaryScaleType SalaryScaleType { get; set; }
        public virtual JobGrade FromJobGrade { get; set; }
        public virtual JobGrade ThruJobGrade { get; set; }
        public virtual ICollection<Payroll> Payrolls { get; set; }
    }
}
