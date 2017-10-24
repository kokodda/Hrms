using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Candidate
    {
        public long Id { get; set; }
        public string AppForm { get; set; }
        public long OrgUnitId { get; set; }
        public long? PositionId { get; set; }
        public bool IsHead { get; set; }
        public string JobName { get; set; }
        public string OthJobName { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string OthFirstName { get; set; }
        public string OthFamilyName { get; set; }
        public string OthFatherName { get; set; }
        public string OthMotherName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsMale { get; set; }
        public int NationalityId { get; set; }
        public string MaritalStatus { get; set; }
        public bool IsMilitaryExempted { get; set; }
        public string Phone { get; set; }
        public string HomePhone1 { get; set; }
        public string HomePhone2 { get; set; }
        public string Email { get; set; }
        public int GovernorateId { get; set; }
        public string Address { get; set; }
        public string PermenantAddress { get; set; }
        public int? FinalScore { get; set; }
        public bool IsSubmitted { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual OrgUnit OrgUnit { get; set; }
        public virtual Position Position { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Governorate Governorate { get; set; }
    }
}
