using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class CompanyGroupMember
    {
        public long Id { get; set; }
        public long CompanyGroupId { get; set; }
        public long CompanyId { get; set; }
        public int SortOrder { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompanyGroup CompanyGroup { get; set; }
    }
}
