using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string LegalTypeCode { get; set; }
        public string ShortName { get; set; }
        public string OthShortName { get; set; }
        public byte[] Logo { get; set; }

        public virtual OrgUnit OrgUnit { get; set; }
        public virtual ICollection<CompanyGroupMember> CompanyGroupMembers { get; set; }
        public virtual ICollection<SalaryScale> SalaryScales { get; set; }
    }
}
