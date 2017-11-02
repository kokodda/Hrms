using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class CompanyGroupAccount
    {
        public long Id { get; set; }
        public long CompanyAccountId { get; set; }
        public long CompanyGroupId { get; set; }

        public virtual CompanyAccount CompanyAccount { get; set; }
        public virtual CompanyGroup CompanyGroup { get; set; }
    }
}
