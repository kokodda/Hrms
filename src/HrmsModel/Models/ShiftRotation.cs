using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class ShiftRotation
    {
        public int Id { get; set; }
        public int GenericSubGroupId { get; set; }
        public int ShiftId { get; set; }
        public int NbrDays { get; set; }
        public int SortOrder { get; set; }

        public virtual Shift Shift { get; set; }
        public virtual GenericSubGroup GenericSubGroup { get; set; }
    }
}
