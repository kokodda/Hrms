using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HrmsModel.Data;

namespace HrmsApp.Data.Migrations
{
    [DbContext(typeof(HrmsDbContext))]
    [Migration("20170920044911_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HrmsModel.Models.AllowancePolicy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AllowanceTypeId");

                    b.Property<int>("Amount");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("Date");

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
                        .HasColumnType("Date");

                    b.Property<string>("Remarks");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_AllowanceType_SysCode");

                    b.ToTable("AllowanceTypes");
                });

            modelBuilder.Entity("HrmsModel.Models.Candidate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("AppForm");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("Date");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("GovernorateId");

                    b.Property<string>("HomePhone1")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("HomePhone2")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<bool>("IsMale")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsMilitaryExempted");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("Date");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("MotherName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("NationalityId");

                    b.Property<long>("OrgUnitId");

                    b.Property<string>("OthFamilyName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("OthFatherName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("OthFirstName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("OthMotherName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("PermenantAddress")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("GovernorateId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("OrgUnitId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("HrmsModel.Models.Competency", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompetencySubCategoryId");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("Date");

                    b.Property<int?>("GradeGroupId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsCompanyPolicy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("JobGradeId");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("Date");

                    b.Property<string>("Remarks");

                    b.Property<string>("Requirements");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 256);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("Date");

                    b.Property<string>("BloodGroup")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("EmpUid");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("GovernorateId");

                    b.Property<string>("HomePhone1")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("HomePhone2")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsMale")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsMilitaryExempted");

                    b.Property<DateTime?>("JoinDate")
                        .HasColumnType("Date");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("Date");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("MotherName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("NationalityId");

                    b.Property<string>("OthFamilyName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("OthFatherName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("OthFirstName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("OthMotherName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("PermenantAddress")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<byte[]>("Photo");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 256);

                    b.Property<int>("DocumentTypeId");

                    b.Property<long>("EmployeeId");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("Date");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Url")
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeDocuments");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeFamily", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BirthDate")
                        .HasColumnType("Date");

                    b.Property<string>("Contacts")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<long>("EmployeeId");

                    b.Property<int>("FamilyMemberTypeId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("LanguageTypeId");

                    b.Property<string>("ReadingLevel")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("SpeakingLevel")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("WritingLevel")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LanguageTypeId");

                    b.ToTable("EmployeeLanguages");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeLeave", b =>
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

                    b.ToTable("EmployeeLeaves");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeePosition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Details")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<long>("EmployeeId");

                    b.Property<int>("EmployeeTypeId");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsActing")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAttendRequired")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsOverTimeAllowed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsProbation")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<long?>("OrgUnitId");

                    b.Property<string>("OthName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Remarks")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<long?>("SalaryScaleTypeId");

                    b.Property<int?>("SalaryStepId");

                    b.Property<DateTime?>("ThruDate")
                        .HasColumnType("date");

                    b.Property<int>("TotalSalary")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EmployeeTypeId");

                    b.HasIndex("OrgUnitId");

                    b.HasIndex("SalaryScaleTypeId");

                    b.HasIndex("SalaryStepId");

                    b.ToTable("EmployeePositions");
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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime>("ObtainedDate")
                        .HasColumnType("date");

                    b.Property<string>("OthName")
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 256);

                    b.Property<long>("EmployeeId");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("Date");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("Date");

                    b.Property<int>("TotalSalary");

                    b.Property<string>("UpdatedBy")
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeSalarys");
                });

            modelBuilder.Entity("HrmsModel.Models.EmployeeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_EmployeeType_SysCode");

                    b.ToTable("EmployeeTypes");
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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.ToTable("GenericGroups");
                });

            modelBuilder.Entity("HrmsModel.Models.GenericSubGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<int>("GenericGroupId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 10);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.ToTable("GradeGroups");
                });

            modelBuilder.Entity("HrmsModel.Models.JobGrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<int>("GradeGroupId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasColumnType("Date");

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
                        .HasColumnType("Date");

                    b.Property<int>("LeaveTypeId");

                    b.Property<string>("Remarks");

                    b.Property<int>("TotalDays");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("SortOrder");

                    b.HasKey("Id");

                    b.ToTable("Nationalities");
                });

            modelBuilder.Entity("HrmsModel.Models.OrgUnit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("Date");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("JobCode")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("JobDescription");

                    b.Property<int?>("JobGradeId");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("Date");

                    b.Property<long?>("LineManagerOrgUnitId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("OrgUnitTypeId");

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthPositionName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("SortOrder");

                    b.Property<int?>("StandardTitleTypeId");

                    b.Property<int>("TotalStaff")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("TotalVacant")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("JobGradeId");

                    b.HasIndex("OrgUnitTypeId");

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("SortOrder");

                    b.Property<string>("SysCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SysCode")
                        .HasName("UK_OrgUnitType_SysCode");

                    b.ToTable("OrgUnitTypes");
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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 3);

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("Date");

                    b.Property<int>("FromJobGradeId");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<int>("MinSalary");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("SalaryScaleTypeId");

                    b.Property<DateTime?>("ThruDate")
                        .HasColumnType("Date");

                    b.Property<int>("ThruJobGradeId");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 10);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .HasAnnotation("MaxLength", 100);

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
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OthName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

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

            modelBuilder.Entity("HrmsModel.Models.EmployeePosition", b =>
                {
                    b.HasOne("HrmsModel.Models.Employee", "Employee")
                        .WithMany("EmployeePositions")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("HrmsModel.Models.EmployeeType", "EmployeeType")
                        .WithMany("EmployeePositions")
                        .HasForeignKey("EmployeeTypeId");

                    b.HasOne("HrmsModel.Models.OrgUnit", "OrgUnit")
                        .WithMany("EmployeePositions")
                        .HasForeignKey("OrgUnitId");

                    b.HasOne("HrmsModel.Models.SalaryScale", "SalaryScale")
                        .WithMany("EmployeePositions")
                        .HasForeignKey("SalaryScaleTypeId");

                    b.HasOne("HrmsModel.Models.SalaryStep", "SalaryStep")
                        .WithMany("EmployeePositions")
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

            modelBuilder.Entity("HrmsModel.Models.GenericSubGroup", b =>
                {
                    b.HasOne("HrmsModel.Models.GenericGroup", "GenericGroup")
                        .WithMany("GenericSubGroups")
                        .HasForeignKey("GenericGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
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

                    b.HasOne("HrmsModel.Models.StandardTitleType", "StandardTitleType")
                        .WithMany("OrgUnits")
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
