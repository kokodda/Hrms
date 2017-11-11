using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public long CalendarId { get; set; }

        public virtual Calendar Calendar { get; set; }
        public virtual ICollection<ShiftRotation> ShiftRotations { get; set; }
        public virtual ICollection<ShiftGroup> ShiftGroups { get; set; }
    }
}
