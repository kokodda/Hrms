using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeeGroup
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int GenericSubGroupId { get; set; }
        public DateTime JoinDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual GenericSubGroup GenericSubGroup { get; set; }
    }
}
