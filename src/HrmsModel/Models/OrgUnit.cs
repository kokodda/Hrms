using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class OrgUnit
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public int OrgUnitTypeId { get; set; }
        public int SortOrder { get; set; }
        public string HeadPositionName { get; set; }
        public string OthHeadPositionName { get; set; }
        public decimal? JobWeight { get; set; }
        public int? StandardTitleTypeId { get; set; }
        public int? JobGradeId { get; set; }
        public bool IsAttendRequired { get; set; }
        public bool IsOverTimeAllowed { get; set; }
        public long? LineManagerOrgUnitId { get; set; }
        public long? ReportingToOrgUnitId { get; set; }
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public bool IsVacant { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual OrgUnitType OrgUnitType { get; set; }
        public virtual JobGrade JobGrade { get; set; }
        public virtual StandardTitleType StandardTitleType { get; set; }
        public virtual ICollection<Employment> Employments { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
    }
}
