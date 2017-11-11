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
        public string ShiftTypeCode { get; set; }
        public long CalendarId { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ThruTime { get; set; }
        public int SortOrder { get; set; }

        public virtual Calendar Calendar { get; set; }
        public virtual ICollection<ShiftRotation> ShiftRotations { get; set; }
    }
}
