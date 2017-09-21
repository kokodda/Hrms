using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class GenericGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<GenericSubGroup> GenericSubGroups { get; set; }
    }
}
