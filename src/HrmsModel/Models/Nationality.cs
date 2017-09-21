using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Nationality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OthName { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
