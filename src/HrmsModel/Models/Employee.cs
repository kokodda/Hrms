using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrmsModel.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string EmpUid { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string OthFirstName { get; set; }
        public string OthFamilyName { get; set; }
        public string OthFatherName { get; set; }
        public string OthMotherName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsMale { get; set; }
        public int NationalityId { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public bool IsMilitaryExempted { get; set; }
        public string Phone { get; set; }
        public string HomePhone1 { get; set; }
        public string HomePhone2 { get; set; }
        public string Email { get; set; }
        public int GovernorateId { get; set; }
        public string Address { get; set; }
        public string PermenantAddress { get; set; }
        public byte[] Photo { get; set; }
        public string Brief { get; set; }
        public string OthBrief { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Nationality Nationality { get; set; }
        public virtual Governorate Governorate { get; set; }
        public virtual ICollection<Employment> Employments { get; set; }
        public virtual ICollection<EmployeeLeave> EmployeeLeaves { get; set; }
        public virtual ICollection<EmployeeLeaveBalance> EmployeeLeaveBalances { get; set; }
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
        public virtual ICollection<EmployeeQualification> EmployeeQualifications { get; set; }
        public virtual ICollection<EmployeeFamily> EmployeeFamilies { get; set; }
        public virtual ICollection<EmployeeLanguage> EmployeeLanguages { get; set; }
        public virtual ICollection<EmployeeDocument> EmployeeDocuments { get; set; }
        public virtual ICollection<EmployeeGroup> EmployeeGroups { get; set; }
        public virtual ICollection<PayrollEmployee> PayrollEmployees { get; set; }
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
    }
}
