using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeeFamily
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int FamilyMemberTypeId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Contacts { get; set; }
        public bool IsActive { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual FamilyMemberType FamilyMemberType { get; set; }
    }
}
