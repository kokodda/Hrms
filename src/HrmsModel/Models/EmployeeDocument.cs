using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class EmployeeDocument
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Details { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }
}
