using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeePosition
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public long EmployeeId { get; set; }
        public long? OrgUnitId { get; set; }
        public int EmployeeTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
        public bool IsActing { get; set; }
        public int? SalaryStepId { get; set; }
        public long? SalaryScaleTypeId { get; set; }
        public bool IsAttendRequired { get; set; }
        public bool IsOverTimeAllowed { get; set; }
        public int TotalSalary { get; set; }
        public string Details { get; set; }
        public string Remarks { get; set; }
        public bool IsProbation { get; set; }
        public bool IsActive { get; set; }

        public virtual OrgUnit OrgUnit { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public virtual SalaryScale SalaryScale { get; set; }
        public virtual SalaryStep SalaryStep { get; set; }
    }
}
