using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Position
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public long OrgUnitId { get; set; }
        public long? ReportingToOrgUnitId { get; set; }
        public long? LineManagerOrgUnitId { get; set; }
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public decimal? JobWeight { get; set; }
        public int? StandardTitleTypeId { get; set; }
        public int? JobGradeId { get; set; }
        public int? SalaryStepId { get; set; }
        public bool IsAttendRequired { get; set; }
        public bool IsOverTimeAllowed { get; set; }
        public int Capacity { get; set; }
        public int TotalVacant { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual OrgUnit OrgUnit { get; set; }
        public virtual SalaryStep SalaryStep { get; set; }
        public virtual JobGrade JobGrade { get; set; }
        public virtual StandardTitleType StandardTitleType { get; set; }
        public virtual ICollection<Employment> Employments { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
