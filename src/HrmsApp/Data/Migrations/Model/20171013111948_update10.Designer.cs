using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HrmsModel.Data;

namespace HrmsApp.Data.Migrations.Model
{
    [DbContext(typeof(HrmsDbContext))]
    [Migration("20171013111948_update10")]
    partial class update10
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HrmsModel.Models.AllowancePolicy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AllowanceTypeId");

                    b.Property<int>("Amount");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("Description");

                    b.Property<int?>("GradeGroupId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsCompanyPolicy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("JobGradeId");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<string>("Remarks");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("isPercentage")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.HasIndex("AllowanceTypeId");

                    b.HasIndex("GradeGroupId");

                    b.HasIndex("JobGradeId");

                    b.ToTable("AllowancePolicies");
                });

            modelBuilder.Entity("HrmsModel.Models.AllowanceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_AllowanceType_SysCode");

                    b.ToTable("AllowanceTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.Calendar", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BreakMinutes")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime>("EffectiveFromDate")
                        .HasColumnType("date");

                    b.Property<string>("FirstWeekDay");

                    b.Property<int>("FlexStartMinutes")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime>("FromTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("NbrWorkingDays");

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ThruTime")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("HrmsModel.Models.Candidate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(256);

                    b.Property<string>("AppForm");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("GovernorateId");

                    b.Property<string>("HomePhone1")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("HomePhone2")
                        .HasMaxLength(50);

                    b.Property<bool>("IsHead");

                    b.Property<bool>("IsMale")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsMilitaryExempted");

                    b.Property<string>("JobName");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MotherName")
                        .HasMaxLength(50);

                    b.Property<int>("NationalityId");

                    b.Property<long>("OrgUnitId");

                    b.Property<string>("OthFamilyName")
                        .HasMaxLength(50);

                    b.Property<string>("OthFatherName")
                        .HasMaxLength(50);

                    b.Property<string>("OthFirstName")
                        .HasMaxLength(50);

                    b.Property<string>("OthJobName");

                    b.Property<string>("OthMotherName")
                        .HasMaxLength(50);

                    b.Property<string>("PermenantAddress")
                        .HasMaxLength(256);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long?>("PositionId");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("GovernorateId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("OrgUnitId");

                    b.HasIndex("PositionId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("HrmsModel.Models.Competency", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompetencySubCategoryId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<int?>("GradeGroupId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsCompanyPolicy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("JobGradeId");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<string>("Remarks");

                    b.Property<string>("Requirements");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CompetencySubCategoryId");

                    b.HasIndex("GradeGroupId");

                    b.HasIndex("JobGradeId");

                    b.ToTable("Competencies");
                });

            modelBuilder.Entity("HrmsModel.Models.CompetencyAreaType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_CompetencyAreaType_SysCode");

                    b.ToTable("CompetencyAreaTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.CompetencyCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.ToTable("CompetencyCategories");
                });

            modelBuilder.Entity("HrmsModel.Models.CompetencySubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompetencyCategoryId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.HasIndex("CompetencyCategoryId");

                    b.ToTable("CompetencySubCategories");
                });

            modelBuilder.Entity("HrmsModel.Models.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_DocumentType_SysCode");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(256);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("BloodGroup")
                        .HasMaxLength(50);

                    b.Property<string>("Brief");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("EmpUid");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("GovernorateId");

                    b.Property<string>("HomePhone1")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("HomePhone2")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsMale")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsMilitaryExempted");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MotherName")
                        .HasMaxLength(50);

                    b.Property<int>("NationalityId");

                    b.Property<string>("OthBrief");

                    b.Property<string>("OthFamilyName")
                        .HasMaxLength(50);

                    b.Property<string>("OthFatherName")
                        .HasMaxLength(50);

                    b.Property<string>("OthFirstName")
                        .HasMaxLength(50);

                    b.Property<string>("OthMotherName")
                        .HasMaxLength(50);

                    b.Property<string>("PermenantAddress")
                        .HasMaxLength(256);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<byte[]>("Photo");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("GovernorateId");

                    b.HasIndex("NationalityId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeDocument", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("DocumentTypeId");

                    b.Property<long>("EmployeeId");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Url")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeDocuments");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeFamily", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Contacts")
                        .HasMaxLength(100);

                    b.Property<long>("EmployeeId");

                    b.Property<int>("FamilyMemberTypeId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("FamilyMemberTypeId");

                    b.ToTable("EmployeeFamilies");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeGroup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("EmployeeId");

                    b.Property<int>("GenericSubGroupId");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("GenericSubGroupId");

                    b.ToTable("EmployeeGroups");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeLanguage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("EmployeeId");

                    b.Property<string>("LanguageName")
                        .HasMaxLength(50);

                    b.Property<int>("LanguageTypeId");

                    b.Property<string>("ReadingLevel")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("SpeakingLevel")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("WritingLevel")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LanguageTypeId");

                    b.ToTable("EmployeeLanguages");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeLeave", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ActualFromDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("ActualThruDate")
                        .HasColumnType("date");

                    b.Property<string>("AppForm");

                    b.Property<string>("Details");

                    b.Property<long>("EmployeeId");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsCancelled")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsEndorsed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<int>("LeaveTypeId");

                    b.Property<DateTime>("SubmittedDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("ThruDate")
                        .HasColumnType("date");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LeaveTypeId");

                    b.ToTable("EmployeeLeaves");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeLeaveBalance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnnualEntitlement")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("BalanceDays")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("BalanceHours")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("CarryForward")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<long>("EmployeeId");

                    b.Property<int>("LeaveTypeId");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LeaveTypeId");

                    b.ToTable("EmployeeLeaveBalances");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeePromotion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BasicSalary")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("Details")
                        .HasMaxLength(256);

                    b.Property<DateTime>("EffectiveFromDate")
                        .HasColumnType("date");

                    b.Property<long>("EmploymentId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsCancelled")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsIncreasePercentage")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("JobGradeId");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<int>("PromotionTypeId");

                    b.Property<string>("Remarks")
                        .HasMaxLength(256);

                    b.Property<int>("SalaryIncreaseValue")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int?>("SalaryStepId");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("EmploymentId");

                    b.HasIndex("JobGradeId");

                    b.HasIndex("PromotionTypeId");

                    b.HasIndex("SalaryStepId");

                    b.ToTable("EmployeePromotions");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeQualification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompetencyAreaTypeId");

                    b.Property<int?>("CompetencySubCategoryId");

                    b.Property<long>("EmployeeId");

                    b.Property<bool>("IsPlanned")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ObtainedDate")
                        .HasColumnType("date");

                    b.Property<string>("OthName")
                        .HasMaxLength(100);

                    b.Property<int>("QualificationTypeId");

                    b.HasKey("Id");

                    b.HasIndex("CompetencyAreaTypeId");

                    b.HasIndex("CompetencySubCategoryId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("QualificationTypeId");

                    b.ToTable("EmployeeQualifications");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeSalary", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Details")
                        .HasMaxLength(256);

                    b.Property<long>("EmployeeId");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<int>("TotalSalary");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeSalarys");
                });

            modelBuilder.Entity("HrmsModel.Models.Employment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BasicSalary")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<string>("Details")
                        .HasMaxLength(256);

                    b.Property<long>("EmployeeId");

                    b.Property<int>("EmploymentTypeId");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsActing")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsAttendRequired")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsHead")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsOverTimeAllowed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsPayrollIncluded")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsProbation")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("JobGradeId");

                    b.Property<string>("JobName")
                        .HasMaxLength(100);

                    b.Property<long>("OrgUnitId");

                    b.Property<string>("OthJobName")
                        .HasMaxLength(100);

                    b.Property<long?>("PositionId");

                    b.Property<string>("Remarks")
                        .HasMaxLength(256);

                    b.Property<int?>("SalaryScaleTypeId");

                    b.Property<int?>("SalaryStepId");

                    b.Property<DateTime?>("ThruDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EmploymentTypeId");

                    b.HasIndex("JobGradeId");

                    b.HasIndex("OrgUnitId");

                    b.HasIndex("PositionId");

                    b.HasIndex("SalaryScaleTypeId");

                    b.HasIndex("SalaryStepId");

                    b.ToTable("Employments");
                });

            modelBuilder.Entity("HrmsModel.Models.EmploymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_EmploymentType_SysCode");

                    b.ToTable("EmploymentTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.FamilyMemberType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_FamilyMemberType_SysCode");

                    b.ToTable("FamilyMemberTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.GenericGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.ToTable("GenericGroups");
                });

            modelBuilder.Entity("HrmsModel.Models.GenericSubGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<int>("GenericGroupId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.HasIndex("GenericGroupId");

                    b.ToTable("GenericSubGroups");
                });

            modelBuilder.Entity("HrmsModel.Models.Governorate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.ToTable("Governorates");
                });

            modelBuilder.Entity("HrmsModel.Models.GradeGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.ToTable("GradeGroups");
                });

            modelBuilder.Entity("HrmsModel.Models.Holiday", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FromDay");

                    b.Property<int>("FromMonth");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsHijri")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("NbrDays");

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("HrmsModel.Models.HolidayVariation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CalendarId");

                    b.Property<int>("CompensateNbrDays")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("date");

                    b.Property<long>("HolidayId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsComponsated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Narration")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("NbrDays")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("CalendarId");

                    b.HasIndex("HolidayId");

                    b.ToTable("HolidayVariations");
                });

            modelBuilder.Entity("HrmsModel.Models.JobGrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("GradeGroupId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .HasMaxLength(100);

                    b.Property<int>("SalaryIncrPctg")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.HasIndex("GradeGroupId");

                    b.ToTable("JobGrades");
                });

            modelBuilder.Entity("HrmsModel.Models.LanguageType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_LanguageType_SysCode");

                    b.ToTable("LanguageTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.LeavePolicy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("Description");

                    b.Property<int?>("GradeGroupId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsCompanyPolicy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("JobGradeId");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<int>("LeaveTypeId");

                    b.Property<string>("Remarks");

                    b.Property<int>("TotalDays");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("GradeGroupId");

                    b.HasIndex("JobGradeId");

                    b.HasIndex("LeaveTypeId");

                    b.ToTable("LeavePolicies");
                });

            modelBuilder.Entity("HrmsModel.Models.LeaveType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_LeaveType_SysCode");

                    b.ToTable("LeaveTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.Nationality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.ToTable("Nationalities");
                });

            modelBuilder.Entity("HrmsModel.Models.OrgUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("HeadPositionName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsAttendRequired");

                    b.Property<bool>("IsOverTimeAllowed");

                    b.Property<bool>("IsVacant")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("JobCode")
                        .HasMaxLength(100);

                    b.Property<string>("JobDescription");

                    b.Property<int?>("JobGradeId");

                    b.Property<decimal?>("JobWeight");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<long?>("LineManagerOrgUnitId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("OrgUnitTypeId");

                    b.Property<string>("OthHeadPositionName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<long?>("ReportingToOrgUnitId");

                    b.Property<int?>("SalaryStepId");

                    b.Property<int>("SortOrder");

                    b.Property<int?>("StandardTitleTypeId");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("JobGradeId");

                    b.HasIndex("OrgUnitTypeId");

                    b.HasIndex("SalaryStepId");

                    b.HasIndex("StandardTitleTypeId");

                    b.ToTable("OrgUnits");
                });

            modelBuilder.Entity("HrmsModel.Models.OrgUnitType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_OrgUnitType_SysCode");

                    b.ToTable("OrgUnitTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.Position", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsAttendRequired");

                    b.Property<bool>("IsOverTimeAllowed");

                    b.Property<string>("JobCode")
                        .HasMaxLength(100);

                    b.Property<string>("JobDescription");

                    b.Property<int?>("JobGradeId");

                    b.Property<decimal?>("JobWeight");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<long?>("LineManagerOrgUnitId");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<long>("OrgUnitId");

                    b.Property<string>("OthName")
                        .HasMaxLength(100);

                    b.Property<long?>("ReportingToOrgUnitId");

                    b.Property<int?>("SalaryStepId");

                    b.Property<int?>("StandardTitleTypeId");

                    b.Property<int>("TotalVacant")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("JobGradeId");

                    b.HasIndex("OrgUnitId");

                    b.HasIndex("SalaryStepId");

                    b.HasIndex("StandardTitleTypeId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("HrmsModel.Models.PromotionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_PromotionType_SysCode");

                    b.ToTable("PromotionTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.QualificationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_QualificationType_SysCode");

                    b.ToTable("QualificationTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.SalaryScale", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("date");

                    b.Property<int>("FromJobGradeId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int>("MinSalary");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SalaryScaleTypeId");

                    b.Property<DateTime?>("ThruDate")
                        .HasColumnType("date");

                    b.Property<int>("ThruJobGradeId");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("FromJobGradeId");

                    b.HasIndex("SalaryScaleTypeId");

                    b.HasIndex("ThruJobGradeId");

                    b.ToTable("SalaryScales");
                });

            modelBuilder.Entity("HrmsModel.Models.SalaryScaleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_SalaryScaleType_SysCode");

                    b.ToTable("SalaryScaleTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.SalaryStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .HasMaxLength(100);

                    b.Property<int>("SalaryIncrPctg")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.ToTable("SalarySteps");
                });

            modelBuilder.Entity("HrmsModel.Models.StandardTitleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_StandardTitleType_SysCode");

                    b.ToTable("StandardTitleTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.AllowancePolicy", b =>
                {
                    b.HasOne("HrmsModel.Models.AllowanceType", "AllowanceType")
                        .WithMany("AllowancePolicies")
                        .HasForeignKey("AllowanceTypeId");

                    b.HasOne("HrmsModel.Models.GradeGroup", "GradeGroup")
                        .WithMany("AllowancePolicies")
                        .HasForeignKey("GradeGroupId");

                    b.HasOne("HrmsModel.Models.JobGrade", "JobGrade")
                        .WithMany("AllowancePolicies")
                        .HasForeignKey("JobGradeId");
                });

            modelBuilder.Entity("HrmsModel.Models.Candidate", b =>
                {
                    b.HasOne("HrmsModel.Models.Governorate", "Governorate")
                        .WithMany("Candidates")
                        .HasForeignKey("GovernorateId");

                    b.HasOne("HrmsModel.Models.Nationality", "Nationality")
                        .WithMany("Candidates")
                        .HasForeignKey("NationalityId");

                    b.HasOne("HrmsModel.Models.OrgUnit", "OrgUnit")
                        .WithMany("Candidates")
                        .HasForeignKey("OrgUnitId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HrmsModel.Models.Position", "Position")
                        .WithMany("Candidates")
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("HrmsModel.Models.Competency", b =>
                {
                    b.HasOne("HrmsModel.Models.CompetencySubCategory", "CompetencySubCategory")
                        .WithMany("Competencies")
                        .HasForeignKey("CompetencySubCategoryId");

                    b.HasOne("HrmsModel.Models.GradeGroup", "GradeGroup")
                        .WithMany("Competencies")
                        .HasForeignKey("GradeGroupId");

                    b.HasOne("HrmsModel.Models.JobGrade", "JobGrade")
                        .WithMany("Competencies")
                        .HasForeignKey("JobGradeId");
                });

            modelBuilder.Entity("HrmsModel.Models.CompetencySubCategory", b =>
                {
                    b.HasOne("HrmsModel.Models.CompetencyCategory", "CompetencyCategory")
                        .WithMany("CompetencySubCategories")
                        .HasForeignKey("CompetencyCategoryId");
                });

            modelBuilder.Entity("HrmsModel.Models.Employee", b =>
                {
                    b.HasOne("HrmsModel.Models.Governorate", "Governorate")
                        .WithMany("Employees")
                        .HasForeignKey("GovernorateId");

                    b.HasOne("HrmsModel.Models.Nationality", "Nationality")
                        .WithMany("Employees")
                        .HasForeignKey("NationalityId");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeDocument", b =>
                {
                    b.HasOne("HrmsModel.Models.DocumentType", "DocumentType")
                        .WithMany("EmployeeDocuments")
                        .HasForeignKey("DocumentTypeId");

                    b.HasOne("HrmsModel.Models.Employee", "Employee")
                        .WithMany("EmployeeDocuments")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeFamily", b =>
                {
                    b.HasOne("HrmsModel.Models.Employee", "Employee")
                        .WithMany("EmployeeFamilies")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HrmsModel.Models.FamilyMemberType", "FamilyMemberType")
                        .WithMany("EmployeeFamilies")
                        .HasForeignKey("FamilyMemberTypeId");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeGroup", b =>
                {
                    b.HasOne("HrmsModel.Models.Employee", "Employee")
                        .WithMany("EmployeeGroups")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HrmsModel.Models.GenericSubGroup", "GenericSubGroup")
                        .WithMany("EmployeeGroups")
                        .HasForeignKey("GenericSubGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeLanguage", b =>
                {
                    b.HasOne("HrmsModel.Models.Employee", "Employee")
                        .WithMany("EmployeeLanguages")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HrmsModel.Models.LanguageType", "LanguageType")
                        .WithMany("EmployeeLanguages")
                        .HasForeignKey("LanguageTypeId");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeLeave", b =>
                {
                    b.HasOne("HrmsModel.Models.Employee", "Employee")
                        .WithMany("EmployeeLeaves")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HrmsModel.Models.LeaveType", "LeaveType")
                        .WithMany("EmployeeLeaves")
                        .HasForeignKey("LeaveTypeId");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeLeaveBalance", b =>
                {
                    b.HasOne("HrmsModel.Models.Employee", "Employee")
                        .WithMany("EmployeeLeaveBalances")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HrmsModel.Models.LeaveType", "LeaveType")
                        .WithMany("EmployeeLeaveBalances")
                        .HasForeignKey("LeaveTypeId");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeePromotion", b =>
                {
                    b.HasOne("HrmsModel.Models.Employment", "Employment")
                        .WithMany("EmployeePromotions")
                        .HasForeignKey("EmploymentId");

                    b.HasOne("HrmsModel.Models.JobGrade", "JobGrade")
                        .WithMany("EmployeePromotions")
                        .HasForeignKey("JobGradeId");

                    b.HasOne("HrmsModel.Models.PromotionType", "PromotionType")
                        .WithMany("EmployeePromotions")
                        .HasForeignKey("PromotionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HrmsModel.Models.SalaryStep", "SalaryStep")
                        .WithMany("EmployeePromotions")
                        .HasForeignKey("SalaryStepId");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeQualification", b =>
                {
                    b.HasOne("HrmsModel.Models.CompetencyAreaType", "CompetencyAreaType")
                        .WithMany("EmployeeQualifications")
                        .HasForeignKey("CompetencyAreaTypeId");

                    b.HasOne("HrmsModel.Models.CompetencySubCategory", "CompetencySubCategory")
                        .WithMany("EmployeeQualifications")
                        .HasForeignKey("CompetencySubCategoryId");

                    b.HasOne("HrmsModel.Models.Employee", "Employee")
                        .WithMany("EmployeeQualifications")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HrmsModel.Models.QualificationType", "QualificationType")
                        .WithMany("EmployeeQualifications")
                        .HasForeignKey("QualificationTypeId");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeSalary", b =>
                {
                    b.HasOne("HrmsModel.Models.Employee", "Employee")
                        .WithMany("EmployeeSalaries")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("HrmsModel.Models.Employment", b =>
                {
                    b.HasOne("HrmsModel.Models.Employee", "Employee")
                        .WithMany("Employments")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HrmsModel.Models.EmploymentType", "EmploymentType")
                        .WithMany("Employments")
                        .HasForeignKey("EmploymentTypeId");

                    b.HasOne("HrmsModel.Models.JobGrade", "JobGrade")
                        .WithMany("Employments")
                        .HasForeignKey("JobGradeId");

                    b.HasOne("HrmsModel.Models.OrgUnit", "OrgUnit")
                        .WithMany("Employments")
                        .HasForeignKey("OrgUnitId");

                    b.HasOne("HrmsModel.Models.Position", "Position")
                        .WithMany("Employments")
                        .HasForeignKey("PositionId");

                    b.HasOne("HrmsModel.Models.SalaryScaleType", "SalaryScaleType")
                        .WithMany("Employments")
                        .HasForeignKey("SalaryScaleTypeId");

                    b.HasOne("HrmsModel.Models.SalaryStep", "SalaryStep")
                        .WithMany("Employments")
                        .HasForeignKey("SalaryStepId");
                });

            modelBuilder.Entity("HrmsModel.Models.GenericSubGroup", b =>
                {
                    b.HasOne("HrmsModel.Models.GenericGroup", "GenericGroup")
                        .WithMany("GenericSubGroups")
                        .HasForeignKey("GenericGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HrmsModel.Models.HolidayVariation", b =>
                {
                    b.HasOne("HrmsModel.Models.Calendar", "Calendar")
                        .WithMany("HolidayVariations")
                        .HasForeignKey("CalendarId");

                    b.HasOne("HrmsModel.Models.Holiday", "Holiday")
                        .WithMany("HolidayVariations")
                        .HasForeignKey("HolidayId");
                });

            modelBuilder.Entity("HrmsModel.Models.JobGrade", b =>
                {
                    b.HasOne("HrmsModel.Models.GradeGroup", "GradeGroup")
                        .WithMany("JobGrades")
                        .HasForeignKey("GradeGroupId");
                });

            modelBuilder.Entity("HrmsModel.Models.LeavePolicy", b =>
                {
                    b.HasOne("HrmsModel.Models.GradeGroup", "GradeGroup")
                        .WithMany("LeavePolicies")
                        .HasForeignKey("GradeGroupId");

                    b.HasOne("HrmsModel.Models.JobGrade", "JobGrade")
                        .WithMany("LeavePolicies")
                        .HasForeignKey("JobGradeId");

                    b.HasOne("HrmsModel.Models.LeaveType", "LeaveType")
                        .WithMany("LeavePolicies")
                        .HasForeignKey("LeaveTypeId");
                });

            modelBuilder.Entity("HrmsModel.Models.OrgUnit", b =>
                {
                    b.HasOne("HrmsModel.Models.JobGrade", "JobGrade")
                        .WithMany("OrgUnits")
                        .HasForeignKey("JobGradeId");

                    b.HasOne("HrmsModel.Models.OrgUnitType", "OrgUnitType")
                        .WithMany("OrgUnits")
                        .HasForeignKey("OrgUnitTypeId");

                    b.HasOne("HrmsModel.Models.SalaryStep", "SalaryStep")
                        .WithMany("OrgUnits")
                        .HasForeignKey("SalaryStepId");

                    b.HasOne("HrmsModel.Models.StandardTitleType", "StandardTitleType")
                        .WithMany("OrgUnits")
                        .HasForeignKey("StandardTitleTypeId");
                });

            modelBuilder.Entity("HrmsModel.Models.Position", b =>
                {
                    b.HasOne("HrmsModel.Models.JobGrade", "JobGrade")
                        .WithMany("Positions")
                        .HasForeignKey("JobGradeId");

                    b.HasOne("HrmsModel.Models.OrgUnit", "OrgUnit")
                        .WithMany("Positions")
                        .HasForeignKey("OrgUnitId");

                    b.HasOne("HrmsModel.Models.SalaryStep", "SalaryStep")
                        .WithMany("Positions")
                        .HasForeignKey("SalaryStepId");

                    b.HasOne("HrmsModel.Models.StandardTitleType", "StandardTitleType")
                        .WithMany("Positions")
                        .HasForeignKey("StandardTitleTypeId");
                });

            modelBuilder.Entity("HrmsModel.Models.SalaryScale", b =>
                {
                    b.HasOne("HrmsModel.Models.JobGrade", "FromJobGrade")
                        .WithMany("FromGradeSalaryScales")
                        .HasForeignKey("FromJobGradeId");

                    b.HasOne("HrmsModel.Models.SalaryScaleType", "SalaryScaleType")
                        .WithMany("SalaryScales")
                        .HasForeignKey("SalaryScaleTypeId");

                    b.HasOne("HrmsModel.Models.JobGrade", "ThruJobGrade")
                        .WithMany("ThruGradeSalaryScales")
                        .HasForeignKey("ThruJobGradeId");
                });
        }
    }
}
