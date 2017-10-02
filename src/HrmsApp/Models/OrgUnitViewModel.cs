using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrmsModel.Models;

namespace HrmsApp.Models
{
    public class OrgUnitViewModel
    {
        public OrgUnit OrgUnit { get; set; }
        public string ParentName { get; set; }
        public string HeadedByName { get; set; }
        public int TotalPositions { get; set; }
        public int TotalPersonnel { get; set; }
        public int CurrentPersonnel { get; set; }
        public int TotalVacancy { get; set; }
    }
}
