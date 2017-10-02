using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllowanceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowanceTypes", x => x.Id);
                    table.UniqueConstraint("UK_AllowanceType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyAreaTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyAreaTypes", x => x.Id);
                    table.UniqueConstraint("UK_CompetencyAreaType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                    table.UniqueConstraint("UK_DocumentType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentTypes", x => x.Id);
                    table.UniqueConstraint("UK_EmploymentType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMemberTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMemberTypes", x => x.Id);
                    table.UniqueConstraint("UK_FamilyMemberType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "GenericGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Governorates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    OthName = table.Column<string>(maxLength: 100, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTypes", x => x.Id);
                    table.UniqueConstraint("UK_LanguageType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                    table.UniqueConstraint("UK_LeaveType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrgUnitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgUnitTypes", x => x.Id);
                    table.UniqueConstraint("UK_OrgUnitType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "PromotionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionTypes", x => x.Id);
                    table.UniqueConstraint("UK_PromotionType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "QualificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationTypes", x => x.Id);
                    table.UniqueConstraint("UK_QualificationType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "SalaryScaleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryScaleTypes", x => x.Id);
                    table.UniqueConstraint("UK_SalaryScaleType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "SalarySteps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    OthName = table.Column<string>(maxLength: 100, nullable: true),
                    SalaryIncrPctg = table.Column<int>(nullable: false, defaultValue: 0),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarySteps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StandardTitleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    SysCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardTitleTypes", x => x.Id);
                    table.UniqueConstraint("UK_StandardTitleType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "CompetencySubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetencyCategoryId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencySubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetencySubCategories_CompetencyCategories_CompetencyCategoryId",
                        column: x => x.CompetencyCategoryId,
                        principalTable: "CompetencyCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GenericSubGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    GenericGroupId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericSubGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenericSubGroups_GenericGroups_GenericGroupId",
                        column: x => x.GenericGroupId,
                        principalTable: "GenericGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobGrades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    GradeGroupId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    OthName = table.Column<string>(maxLength: 100, nullable: true),
                    SalaryIncrPctg = table.Column<int>(nullable: false, defaultValue: 0),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobGrades_GradeGroups_GradeGroupId",
                        column: x => x.GradeGroupId,
                        principalTable: "GradeGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    BloodGroup = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    EmpUid = table.Column<string>(nullable: true),
                    FamilyName = table.Column<string>(maxLength: 50, nullable: false),
                    FatherName = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    GovernorateId = table.Column<int>(nullable: false),
                    HomePhone1 = table.Column<string>(maxLength: 50, nullable: false),
                    HomePhone2 = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: false),
                    IsMale = table.Column<bool>(nullable: false, defaultValue: true),
                    IsMilitaryExempted = table.Column<bool>(nullable: false),
                    JoinDate = table.Column<DateTime>(type: "date", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    MaritalStatus = table.Column<string>(maxLength: 50, nullable: false),
                    MotherName = table.Column<string>(maxLength: 50, nullable: true),
                    NationalityId = table.Column<int>(nullable: false),
                    OthFamilyName = table.Column<string>(maxLength: 50, nullable: true),
                    OthFatherName = table.Column<string>(maxLength: 50, nullable: true),
                    OthFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    OthMotherName = table.Column<string>(maxLength: 50, nullable: true),
                    PermenantAddress = table.Column<string>(maxLength: 256, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    Photo = table.Column<byte[]>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AllowancePolicies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowanceTypeId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GradeGroupId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsCompanyPolicy = table.Column<bool>(nullable: false, defaultValue: false),
                    JobGradeId = table.Column<int>(nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false),
                    isPercentage = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowancePolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllowancePolicies_AllowanceTypes_AllowanceTypeId",
                        column: x => x.AllowanceTypeId,
                        principalTable: "AllowanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllowancePolicies_GradeGroups_GradeGroupId",
                        column: x => x.GradeGroupId,
                        principalTable: "GradeGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllowancePolicies_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Competencies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetencySubCategoryId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    GradeGroupId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsCompanyPolicy = table.Column<bool>(nullable: false, defaultValue: false),
                    JobGradeId = table.Column<int>(nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Requirements = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competencies_CompetencySubCategories_CompetencySubCategoryId",
                        column: x => x.CompetencySubCategoryId,
                        principalTable: "CompetencySubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competencies_GradeGroups_GradeGroupId",
                        column: x => x.GradeGroupId,
                        principalTable: "GradeGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competencies_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeavePolicies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GradeGroupId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsCompanyPolicy = table.Column<bool>(nullable: false, defaultValue: false),
                    JobGradeId = table.Column<int>(nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    LeaveTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    TotalDays = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavePolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeavePolicies_GradeGroups_GradeGroupId",
                        column: x => x.GradeGroupId,
                        principalTable: "GradeGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeavePolicies_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeavePolicies_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrgUnits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    HeadPositionName = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsAttendRequired = table.Column<bool>(nullable: false),
                    IsOverTimeAllowed = table.Column<bool>(nullable: false),
                    IsVacant = table.Column<bool>(nullable: false, defaultValue: false),
                    JobCode = table.Column<string>(maxLength: 100, nullable: true),
                    JobDescription = table.Column<string>(nullable: true),
                    JobGradeId = table.Column<int>(nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    LineManagerOrgUnitId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OrgUnitTypeId = table.Column<int>(nullable: false),
                    OthHeadPositionName = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    ReportingToOrgUnitId = table.Column<long>(nullable: true),
                    SalaryStepId = table.Column<int>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    StandardTitleTypeId = table.Column<int>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgUnits_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrgUnits_OrgUnitTypes_OrgUnitTypeId",
                        column: x => x.OrgUnitTypeId,
                        principalTable: "OrgUnitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrgUnits_SalarySteps_SalaryStepId",
                        column: x => x.SalaryStepId,
                        principalTable: "SalarySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrgUnits_StandardTitleTypes_StandardTitleTypeId",
                        column: x => x.StandardTitleTypeId,
                        principalTable: "StandardTitleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalaryScales",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrencyCode = table.Column<string>(maxLength: 3, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    FromJobGradeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    MinSalary = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    SalaryScaleTypeId = table.Column<int>(nullable: false),
                    ThruDate = table.Column<DateTime>(type: "date", nullable: true),
                    ThruJobGradeId = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryScales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryScales_JobGrades_FromJobGradeId",
                        column: x => x.FromJobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryScales_SalaryScaleTypes_SalaryScaleTypeId",
                        column: x => x.SalaryScaleTypeId,
                        principalTable: "SalaryScaleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryScales_JobGrades_ThruJobGradeId",
                        column: x => x.ThruJobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Details = table.Column<string>(maxLength: 256, nullable: false),
                    DocumentTypeId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Url = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDocuments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDocuments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeFamilies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Contacts = table.Column<string>(maxLength: 100, nullable: true),
                    EmployeeId = table.Column<long>(nullable: false),
                    FamilyMemberTypeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFamilies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeFamilies_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeFamilies_FamilyMemberTypes_FamilyMemberTypeId",
                        column: x => x.FamilyMemberTypeId,
                        principalTable: "FamilyMemberTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<long>(nullable: false),
                    GenericSubGroupId = table.Column<int>(nullable: false),
                    JoinDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeGroups_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeGroups_GenericSubGroups_GenericSubGroupId",
                        column: x => x.GenericSubGroupId,
                        principalTable: "GenericSubGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLanguages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<long>(nullable: false),
                    LanguageName = table.Column<string>(maxLength: 50, nullable: true),
                    LanguageTypeId = table.Column<int>(nullable: false),
                    ReadingLevel = table.Column<string>(maxLength: 20, nullable: false),
                    SpeakingLevel = table.Column<string>(maxLength: 20, nullable: false),
                    WritingLevel = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLanguages_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeLanguages_LanguageTypes_LanguageTypeId",
                        column: x => x.LanguageTypeId,
                        principalTable: "LanguageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaves",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActualFromDate = table.Column<DateTime>(type: "date", nullable: true),
                    ActualThruDate = table.Column<DateTime>(type: "date", nullable: true),
                    AppForm = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<long>(nullable: false),
                    FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsApproved = table.Column<bool>(nullable: false, defaultValue: false),
                    IsCancelled = table.Column<bool>(nullable: false, defaultValue: false),
                    IsEndorsed = table.Column<bool>(nullable: false, defaultValue: false),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    LeaveTypeId = table.Column<int>(nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "date", nullable: false),
                    ThruDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaves_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaves_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveBalances",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnnualEntitlement = table.Column<int>(nullable: false, defaultValue: 0),
                    BalanceDays = table.Column<int>(nullable: false, defaultValue: 0),
                    BalanceHours = table.Column<int>(nullable: false, defaultValue: 0),
                    CarryForward = table.Column<int>(nullable: false, defaultValue: 0),
                    EmployeeId = table.Column<long>(nullable: false),
                    LeaveTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveBalances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveBalances_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeQualifications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetencyAreaTypeId = table.Column<int>(nullable: false),
                    CompetencySubCategoryId = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<long>(nullable: false),
                    IsPlanned = table.Column<bool>(nullable: false, defaultValue: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ObtainedDate = table.Column<DateTime>(type: "date", nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: true),
                    QualificationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeQualifications_CompetencyAreaTypes_CompetencyAreaTypeId",
                        column: x => x.CompetencyAreaTypeId,
                        principalTable: "CompetencyAreaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeQualifications_CompetencySubCategories_CompetencySubCategoryId",
                        column: x => x.CompetencySubCategoryId,
                        principalTable: "CompetencySubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeQualifications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeQualifications_QualificationTypes_QualificationTypeId",
                        column: x => x.QualificationTypeId,
                        principalTable: "QualificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalarys",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Details = table.Column<string>(maxLength: 256, nullable: true),
                    EmployeeId = table.Column<long>(nullable: false),
                    FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    TotalSalary = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalarys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSalarys_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Capacity = table.Column<int>(nullable: false, defaultValue: 1),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsAttendRequired = table.Column<bool>(nullable: false),
                    IsOverTimeAllowed = table.Column<bool>(nullable: false),
                    JobCode = table.Column<string>(maxLength: 100, nullable: true),
                    JobDescription = table.Column<string>(nullable: true),
                    JobGradeId = table.Column<int>(nullable: true),
                    JobWeight = table.Column<decimal>(nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    LineManagerOrgUnitId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    OrgUnitId = table.Column<long>(nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: true),
                    ReportingToOrgUnitId = table.Column<long>(nullable: true),
                    SalaryStepId = table.Column<int>(nullable: true),
                    StandardTitleTypeId = table.Column<int>(nullable: true),
                    TotalStaff = table.Column<int>(nullable: false, defaultValue: 0),
                    TotalVacant = table.Column<int>(nullable: false, defaultValue: 1),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Positions_OrgUnits_OrgUnitId",
                        column: x => x.OrgUnitId,
                        principalTable: "OrgUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Positions_SalarySteps_SalaryStepId",
                        column: x => x.SalaryStepId,
                        principalTable: "SalarySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Positions_StandardTitleTypes_StandardTitleTypeId",
                        column: x => x.StandardTitleTypeId,
                        principalTable: "StandardTitleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    AppForm = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    FamilyName = table.Column<string>(maxLength: 50, nullable: false),
                    FatherName = table.Column<string>(maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    GovernorateId = table.Column<int>(nullable: false),
                    HomePhone1 = table.Column<string>(maxLength: 50, nullable: false),
                    HomePhone2 = table.Column<string>(maxLength: 50, nullable: true),
                    IsHead = table.Column<bool>(nullable: false),
                    IsMale = table.Column<bool>(nullable: false, defaultValue: true),
                    IsMilitaryExempted = table.Column<bool>(nullable: false),
                    JobName = table.Column<string>(nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    MaritalStatus = table.Column<string>(maxLength: 50, nullable: false),
                    MotherName = table.Column<string>(maxLength: 50, nullable: true),
                    NationalityId = table.Column<int>(nullable: false),
                    OrgUnitId = table.Column<long>(nullable: false),
                    OthFamilyName = table.Column<string>(maxLength: 50, nullable: true),
                    OthFatherName = table.Column<string>(maxLength: 50, nullable: true),
                    OthFirstName = table.Column<string>(maxLength: 50, nullable: true),
                    OthJobName = table.Column<string>(nullable: true),
                    OthMotherName = table.Column<string>(maxLength: 50, nullable: true),
                    PermenantAddress = table.Column<string>(maxLength: 256, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    PositionId = table.Column<long>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidates_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidates_OrgUnits_OrgUnitId",
                        column: x => x.OrgUnitId,
                        principalTable: "OrgUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidates_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BasicSalary = table.Column<int>(nullable: false, defaultValue: 0),
                    Details = table.Column<string>(maxLength: 256, nullable: true),
                    EmployeeId = table.Column<long>(nullable: false),
                    EmploymentTypeId = table.Column<int>(nullable: false),
                    FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsActing = table.Column<bool>(nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: false),
                    IsAttendRequired = table.Column<bool>(nullable: false, defaultValue: false),
                    IsHead = table.Column<bool>(nullable: false, defaultValue: false),
                    IsOverTimeAllowed = table.Column<bool>(nullable: false, defaultValue: false),
                    IsProbation = table.Column<bool>(nullable: false, defaultValue: false),
                    JobGradeId = table.Column<int>(nullable: true),
                    JobName = table.Column<string>(maxLength: 100, nullable: true),
                    OrgUnitId = table.Column<long>(nullable: false),
                    OthJobName = table.Column<string>(maxLength: 100, nullable: true),
                    PositionId = table.Column<long>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 256, nullable: true),
                    SalaryScaleTypeId = table.Column<int>(nullable: true),
                    SalaryStepId = table.Column<int>(nullable: true),
                    ThruDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employments_EmploymentTypes_EmploymentTypeId",
                        column: x => x.EmploymentTypeId,
                        principalTable: "EmploymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employments_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employments_OrgUnits_OrgUnitId",
                        column: x => x.OrgUnitId,
                        principalTable: "OrgUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employments_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employments_SalaryScaleTypes_SalaryScaleTypeId",
                        column: x => x.SalaryScaleTypeId,
                        principalTable: "SalaryScaleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employments_SalarySteps_SalaryStepId",
                        column: x => x.SalaryStepId,
                        principalTable: "SalarySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePromotions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BasicSalary = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Details = table.Column<string>(maxLength: 256, nullable: true),
                    EffectiveFromDate = table.Column<DateTime>(type: "date", nullable: false),
                    EmploymentId = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: false),
                    IsApproved = table.Column<bool>(nullable: false, defaultValue: false),
                    IsCancelled = table.Column<bool>(nullable: false, defaultValue: false),
                    IsIncreasePercentage = table.Column<bool>(nullable: false, defaultValue: false),
                    JobGradeId = table.Column<int>(nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    PromotionTypeId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 256, nullable: true),
                    SalaryIncreaseValue = table.Column<int>(nullable: false, defaultValue: 0),
                    SalaryScaleTypeId = table.Column<int>(nullable: true),
                    SalaryStepId = table.Column<int>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePromotions_Employments_EmploymentId",
                        column: x => x.EmploymentId,
                        principalTable: "Employments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePromotions_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePromotions_PromotionTypes_PromotionTypeId",
                        column: x => x.PromotionTypeId,
                        principalTable: "PromotionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePromotions_SalaryScaleTypes_SalaryScaleTypeId",
                        column: x => x.SalaryScaleTypeId,
                        principalTable: "SalaryScaleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePromotions_SalarySteps_SalaryStepId",
                        column: x => x.SalaryStepId,
                        principalTable: "SalarySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllowancePolicies_AllowanceTypeId",
                table: "AllowancePolicies",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AllowancePolicies_GradeGroupId",
                table: "AllowancePolicies",
                column: "GradeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AllowancePolicies_JobGradeId",
                table: "AllowancePolicies",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_GovernorateId",
                table: "Candidates",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_NationalityId",
                table: "Candidates",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_OrgUnitId",
                table: "Candidates",
                column: "OrgUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PositionId",
                table: "Candidates",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_CompetencySubCategoryId",
                table: "Competencies",
                column: "CompetencySubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_GradeGroupId",
                table: "Competencies",
                column: "GradeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_JobGradeId",
                table: "Competencies",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencySubCategories_CompetencyCategoryId",
                table: "CompetencySubCategories",
                column: "CompetencyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GovernorateId",
                table: "Employees",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NationalityId",
                table: "Employees",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_DocumentTypeId",
                table: "EmployeeDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_EmployeeId",
                table: "EmployeeDocuments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFamilies_EmployeeId",
                table: "EmployeeFamilies",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFamilies_FamilyMemberTypeId",
                table: "EmployeeFamilies",
                column: "FamilyMemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGroups_EmployeeId",
                table: "EmployeeGroups",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGroups_GenericSubGroupId",
                table: "EmployeeGroups",
                column: "GenericSubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLanguages_EmployeeId",
                table: "EmployeeLanguages",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLanguages_LanguageTypeId",
                table: "EmployeeLanguages",
                column: "LanguageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_EmployeeId",
                table: "EmployeeLeaves",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaves_LeaveTypeId",
                table: "EmployeeLeaves",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveBalances_EmployeeId",
                table: "EmployeeLeaveBalances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveBalances_LeaveTypeId",
                table: "EmployeeLeaveBalances",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotions_EmploymentId",
                table: "EmployeePromotions",
                column: "EmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotions_JobGradeId",
                table: "EmployeePromotions",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotions_PromotionTypeId",
                table: "EmployeePromotions",
                column: "PromotionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotions_SalaryScaleTypeId",
                table: "EmployeePromotions",
                column: "SalaryScaleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotions_SalaryStepId",
                table: "EmployeePromotions",
                column: "SalaryStepId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualifications_CompetencyAreaTypeId",
                table: "EmployeeQualifications",
                column: "CompetencyAreaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualifications_CompetencySubCategoryId",
                table: "EmployeeQualifications",
                column: "CompetencySubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualifications_EmployeeId",
                table: "EmployeeQualifications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualifications_QualificationTypeId",
                table: "EmployeeQualifications",
                column: "QualificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalarys_EmployeeId",
                table: "EmployeeSalarys",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_EmployeeId",
                table: "Employments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_EmploymentTypeId",
                table: "Employments",
                column: "EmploymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_JobGradeId",
                table: "Employments",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_OrgUnitId",
                table: "Employments",
                column: "OrgUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_PositionId",
                table: "Employments",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_SalaryScaleTypeId",
                table: "Employments",
                column: "SalaryScaleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_SalaryStepId",
                table: "Employments",
                column: "SalaryStepId");

            migrationBuilder.CreateIndex(
                name: "IX_GenericSubGroups_GenericGroupId",
                table: "GenericSubGroups",
                column: "GenericGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_JobGrades_GradeGroupId",
                table: "JobGrades",
                column: "GradeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavePolicies_GradeGroupId",
                table: "LeavePolicies",
                column: "GradeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavePolicies_JobGradeId",
                table: "LeavePolicies",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavePolicies_LeaveTypeId",
                table: "LeavePolicies",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_JobGradeId",
                table: "OrgUnits",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_OrgUnitTypeId",
                table: "OrgUnits",
                column: "OrgUnitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_SalaryStepId",
                table: "OrgUnits",
                column: "SalaryStepId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_StandardTitleTypeId",
                table: "OrgUnits",
                column: "StandardTitleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_JobGradeId",
                table: "Positions",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_OrgUnitId",
                table: "Positions",
                column: "OrgUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SalaryStepId",
                table: "Positions",
                column: "SalaryStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_StandardTitleTypeId",
                table: "Positions",
                column: "StandardTitleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryScales_FromJobGradeId",
                table: "SalaryScales",
                column: "FromJobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryScales_SalaryScaleTypeId",
                table: "SalaryScales",
                column: "SalaryScaleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryScales_ThruJobGradeId",
                table: "SalaryScales",
                column: "ThruJobGradeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowancePolicies");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Competencies");

            migrationBuilder.DropTable(
                name: "EmployeeDocuments");

            migrationBuilder.DropTable(
                name: "EmployeeFamilies");

            migrationBuilder.DropTable(
                name: "EmployeeGroups");

            migrationBuilder.DropTable(
                name: "EmployeeLanguages");

            migrationBuilder.DropTable(
                name: "EmployeeLeaves");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveBalances");

            migrationBuilder.DropTable(
                name: "EmployeePromotions");

            migrationBuilder.DropTable(
                name: "EmployeeQualifications");

            migrationBuilder.DropTable(
                name: "EmployeeSalarys");

            migrationBuilder.DropTable(
                name: "LeavePolicies");

            migrationBuilder.DropTable(
                name: "SalaryScales");

            migrationBuilder.DropTable(
                name: "AllowanceTypes");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "FamilyMemberTypes");

            migrationBuilder.DropTable(
                name: "GenericSubGroups");

            migrationBuilder.DropTable(
                name: "LanguageTypes");

            migrationBuilder.DropTable(
                name: "Employments");

            migrationBuilder.DropTable(
                name: "PromotionTypes");

            migrationBuilder.DropTable(
                name: "CompetencyAreaTypes");

            migrationBuilder.DropTable(
                name: "CompetencySubCategories");

            migrationBuilder.DropTable(
                name: "QualificationTypes");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "GenericGroups");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmploymentTypes");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "SalaryScaleTypes");

            migrationBuilder.DropTable(
                name: "CompetencyCategories");

            migrationBuilder.DropTable(
                name: "Governorates");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "OrgUnits");

            migrationBuilder.DropTable(
                name: "JobGrades");

            migrationBuilder.DropTable(
                name: "OrgUnitTypes");

            migrationBuilder.DropTable(
                name: "SalarySteps");

            migrationBuilder.DropTable(
                name: "StandardTitleTypes");

            migrationBuilder.DropTable(
                name: "GradeGroups");
        }
    }
}
