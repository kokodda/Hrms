using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class CompanyAccount
    {
        public long Id { get; set; }
        public long OrgUnitId { get; set; }
        public string AccountId { get; set; }

        public virtual OrgUnit OrgUnit { get; set; }
        public virtual ICollection<CompanyGroupAccount> CompanyGroupAccounts { get; set; }
    }
}
