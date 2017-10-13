using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Holiday
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public int FromDay { get; set; }
        public int FromMonth { get; set; }
        public int NbrDays { get; set; }
        public bool IsHijri { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<HolidayVariation> HolidayVariations { get; set; }
    }
}
