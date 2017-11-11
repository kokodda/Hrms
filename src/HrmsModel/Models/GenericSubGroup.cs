using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class GenericSubGroup
    {
        public int Id { get; set; }
        public int GenericGroupId { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual GenericGroup GenericGroup { get; set; }
        public virtual ICollection<EmployeeGroup> EmployeeGroups { get; set; }
        public virtual ICollection<ShiftGroup> ShiftGroups { get; set; }
    }
}
