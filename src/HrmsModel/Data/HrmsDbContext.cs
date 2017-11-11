using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using HrmsModel.Models;
using Microsoft.AspNetCore.Builder;

namespace HrmsModel.Data
{
    public class HrmsDbContext : DbContext
    {
        public HrmsDbContext(DbContextOptions<HrmsDbContext> options) : base(options)
        {

        }

        public virtual DbSet<OrgUnitType> OrgUnitTypes { get; set; }
        public virtual DbSet<StandardTitleType> StandardTitleTypes { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<AllowanceType> AllowanceTypes { get; set; }
        public virtual DbSet<CompetencyAreaType> CompetencyAreaTypes { get; set; }
        public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }
        public virtual DbSet<FamilyMemberType> FamilyMemberTypes { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<QualificationType> QualificationTypes { get; set; }
        public virtual DbSet<LanguageType> LanguageTypes { get; set; }
        public virtual DbSet<SalaryScaleType> SalaryScaleTypes { get; set; }
        public virtual DbSet<PromotionType> PromotionTypes { get; set; }
        public virtual DbSet<Governorate> Governorates { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<PayrollComponentType> PayrollComponentTypes { get; set; }
        public virtual DbSet<AttendanceActionType> AttendanceActionTypes { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }

        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<HolidayVariation> HolidayVariations { get; set; }
        public virtual DbSet<OrgUnit> OrgUnits { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<GradeGroup> GradeGroups { get; set; }
        public virtual DbSet<JobGrade> JobGrades { get; set; }
        public virtual DbSet<SalaryStep> SalarySteps { get; set; }
        public virtual DbSet<CompetencyCategory> CompetencyCategories { get; set; }
        public virtual DbSet<CompetencySubCategory> CompetencySubCategories { get; set; }
        public virtual DbSet<LeavePolicy> LeavePolicies { get; set; }
        public virtual DbSet<AllowancePolicy> AllowancePolicies { get; set; }
        public virtual DbSet<Competency> Competencies { get; set; }
        public virtual DbSet<SalaryScale> SalaryScales { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public virtual DbSet<EmployeeFamily> EmployeeFamilies { get; set; }
        public virtual DbSet<EmployeeLanguage> EmployeeLanguages { get; set; }
        public virtual DbSet<EmployeeLeave> EmployeeLeaves { get; set; }
        public virtual DbSet<EmployeeLeaveBalance> EmployeeLeaveBalances { get; set; }
        public virtual DbSet<Employment> Employments { get; set; }
        public virtual DbSet<EmployeeQualification> EmployeeQualifications { get; set; }
        public virtual DbSet<Remuneration> Remunerations { get; set; }
        public virtual DbSet<GenericGroup> GenericGroups { get; set; }
        public virtual DbSet<GenericSubGroup> GenericSubGroups { get; set; }
        public virtual DbSet<EmployeeGroup> EmployeeGroups { get; set; }
        public virtual DbSet<EmployeePromotion> EmployeePromotions { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
        public virtual DbSet<Payroll> Payrolls { get; set; }
        public virtual DbSet<PayrollEmployee> PayrollEmployees { get; set; }
        public virtual DbSet<PayrollAddition> PayrollAdditions { get; set; }
        public virtual DbSet<PayrollDeduction> PayrollDeductions { get; set; }
        public virtual DbSet<CarouselItem> CarouselItems { get; set; }
        public virtual DbSet<SiteContent> SiteContents { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyGroup> CompanyGroups { get; set; }
        public virtual DbSet<CompanyGroupMember> CompanyGroupMembers { get; set; }
        public virtual DbSet<CompanyAccount> CompanyAccounts { get; set; }
        public virtual DbSet<CompanyGroupAccount> CompanyGroupAccounts { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<ShiftRotation> ShiftRotations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrgUnitType>().HasKey(b => b.Id);
            modelBuilder.Entity<OrgUnitType>().HasAlternateKey(b => b.SysCode).HasName("UK_OrgUnitType_SysCode");
            modelBuilder.Entity<OrgUnitType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<OrgUnitType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<OrgUnitType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<OrgUnitType>().ToTable("OrgUnitTypes");

            modelBuilder.Entity<StandardTitleType>().HasKey(b => b.Id);
            modelBuilder.Entity<StandardTitleType>().HasAlternateKey(b => b.SysCode).HasName("UK_StandardTitleType_SysCode");
            modelBuilder.Entity<StandardTitleType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<StandardTitleType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<StandardTitleType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<StandardTitleType>().ToTable("StandardTitleTypes");

            modelBuilder.Entity<LeaveType>().HasKey(b => b.Id);
            modelBuilder.Entity<LeaveType>().HasAlternateKey(b => b.SysCode).HasName("UK_LeaveType_SysCode");
            modelBuilder.Entity<LeaveType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<LeaveType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<LeaveType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<LeaveType>().ToTable("LeaveTypes");

            modelBuilder.Entity<AllowanceType>().HasKey(b => b.Id);
            modelBuilder.Entity<AllowanceType>().HasAlternateKey(b => b.SysCode).HasName("UK_AllowanceType_SysCode");
            modelBuilder.Entity<AllowanceType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<AllowanceType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<AllowanceType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<AllowanceType>().ToTable("AllowanceTypes");

            modelBuilder.Entity<CompetencyAreaType>().HasKey(b => b.Id);
            modelBuilder.Entity<CompetencyAreaType>().HasAlternateKey(b => b.SysCode).HasName("UK_CompetencyAreaType_SysCode");
            modelBuilder.Entity<CompetencyAreaType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<CompetencyAreaType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<CompetencyAreaType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<CompetencyAreaType>().ToTable("CompetencyAreaTypes");

            modelBuilder.Entity<SalaryScaleType>().HasKey(b => b.Id);
            modelBuilder.Entity<SalaryScaleType>().HasAlternateKey(b => b.SysCode).HasName("UK_SalaryScaleType_SysCode");
            modelBuilder.Entity<SalaryScaleType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<SalaryScaleType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<SalaryScaleType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<SalaryScaleType>().ToTable("SalaryScaleTypes");

            modelBuilder.Entity<EmploymentType>().HasKey(b => b.Id);
            modelBuilder.Entity<EmploymentType>().HasAlternateKey(b => b.SysCode).HasName("UK_EmploymentType_SysCode");
            modelBuilder.Entity<EmploymentType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<EmploymentType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<EmploymentType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<EmploymentType>().ToTable("EmploymentTypes");

            modelBuilder.Entity<FamilyMemberType>().HasKey(b => b.Id);
            modelBuilder.Entity<FamilyMemberType>().HasAlternateKey(b => b.SysCode).HasName("UK_FamilyMemberType_SysCode");
            modelBuilder.Entity<FamilyMemberType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<FamilyMemberType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<FamilyMemberType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<FamilyMemberType>().ToTable("FamilyMemberTypes");

            modelBuilder.Entity<DocumentType>().HasKey(b => b.Id);
            modelBuilder.Entity<DocumentType>().HasAlternateKey(b => b.SysCode).HasName("UK_DocumentType_SysCode");
            modelBuilder.Entity<DocumentType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<DocumentType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<DocumentType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<DocumentType>().ToTable("DocumentTypes");

            modelBuilder.Entity<QualificationType>().HasKey(b => b.Id);
            modelBuilder.Entity<QualificationType>().HasAlternateKey(b => b.SysCode).HasName("UK_QualificationType_SysCode");
            modelBuilder.Entity<QualificationType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<QualificationType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<QualificationType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<QualificationType>().ToTable("QualificationTypes");

            modelBuilder.Entity<LanguageType>().HasKey(b => b.Id);
            modelBuilder.Entity<LanguageType>().HasAlternateKey(b => b.SysCode).HasName("UK_LanguageType_SysCode");
            modelBuilder.Entity<LanguageType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<LanguageType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<LanguageType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<LanguageType>().ToTable("LanguageTypes");

            modelBuilder.Entity<PromotionType>().HasKey(b => b.Id);
            modelBuilder.Entity<PromotionType>().HasAlternateKey(b => b.SysCode).HasName("UK_PromotionType_SysCode");
            modelBuilder.Entity<PromotionType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<PromotionType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<PromotionType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<PromotionType>().ToTable("PromotionTypes");

            modelBuilder.Entity<PayrollComponentType>().HasKey(b => b.Id);
            modelBuilder.Entity<PayrollComponentType>().HasAlternateKey(b => b.SysCode).HasName("UK_PayrollComponentType_SysCode");
            modelBuilder.Entity<PayrollComponentType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<PayrollComponentType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<PayrollComponentType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<PayrollComponentType>().ToTable("PayrollComponentTypes");

            modelBuilder.Entity<AttendanceActionType>().HasKey(b => b.Id);
            modelBuilder.Entity<AttendanceActionType>().HasAlternateKey(b => b.SysCode).HasName("UK_AttendanceActionType_SysCode");
            modelBuilder.Entity<AttendanceActionType>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<AttendanceActionType>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<AttendanceActionType>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<AttendanceActionType>().ToTable("AttendanceActionTypes");

            modelBuilder.Entity<Governorate>().HasKey(b => b.Id);
            modelBuilder.Entity<Governorate>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Governorate>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Governorate>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Governorate>().ToTable("Governorates");

            modelBuilder.Entity<Nationality>().HasKey(b => b.Id);
            modelBuilder.Entity<Nationality>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Nationality>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Nationality>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Nationality>().ToTable("Nationalities");

            modelBuilder.Entity<Bank>().HasKey(b => b.Id);
            modelBuilder.Entity<Bank>().Property(b => b.SysCode).HasMaxLength(10);
            modelBuilder.Entity<Bank>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Bank>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Bank>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Bank>().ToTable("Banks");

            modelBuilder.Entity<CompetencyCategory>().HasKey(b => b.Id);
            modelBuilder.Entity<CompetencyCategory>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<CompetencyCategory>().Property(b => b.OthName).IsRequired(false).HasMaxLength(100);
            modelBuilder.Entity<CompetencyCategory>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<CompetencyCategory>().ToTable("CompetencyCategories");

            modelBuilder.Entity<CompetencySubCategory>().HasKey(b => b.Id);
            modelBuilder.Entity<CompetencySubCategory>().HasOne(b => b.CompetencyCategory).WithMany(b => b.CompetencySubCategories).HasForeignKey(b => b.CompetencyCategoryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CompetencySubCategory>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<CompetencySubCategory>().Property(b => b.OthName).IsRequired(false).HasMaxLength(100);
            modelBuilder.Entity<CompetencySubCategory>().Property(b => b.Description).IsRequired(false);
            modelBuilder.Entity<CompetencySubCategory>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<CompetencySubCategory>().ToTable("CompetencySubCategories");

            modelBuilder.Entity<GradeGroup>().HasKey(b => b.Id);
            modelBuilder.Entity<GradeGroup>().Property(b => b.Code).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<GradeGroup>().Property(b => b.Name).IsRequired(false).HasMaxLength(100);
            modelBuilder.Entity<GradeGroup>().Property(b => b.OthName).IsRequired(false).HasMaxLength(100);
            modelBuilder.Entity<GradeGroup>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<GradeGroup>().ToTable("GradeGroups");

            modelBuilder.Entity<JobGrade>().HasKey(b => b.Id);
            modelBuilder.Entity<JobGrade>().HasOne(b => b.GradeGroup).WithMany(b => b.JobGrades).HasForeignKey(b => b.GradeGroupId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<JobGrade>().Property(b => b.Code).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<JobGrade>().Property(b => b.Name).IsRequired(false).HasMaxLength(100);
            modelBuilder.Entity<JobGrade>().Property(b => b.OthName).IsRequired(false).HasMaxLength(100);
            modelBuilder.Entity<JobGrade>().Property(b => b.SalaryIncrPctg).HasDefaultValue(0);
            modelBuilder.Entity<JobGrade>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<JobGrade>().ToTable("JobGrades");

            modelBuilder.Entity<SalaryStep>().HasKey(b => b.Id);
            modelBuilder.Entity<SalaryStep>().Property(b => b.Code).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<SalaryStep>().Property(b => b.Name).IsRequired(false).HasMaxLength(100);
            modelBuilder.Entity<SalaryStep>().Property(b => b.OthName).IsRequired(false).HasMaxLength(100);
            modelBuilder.Entity<SalaryStep>().Property(b => b.SalaryIncrPctg).HasDefaultValue(0);
            modelBuilder.Entity<SalaryStep>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<SalaryStep>().ToTable("SalarySteps");

            modelBuilder.Entity<LeavePolicy>().HasKey(b => b.Id);
            modelBuilder.Entity<LeavePolicy>().HasOne(b => b.LeaveType).WithMany(b => b.LeavePolicies).HasForeignKey(b => b.LeaveTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LeavePolicy>().HasOne(b => b.GradeGroup).WithMany(b => b.LeavePolicies).HasForeignKey(b => b.GradeGroupId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LeavePolicy>().HasOne(b => b.JobGrade).WithMany(b => b.LeavePolicies).HasForeignKey(b => b.JobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LeavePolicy>().Property(b => b.IsCompanyPolicy).HasDefaultValue(false);
            modelBuilder.Entity<LeavePolicy>().Property(b => b.Description).IsRequired(false);
            modelBuilder.Entity<LeavePolicy>().Property(b => b.Remarks).IsRequired(false);
            modelBuilder.Entity<LeavePolicy>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<LeavePolicy>().Property(b => b.CreatedDate).HasColumnType("date");
            modelBuilder.Entity<LeavePolicy>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<LeavePolicy>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<LeavePolicy>().ToTable("LeavePolicies");

            modelBuilder.Entity<AllowancePolicy>().HasKey(b => b.Id);
            modelBuilder.Entity<AllowancePolicy>().HasOne(b => b.AllowanceType).WithMany(b => b.AllowancePolicies).HasForeignKey(b => b.AllowanceTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AllowancePolicy>().HasOne(b => b.GradeGroup).WithMany(b => b.AllowancePolicies).HasForeignKey(b => b.GradeGroupId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AllowancePolicy>().HasOne(b => b.JobGrade).WithMany(b => b.AllowancePolicies).HasForeignKey(b => b.JobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AllowancePolicy>().Property(b => b.IsCompanyPolicy).HasDefaultValue(false);
            modelBuilder.Entity<AllowancePolicy>().Property(b => b.isPercentage).HasDefaultValue(false);
            modelBuilder.Entity<AllowancePolicy>().Property(b => b.Description).IsRequired(false);
            modelBuilder.Entity<AllowancePolicy>().Property(b => b.Remarks).IsRequired(false);
            modelBuilder.Entity<AllowancePolicy>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<AllowancePolicy>().Property(b => b.CreatedDate).HasColumnType("date");
            modelBuilder.Entity<AllowancePolicy>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<AllowancePolicy>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<AllowancePolicy>().ToTable("AllowancePolicies");

            modelBuilder.Entity<Competency>().HasKey(b => b.Id);
            modelBuilder.Entity<Competency>().HasOne(b => b.CompetencySubCategory).WithMany(b => b.Competencies).HasForeignKey(b => b.CompetencySubCategoryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Competency>().HasOne(b => b.GradeGroup).WithMany(b => b.Competencies).HasForeignKey(b => b.GradeGroupId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Competency>().HasOne(b => b.JobGrade).WithMany(b => b.Competencies).HasForeignKey(b => b.JobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Competency>().Property(b => b.IsCompanyPolicy).HasDefaultValue(false);
            modelBuilder.Entity<Competency>().Property(b => b.Requirements).IsRequired(false);
            modelBuilder.Entity<Competency>().Property(b => b.Remarks).IsRequired(false);
            modelBuilder.Entity<Competency>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Competency>().Property(b => b.CreatedDate).HasColumnType("date");
            modelBuilder.Entity<Competency>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<Competency>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Competency>().ToTable("Competencies");

            modelBuilder.Entity<OrgUnit>().HasKey(b => b.Id);
            modelBuilder.Entity<OrgUnit>().HasOne(b => b.OrgUnitType).WithMany(b => b.OrgUnits).HasForeignKey(b => b.OrgUnitTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrgUnit>().HasOne(b => b.JobGrade).WithMany(b => b.OrgUnits).HasForeignKey(b => b.JobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrgUnit>().HasOne(b => b.StandardTitleType).WithMany(b => b.OrgUnits).HasForeignKey(b => b.StandardTitleTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrgUnit>().Property(b => b.Code).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<OrgUnit>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<OrgUnit>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<OrgUnit>().Property(b => b.HeadPositionName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<OrgUnit>().Property(b => b.OthHeadPositionName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<OrgUnit>().Property(b => b.JobCode).HasMaxLength(100);
            modelBuilder.Entity<OrgUnit>().Property(b => b.IsVacant).HasDefaultValue(false);
            modelBuilder.Entity<OrgUnit>().Property(b => b.CreatedDate).HasColumnType("date");
            modelBuilder.Entity<OrgUnit>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<OrgUnit>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<OrgUnit>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<OrgUnit>().ToTable("OrgUnits");

            modelBuilder.Entity<Position>().HasKey(b => b.Id);
            modelBuilder.Entity<Position>().HasOne(b => b.OrgUnit).WithMany(b => b.Positions).HasForeignKey(b => b.OrgUnitId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Position>().HasOne(b => b.JobGrade).WithMany(b => b.Positions).HasForeignKey(b => b.JobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Position>().HasOne(b => b.StandardTitleType).WithMany(b => b.Positions).HasForeignKey(b => b.StandardTitleTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Position>().Property(b => b.Name).HasMaxLength(100);
            modelBuilder.Entity<Position>().Property(b => b.OthName).HasMaxLength(100);
            modelBuilder.Entity<Position>().Property(b => b.JobCode).HasMaxLength(100);
            modelBuilder.Entity<Position>().Property(b => b.Capacity).HasDefaultValue(1);
            modelBuilder.Entity<Position>().Property(b => b.TotalVacant).HasDefaultValue(1);
            modelBuilder.Entity<Position>().Property(b => b.CreatedDate).HasColumnType("date");
            modelBuilder.Entity<Position>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<Position>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Position>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Position>().ToTable("Positions");
            
            modelBuilder.Entity<SalaryScale>().HasKey(b => b.Id);
            modelBuilder.Entity<SalaryScale>().HasOne(b => b.Company).WithMany(b => b.SalaryScales).HasForeignKey(b => b.CompanyId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SalaryScale>().HasOne(b => b.SalaryScaleType).WithMany(b => b.SalaryScales).HasForeignKey(b => b.SalaryScaleTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SalaryScale>().HasOne(b => b.FromJobGrade).WithMany(b => b.FromGradeSalaryScales).HasForeignKey(b => b.FromJobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SalaryScale>().HasOne(b => b.ThruJobGrade).WithMany(b => b.ThruGradeSalaryScales).HasForeignKey(b => b.ThruJobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SalaryScale>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<SalaryScale>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<SalaryScale>().Property(b => b.Description).IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<SalaryScale>().Property(b => b.CurrencyCode).IsRequired().HasMaxLength(3);
            modelBuilder.Entity<SalaryScale>().Property(b => b.FromDate).HasColumnType("date");
            modelBuilder.Entity<SalaryScale>().Property(b => b.ThruDate).HasColumnType("date");
            modelBuilder.Entity<SalaryScale>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<SalaryScale>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<SalaryScale>().ToTable("SalaryScales");

            modelBuilder.Entity<Candidate>().HasKey(b => b.Id);
            modelBuilder.Entity<Candidate>().HasOne(b => b.OrgUnit).WithMany(b => b.Candidates).HasForeignKey(b => b.OrgUnitId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Candidate>().HasOne(b => b.Nationality).WithMany(b => b.Candidates).HasForeignKey(b => b.NationalityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Candidate>().HasOne(b => b.Governorate).WithMany(b => b.Candidates).HasForeignKey(b => b.GovernorateId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Candidate>().Property(b => b.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.FamilyName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.FatherName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.MotherName).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.OthFirstName).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.OthFamilyName).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.OthFatherName).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.OthMotherName).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.BirthDate).HasColumnType("date");
            modelBuilder.Entity<Candidate>().Property(b => b.MaritalStatus).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.Phone).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.HomePhone1).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.HomePhone2).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.Email).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Candidate>().Property(b => b.Address).IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<Candidate>().Property(b => b.PermenantAddress).IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<Candidate>().Property(b => b.IsSubmitted).HasDefaultValue(false);
            modelBuilder.Entity<Candidate>().Property(b => b.IsApproved).HasDefaultValue(false);
            modelBuilder.Entity<Candidate>().Property(b => b.SubmittedDate).HasColumnType("date");
            modelBuilder.Entity<Candidate>().Property(b => b.ApprovedDate).HasColumnType("date");
            modelBuilder.Entity<Candidate>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<Candidate>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Candidate>().ToTable("Candidates");

            modelBuilder.Entity<Employee>().HasKey(b => b.Id);
            modelBuilder.Entity<Employee>().HasOne(b => b.Nationality).WithMany(b => b.Employees).HasForeignKey(b => b.NationalityId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employee>().HasOne(b => b.Governorate).WithMany(b => b.Employees).HasForeignKey(b => b.GovernorateId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employee>().Property(b => b.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.FamilyName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.FatherName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.MotherName).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.OthFirstName).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.OthFamilyName).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.OthFatherName).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.OthMotherName).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.BirthDate).HasColumnType("date");
            modelBuilder.Entity<Employee>().Property(b => b.MaritalStatus).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.BloodGroup).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.Phone).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.HomePhone1).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.HomePhone2).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.Email).IsRequired(false).HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(b => b.Address).IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<Employee>().Property(b => b.PermenantAddress).IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<Employee>().Property(b => b.Branch).HasMaxLength(100);
            modelBuilder.Entity<Employee>().Property(b => b.IBAN).HasMaxLength(100);
            modelBuilder.Entity<Employee>().Property(b => b.JoinDate).HasColumnType("date");
            modelBuilder.Entity<Employee>().Property(b => b.ResignationDate).HasColumnType("date");
            modelBuilder.Entity<Employee>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<Employee>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Employee>().Property(b => b.IsActive).HasDefaultValue(false);
            modelBuilder.Entity<Employee>().ToTable("Employees");

            modelBuilder.Entity<EmployeeDocument>().HasKey(b => b.Id);
            modelBuilder.Entity<EmployeeDocument>().HasOne(b => b.Employee).WithMany(b => b.EmployeeDocuments).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeDocument>().HasOne(b => b.DocumentType).WithMany(b => b.EmployeeDocuments).HasForeignKey(b => b.DocumentTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeDocument>().Property(b => b.Details).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<EmployeeDocument>().Property(b => b.Url).HasMaxLength(450);
            modelBuilder.Entity<EmployeeDocument>().Property(b => b.ExpiryDate).HasColumnType("date");
            modelBuilder.Entity<EmployeeDocument>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<EmployeeDocument>().ToTable("EmployeeDocuments");

            modelBuilder.Entity<EmployeeFamily>().HasKey(b => b.Id);
            modelBuilder.Entity<EmployeeFamily>().HasOne(b => b.Employee).WithMany(b => b.EmployeeFamilies).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeFamily>().HasOne(b => b.FamilyMemberType).WithMany(b => b.EmployeeFamilies).HasForeignKey(b => b.FamilyMemberTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeFamily>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<EmployeeFamily>().Property(b => b.Contacts).HasMaxLength(100);
            modelBuilder.Entity<EmployeeFamily>().Property(b => b.BirthDate).HasColumnType("date");
            modelBuilder.Entity<EmployeeFamily>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<EmployeeFamily>().ToTable("EmployeeFamilies");

            modelBuilder.Entity<EmployeeLanguage>().HasKey(b => b.Id);
            modelBuilder.Entity<EmployeeLanguage>().HasOne(b => b.Employee).WithMany(b => b.EmployeeLanguages).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeLanguage>().HasOne(b => b.LanguageType).WithMany(b => b.EmployeeLanguages).HasForeignKey(b => b.LanguageTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeLanguage>().Property(b => b.LanguageName).HasMaxLength(50);
            modelBuilder.Entity<EmployeeLanguage>().Property(b => b.ReadingLevel).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<EmployeeLanguage>().Property(b => b.SpeakingLevel).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<EmployeeLanguage>().Property(b => b.WritingLevel).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<EmployeeLanguage>().ToTable("EmployeeLanguages");

            modelBuilder.Entity<EmployeeLeaveBalance>().HasKey(b => b.Id);
            modelBuilder.Entity<EmployeeLeaveBalance>().HasOne(b => b.Employee).WithMany(b => b.EmployeeLeaveBalances).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeLeaveBalance>().HasOne(b => b.LeaveType).WithMany(b => b.EmployeeLeaveBalances).HasForeignKey(b => b.LeaveTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeLeaveBalance>().Property(b => b.AnnualEntitlement).HasDefaultValue(0);
            modelBuilder.Entity<EmployeeLeaveBalance>().Property(b => b.BalanceDays).HasDefaultValue(0);
            modelBuilder.Entity<EmployeeLeaveBalance>().Property(b => b.BalanceHours).HasDefaultValue(0);
            modelBuilder.Entity<EmployeeLeaveBalance>().Property(b => b.CarryForward).HasDefaultValue(0);
            modelBuilder.Entity<EmployeeLeaveBalance>().ToTable("EmployeeLeaveBalances");

            modelBuilder.Entity<EmployeeLeave>().HasKey(b => b.Id);
            modelBuilder.Entity<EmployeeLeave>().HasOne(b => b.Employee).WithMany(b => b.EmployeeLeaves).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeLeave>().HasOne(b => b.LeaveType).WithMany(b => b.EmployeeLeaves).HasForeignKey(b => b.LeaveTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeLeave>().Property(b => b.FromDate).HasColumnType("date");
            modelBuilder.Entity<EmployeeLeave>().Property(b => b.ThruDate).HasColumnType("date");
            modelBuilder.Entity<EmployeeLeave>().Property(b => b.ActualFromDate).HasColumnType("date");
            modelBuilder.Entity<EmployeeLeave>().Property(b => b.ActualThruDate).HasColumnType("date");
            modelBuilder.Entity<EmployeeLeave>().Property(b => b.SubmittedDate).HasColumnType("date");
            modelBuilder.Entity<EmployeeLeave>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<EmployeeLeave>().Property(b => b.IsApproved).HasDefaultValue(false);
            modelBuilder.Entity<EmployeeLeave>().Property(b => b.IsEndorsed).HasDefaultValue(false);
            modelBuilder.Entity<EmployeeLeave>().Property(b => b.IsCancelled).HasDefaultValue(false);
            modelBuilder.Entity<EmployeeLeave>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<EmployeeLeave>().ToTable("EmployeeLeaves");

            modelBuilder.Entity<Employment>().HasKey(b => b.Id);
            modelBuilder.Entity<Employment>().HasOne(b => b.Employee).WithMany(b => b.Employments).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employment>().HasOne(b => b.OrgUnit).WithMany(b => b.Employments).HasForeignKey(b => b.OrgUnitId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employment>().HasOne(b => b.EmploymentType).WithMany(b => b.Employments).HasForeignKey(b => b.EmploymentTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employment>().HasOne(b => b.JobGrade).WithMany(b => b.Employments).HasForeignKey(b => b.JobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employment>().HasOne(b => b.SalaryScaleType).WithMany(b => b.Employments).HasForeignKey(b => b.SalaryScaleTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employment>().HasOne(b => b.SalaryStep).WithMany(b => b.Employments).HasForeignKey(b => b.SalaryStepId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employment>().HasOne(b => b.Position).WithMany(b => b.Employments).HasForeignKey(b => b.PositionId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employment>().Property(b => b.IsHead).HasDefaultValue(false);
            modelBuilder.Entity<Employment>().Property(b => b.FromDate).HasColumnType("date");
            modelBuilder.Entity<Employment>().Property(b => b.ThruDate).HasColumnType("date");
            modelBuilder.Entity<Employment>().Property(b => b.IsActing).HasDefaultValue(false);
            modelBuilder.Entity<Employment>().Property(b => b.IsAttendRequired).HasDefaultValue(false);
            modelBuilder.Entity<Employment>().Property(b => b.IsOverTimeAllowed).HasDefaultValue(false);
            modelBuilder.Entity<Employment>().Property(b => b.IsProbation).HasDefaultValue(false);
            modelBuilder.Entity<Employment>().Property(b => b.BasicSalary).HasDefaultValue(0);
            modelBuilder.Entity<Employment>().Property(b => b.IsPayrollExcluded).HasDefaultValue(false);
            modelBuilder.Entity<Employment>().Property(b => b.Details).HasMaxLength(256);
            modelBuilder.Entity<Employment>().Property(b => b.Remarks).HasMaxLength(256);
            modelBuilder.Entity<Employment>().Property(b => b.IsActive).HasDefaultValue(false);
            modelBuilder.Entity<Employment>().ToTable("Employments");

            modelBuilder.Entity<EmployeePromotion>().HasKey(b => b.Id);
            modelBuilder.Entity<EmployeePromotion>().HasOne(b => b.Employment).WithMany(b => b.EmployeePromotions).HasForeignKey(b => b.EmploymentId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeePromotion>().HasOne(b => b.JobGrade).WithMany(b => b.EmployeePromotions).HasForeignKey(b => b.JobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeePromotion>().HasOne(b => b.SalaryStep).WithMany(b => b.EmployeePromotions).HasForeignKey(b => b.SalaryStepId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.EffectiveFromDate).HasColumnType("date");
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.IsApproved).HasDefaultValue(false);
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.IsCancelled).HasDefaultValue(false);
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.IsIncreasePercentage).HasDefaultValue(false);
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.BasicSalary).HasDefaultValue(0);
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.SalaryIncreaseValue).HasDefaultValue(0);
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.Details).HasMaxLength(256);
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.Remarks).HasMaxLength(256);
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.IsActive).HasDefaultValue(false);
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.CreatedDate).HasColumnType("date");
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<EmployeePromotion>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<EmployeePromotion>().ToTable("EmployeePromotions");

            modelBuilder.Entity<EmployeeQualification>().HasKey(b => b.Id);
            modelBuilder.Entity<EmployeeQualification>().HasOne(b => b.Employee).WithMany(b => b.EmployeeQualifications).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeQualification>().HasOne(b => b.QualificationType).WithMany(b => b.EmployeeQualifications).HasForeignKey(b => b.QualificationTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeQualification>().HasOne(b => b.CompetencyAreaType).WithMany(b => b.EmployeeQualifications).HasForeignKey(b => b.CompetencyAreaTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeQualification>().HasOne(b => b.CompetencySubCategory).WithMany(b => b.EmployeeQualifications).HasForeignKey(b => b.CompetencySubCategoryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeQualification>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<EmployeeQualification>().Property(b => b.OthName).HasMaxLength(100);
            modelBuilder.Entity<EmployeeQualification>().Property(b => b.ObtainedDate).HasColumnType("date");
            modelBuilder.Entity<EmployeeQualification>().Property(b => b.IsPlanned).HasDefaultValue(false);
            modelBuilder.Entity<EmployeeQualification>().ToTable("EmployeeQualifications");

            modelBuilder.Entity<Remuneration>().HasKey(b => b.Id);
            modelBuilder.Entity<Remuneration>().HasOne(b => b.Employee).WithMany(b => b.Remunerations).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Remuneration>().HasOne(b => b.Employment).WithMany(b => b.Remunerations).HasForeignKey(b => b.EmploymentId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Remuneration>().HasOne(b => b.AllowanceType).WithMany(b => b.Remunerations).HasForeignKey(b => b.AllowanceTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Remuneration>().Property(b => b.Narration).HasMaxLength(256);
            modelBuilder.Entity<Remuneration>().Property(b => b.FromDate).HasColumnType("date");
            modelBuilder.Entity<Remuneration>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<Remuneration>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Remuneration>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Remuneration>().ToTable("Remunerations");

            modelBuilder.Entity<GenericGroup>().HasKey(b => b.Id);
            modelBuilder.Entity<GenericGroup>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<GenericGroup>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<GenericGroup>().Property(b => b.Description).IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<GenericGroup>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<GenericGroup>().ToTable("GenericGroups");

            modelBuilder.Entity<GenericSubGroup>().HasKey(b => b.Id);
            modelBuilder.Entity<GenericSubGroup>().HasOne(b => b.GenericGroup).WithMany(b => b.GenericSubGroups).HasForeignKey(b => b.GenericGroupId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<GenericSubGroup>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<GenericSubGroup>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<GenericSubGroup>().Property(b => b.Description).IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<GenericSubGroup>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<GenericSubGroup>().ToTable("GenericSubGroups");

            modelBuilder.Entity<EmployeeGroup>().HasKey(b => b.Id);
            modelBuilder.Entity<EmployeeGroup>().HasOne(b => b.Employee).WithMany(b => b.EmployeeGroups).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EmployeeGroup>().HasOne(b => b.GenericSubGroup).WithMany(b => b.EmployeeGroups).HasForeignKey(b => b.GenericSubGroupId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EmployeeGroup>().Property(b => b.JoinDate).HasColumnType("date");
            modelBuilder.Entity<EmployeeGroup>().ToTable("EmployeeGroups");

            modelBuilder.Entity<Calendar>().HasKey(b => b.Id);
            modelBuilder.Entity<Calendar>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Calendar>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Calendar>().Property(b => b.FromTime).HasColumnType("datetime");
            modelBuilder.Entity<Calendar>().Property(b => b.ThruTime).HasColumnType("datetime");
            modelBuilder.Entity<Calendar>().Property(b => b.BreakMinutes).HasDefaultValue(0);
            modelBuilder.Entity<Calendar>().Property(b => b.FlexStartMinutes).HasDefaultValue(0);
            modelBuilder.Entity<Calendar>().Property(b => b.OverTime1Multiplier).HasDefaultValue(0);
            modelBuilder.Entity<Calendar>().Property(b => b.OverTime2Multiplier).HasDefaultValue(0);
            modelBuilder.Entity<Calendar>().Property(b => b.HolidaysMultiplier).HasDefaultValue(0);
            modelBuilder.Entity<Calendar>().Property(b => b.EffectiveFromDate).HasColumnType("date");
            modelBuilder.Entity<Calendar>().Property(b => b.IsDefault).HasDefaultValue(false);
            modelBuilder.Entity<Calendar>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Calendar>().ToTable("Calendars");

            modelBuilder.Entity<Holiday>().HasKey(b => b.Id);
            modelBuilder.Entity<Holiday>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Holiday>().Property(b => b.OthName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Holiday>().Property(b => b.IsHijri).HasDefaultValue(false);
            modelBuilder.Entity<Holiday>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Holiday>().ToTable("Holidays");

            modelBuilder.Entity<HolidayVariation>().HasKey(b => b.Id);
            modelBuilder.Entity<HolidayVariation>().HasOne(b => b.Calendar).WithMany(b => b.HolidayVariations).HasForeignKey(b => b.CalendarId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HolidayVariation>().HasOne(b => b.Holiday).WithMany(b => b.HolidayVariations).HasForeignKey(b => b.HolidayId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HolidayVariation>().Property(b => b.NbrDays).HasDefaultValue(0);
            modelBuilder.Entity<HolidayVariation>().Property(b => b.CompensateNbrDays).HasDefaultValue(0);
            modelBuilder.Entity<HolidayVariation>().Property(b => b.FromDate).HasColumnType("date");
            modelBuilder.Entity<HolidayVariation>().Property(b => b.Narration).IsRequired().HasMaxLength(256);
            modelBuilder.Entity<HolidayVariation>().Property(b => b.IsComponsated).HasDefaultValue(false);
            modelBuilder.Entity<HolidayVariation>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<HolidayVariation>().ToTable("HolidayVariations");

            modelBuilder.Entity<Attendance>().HasKey(b => b.Id);
            modelBuilder.Entity<Attendance>().Property(b => b.FromDate).HasColumnType("date");
            modelBuilder.Entity<Attendance>().Property(b => b.ThruDate).HasColumnType("date");
            modelBuilder.Entity<Attendance>().Property(b => b.IsEndorsed).HasDefaultValue(false);
            modelBuilder.Entity<Attendance>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Attendance>().ToTable("Attendances");

            modelBuilder.Entity<EmployeeAttendance>().HasKey(b => b.Id);
            modelBuilder.Entity<EmployeeAttendance>().HasOne(b => b.Employee).WithMany(b => b.EmployeeAttendances).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeAttendance>().HasOne(b => b.Attendance).WithMany(b => b.EmployeeAttendances).HasForeignKey(b => b.AttendanceId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<EmployeeAttendance>().Property(b => b.FactorValue).HasDefaultValue(1);
            modelBuilder.Entity<EmployeeAttendance>().Property(b => b.CompliancePercentage).HasDefaultValue(100);
            modelBuilder.Entity<EmployeeAttendance>().Property(b => b.NbrDays).HasDefaultValue(0);
            modelBuilder.Entity<EmployeeAttendance>().ToTable("EmployeeAttendances");

            modelBuilder.Entity<Payroll>().HasKey(b => b.Id);
            modelBuilder.Entity<Payroll>().HasOne(b => b.Attendance).WithMany(b => b.Payrolls).HasForeignKey(b => b.AttendanceId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payroll>().HasOne(b => b.SalaryScale).WithMany(b => b.Payrolls).HasForeignKey(b => b.SalaryScaleId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Payroll>().Property(b => b.Narration).HasMaxLength(256);
            modelBuilder.Entity<Payroll>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Payroll>().Property(b => b.IsApproved).HasDefaultValue(false);
            modelBuilder.Entity<Payroll>().Property(b => b.IsEndorsed).HasDefaultValue(false);
            modelBuilder.Entity<Payroll>().Property(b => b.IsExported).HasDefaultValue(false);
            modelBuilder.Entity<Payroll>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<Payroll>().Property(b => b.UpdatedBy).HasMaxLength(100);
            modelBuilder.Entity<Payroll>().ToTable("Payrolls");

            modelBuilder.Entity<PayrollEmployee>().HasKey(b => b.Id);
            modelBuilder.Entity<PayrollEmployee>().HasOne(b => b.Payroll).WithMany(b => b.PayrollEmployees).HasForeignKey(b => b.PayrollId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollEmployee>().HasOne(b => b.Employee).WithMany(b => b.PayrollEmployees).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollEmployee>().HasOne(b => b.Employment).WithMany(b => b.PayrollEmployees).HasForeignKey(b => b.EmploymentId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollEmployee>().HasOne(b => b.PayrollComponentType).WithMany(b => b.PayrollEmployees).HasForeignKey(b => b.PayrollComponentTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollEmployee>().Property(b => b.FromDate).HasColumnType("date");
            modelBuilder.Entity<PayrollEmployee>().Property(b => b.ThruDate).HasColumnType("date");
            modelBuilder.Entity<PayrollEmployee>().ToTable("PayrollEmployees");

            modelBuilder.Entity<PayrollAddition>().HasKey(b => b.Id);
            modelBuilder.Entity<PayrollAddition>().HasOne(b => b.Payroll).WithMany(b => b.PayrollAdditions).HasForeignKey(b => b.PayrollId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollAddition>().HasOne(b => b.Employee).WithMany(b => b.PayrollAdditions).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollAddition>().HasOne(b => b.GradeGroup).WithMany(b => b.PayrollAdditions).HasForeignKey(b => b.GradeGroupId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollAddition>().HasOne(b => b.FromJobGrade).WithMany(b => b.FromPayrollAdditions).HasForeignKey(b => b.FromJobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollAddition>().HasOne(b => b.ThruJobGrade).WithMany(b => b.ThruPayrollAdditions).HasForeignKey(b => b.ThruJobGradeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollAddition>().HasOne(b => b.PayrollComponentType).WithMany(b => b.PayrollAdditions).HasForeignKey(b => b.PayrollComponentTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollAddition>().Property(b => b.Narration).HasMaxLength(256);
            modelBuilder.Entity<PayrollAddition>().ToTable("PayrollAdditions");

            modelBuilder.Entity<PayrollDeduction>().HasKey(b => b.Id);
            modelBuilder.Entity<PayrollDeduction>().HasOne(b => b.Payroll).WithMany(b => b.PayrollDeductions).HasForeignKey(b => b.PayrollId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollDeduction>().HasOne(b => b.Employee).WithMany(b => b.PayrollDeductions).HasForeignKey(b => b.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollDeduction>().HasOne(b => b.PayrollComponentType).WithMany(b => b.PayrollDeductions).HasForeignKey(b => b.PayrollComponentTypeId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PayrollDeduction>().Property(b => b.NbrDays).HasDefaultValue(0);
            modelBuilder.Entity<PayrollDeduction>().Property(b => b.Value).HasDefaultValue(0);
            modelBuilder.Entity<PayrollDeduction>().Property(b => b.IsPercentage).HasDefaultValue(false);
            modelBuilder.Entity<PayrollDeduction>().Property(b => b.Narration).HasMaxLength(256);
            modelBuilder.Entity<PayrollDeduction>().ToTable("PayrollDeductions");

            modelBuilder.Entity<CarouselItem>().HasKey(b => b.Id);
            modelBuilder.Entity<CarouselItem>().Property(b => b.ImageName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<CarouselItem>().Property(b => b.Caption).IsRequired().HasMaxLength(450);
            modelBuilder.Entity<CarouselItem>().Property(b => b.OthCaption).IsRequired().HasMaxLength(450);
            modelBuilder.Entity<CarouselItem>().Property(b => b.BtnCaption).HasMaxLength(100);
            modelBuilder.Entity<CarouselItem>().Property(b => b.OthBtnCaption).HasMaxLength(100);
            modelBuilder.Entity<CarouselItem>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<CarouselItem>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<CarouselItem>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<CarouselItem>().ToTable("CarouselItems");

            modelBuilder.Entity<SiteContent>().HasKey(b => b.Id);
            modelBuilder.Entity<SiteContent>().Property(b => b.Name).HasMaxLength(100);
            modelBuilder.Entity<SiteContent>().Property(b => b.ContentType).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<SiteContent>().Property(b => b.Caption).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<SiteContent>().Property(b => b.OthCaption).HasMaxLength(100);
            modelBuilder.Entity<SiteContent>().Property(b => b.LastUpdated).HasColumnType("date");
            modelBuilder.Entity<SiteContent>().Property(b => b.UpdatedBy).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<SiteContent>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<SiteContent>().ToTable("SiteContents");

            modelBuilder.Entity<Company>().HasKey(b => b.Id);
            modelBuilder.Entity<Company>().HasOne(b => b.OrgUnit).WithOne(b => b.Company).HasForeignKey<Company>(b => b.Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Company>().HasOne(b => b.Calendar).WithMany(b => b.Companies).HasForeignKey(b => b.CalendarId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Company>().Property(b => b.ShortName).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Company>().Property(b => b.OthShortName).HasMaxLength(10);
            modelBuilder.Entity<Company>().Property(b => b.LegalTypeCode).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Company>().Property(b => b.Logo).IsRequired(false);
            modelBuilder.Entity<Company>().ToTable("Companies");

            modelBuilder.Entity<CompanyGroup>().HasKey(b => b.Id);
            modelBuilder.Entity<CompanyGroup>().HasOne(b => b.OrgUnit).WithMany(b => b.CompanyGroups).HasForeignKey(b => b.MotherOrgUnitId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CompanyGroup>().Property(b => b.Name).HasMaxLength(100);
            modelBuilder.Entity<CompanyGroup>().Property(b => b.OthName).HasMaxLength(100);
            modelBuilder.Entity<CompanyGroup>().Property(b => b.Logo).IsRequired(false);
            modelBuilder.Entity<CompanyGroup>().ToTable("CompanyGroups");

            modelBuilder.Entity<CompanyGroupMember>().HasKey(b => b.Id);
            modelBuilder.Entity<CompanyGroupMember>().HasOne(b => b.CompanyGroup).WithMany(b => b.CompanyGroupMembers).HasForeignKey(b => b.CompanyGroupId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CompanyGroupMember>().HasOne(b => b.Company).WithMany(b => b.CompanyGroupMembers).HasForeignKey(b => b.CompanyId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CompanyGroupMember>().Property(b => b.JoinDate).HasColumnType("date");
            modelBuilder.Entity<CompanyGroupMember>().Property(b => b.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<CompanyGroupMember>().ToTable("CompanyGroupMembers");

            modelBuilder.Entity<CompanyAccount>().HasKey(b => b.Id);
            modelBuilder.Entity<CompanyAccount>().HasOne(b => b.OrgUnit).WithMany(b => b.CompanyAccounts).HasForeignKey(b => b.OrgUnitId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CompanyAccount>().Property(b => b.AccountId).IsRequired();
            modelBuilder.Entity<CompanyAccount>().ToTable("CompanyAccounts");

            modelBuilder.Entity<CompanyGroupAccount>().HasKey(b => b.Id);
            modelBuilder.Entity<CompanyGroupAccount>().HasOne(b => b.CompanyGroup).WithMany(b => b.CompanyGroupAccounts).HasForeignKey(b => b.CompanyGroupId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CompanyGroupAccount>().HasOne(b => b.CompanyAccount).WithMany(b => b.CompanyGroupAccounts).HasForeignKey(b => b.CompanyAccountId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CompanyGroupAccount>().ToTable("CompanyGroupAccounts");

            modelBuilder.Entity<Shift>().HasKey(b => b.Id);
            modelBuilder.Entity<Shift>().HasOne(b => b.Calendar).WithMany(b => b.Shifts).HasForeignKey(b => b.CalendarId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Shift>().Property(b => b.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Shift>().Property(b => b.ShiftTypeCode).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Shift>().Property(b => b.FromTime).HasColumnType("datetime");
            modelBuilder.Entity<Shift>().Property(b => b.ThruTime).HasColumnType("datetime");
            modelBuilder.Entity<Shift>().ToTable("Shifts");

            modelBuilder.Entity<ShiftRotation>().HasKey(b => b.Id);
            modelBuilder.Entity<ShiftRotation>().HasOne(b => b.GenericSubGroup).WithMany(b => b.ShiftRotations).HasForeignKey(b => b.GenericSubGroupId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ShiftRotation>().HasOne(b => b.Shift).WithMany(b => b.ShiftRotations).HasForeignKey(b => b.ShiftId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ShiftRotation>().ToTable("ShiftRotations");
        }
    }
}
