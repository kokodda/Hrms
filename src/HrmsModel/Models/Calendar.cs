using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Calendar
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public DateTime EffectiveFromDate { get; set; }
        public int NbrWorkingDays { get; set; }
        public string FirstWeekDay { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ThruTime { get; set; }
        public int BreakMinutes { get; set; }
        public int FlexStartMinutes { get; set; }
        public int OverTime1Multiplier { get; set; }
        public int OverTime2Multiplier { get; set; }
        public int HolidaysMultiplier { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<HolidayVariation> HolidayVariations { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
