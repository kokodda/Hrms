using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class SiteContent
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string Caption { get; set; }
        public string OthCaption { get; set; }
        public string Details { get; set; }
        public string Url { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
