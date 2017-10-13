using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class HolidayVariation
    {
        public long Id { get; set; }
        public long CalendarId { get; set; }
        public long HolidayId { get; set; }
        public DateTime FromDate { get; set; }
        public int NbrDays { get; set; }
        public int CompensateNbrDays { get; set; }
        public string Narration { get; set; }
        public bool IsComponsated { get; set; }
        public bool IsActive { get; set; }

        public virtual Calendar Calendar { get; set; }
        public virtual Holiday Holiday { get; set; }
    }
}
