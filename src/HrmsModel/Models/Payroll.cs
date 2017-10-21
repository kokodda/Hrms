using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Payroll
    {
        public long Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public long? SalaryScaleId { get; set; }
        public long? AttendanceId { get; set; }
        public string Narration { get; set; }
        public bool IsApproved { get; set; }
        public bool IsExported { get; set; }
        public bool IsActive { get; set; }
        public bool IsEndorsed { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual SalaryScale SalaryScale { get; set; }
        public virtual Attendance Attendance { get; set; }
        public virtual ICollection<PayrollEmployee> PayrollEmployees { get; set; }
        public virtual ICollection<PayrollAddition> PayrollAdditions { get; set; }
        public virtual ICollection<PayrollDeduction> PayrollDeductions { get; set; }
    }
}
