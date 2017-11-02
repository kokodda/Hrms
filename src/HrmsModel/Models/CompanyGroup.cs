using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class CompanyGroup
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public long? MotherOrgUnitId { get; set; }
        public byte[] Logo { get; set; }
        public bool IsActive { get; set; }

        public virtual OrgUnit OrgUnit { get; set; }
        public virtual ICollection<CompanyGroupMember> CompanyGroupMembers { get; set; }
        public virtual ICollection<CompanyGroupAccount> CompanyGroupAccounts { get; set; }
    }
}
