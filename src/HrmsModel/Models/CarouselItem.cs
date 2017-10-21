using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class CarouselItem
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Caption { get; set; }
        public string OthCaption { get; set; }
        public string ImageText { get; set; }
        public string OthImageText { get; set; }
        public string BtnCaption { get; set; }
        public string OthBtnCaption { get; set; }
        public string BtnHref { get; set; }
        public short SortOrder { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
