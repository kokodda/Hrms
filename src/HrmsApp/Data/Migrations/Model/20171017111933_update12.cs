using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsEndorsed = table.Column<bool>(nullable: false, defaultValue: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceActionTypes",
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
                    table.PrimaryKey("PK_AttendanceActionTypes", x => x.Id);
                    table.UniqueConstraint("UK_AttendanceActionType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "PayrollComponentTypes",
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
                    table.PrimaryKey("PK_PayrollComponentTypes", x => x.Id);
                    table.UniqueConstraint("UK_PayrollComponentType_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttendanceId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsApproved = table.Column<bool>(nullable: false, defaultValue: false),
                    IsEndorsed = table.Column<bool>(nullable: false, defaultValue: false),
                    IsExported = table.Column<bool>(nullable: false, defaultValue: false),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Narration = table.Column<string>(maxLength: 256, nullable: true),
                    SalaryScaleId = table.Column<long>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payrolls_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payrolls_SalaryScales_SalaryScaleId",
                        column: x => x.SalaryScaleId,
                        principalTable: "SalaryScales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttendances",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttendanceActionTypeId = table.Column<int>(nullable: false),
                    AttendanceId = table.Column<long>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: false),
                    FactorValue = table.Column<int>(nullable: false, defaultValue: 1),
                    NbrDays = table.Column<int>(nullable: false, defaultValue: 0),
                    PayrollComponentTypeId = table.Column<int>(nullable: false),
                    RequiredMinutes = table.Column<int>(nullable: false),
                    TotalMinutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendances_AttendanceActionTypes_AttendanceActionTypeId",
                        column: x => x.AttendanceActionTypeId,
                        principalTable: "AttendanceActionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendances_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendances_PayrollComponentTypes_PayrollComponentTypeId",
                        column: x => x.PayrollComponentTypeId,
                        principalTable: "PayrollComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayrollAdditions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<long>(nullable: true),
                    FactorPercent = table.Column<int>(nullable: false),
                    GradeGroupId = table.Column<int>(nullable: true),
                    IsCompanyLevel = table.Column<bool>(nullable: false, defaultValue: true),
                    JobGradeId = table.Column<int>(nullable: true),
                    Narration = table.Column<string>(maxLength: 256, nullable: true),
                    PayrollComponentTypeId = table.Column<int>(nullable: false),
                    PayrollId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollAdditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollAdditions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollAdditions_GradeGroups_GradeGroupId",
                        column: x => x.GradeGroupId,
                        principalTable: "GradeGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollAdditions_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollAdditions_PayrollComponentTypes_PayrollComponentTypeId",
                        column: x => x.PayrollComponentTypeId,
                        principalTable: "PayrollComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollAdditions_Payrolls_PayrollId",
                        column: x => x.PayrollId,
                        principalTable: "Payrolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayrollDeductions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<long>(nullable: false),
                    IsPercentage = table.Column<bool>(nullable: false, defaultValue: false),
                    Narration = table.Column<string>(maxLength: 256, nullable: true),
                    NbrDays = table.Column<int>(nullable: false, defaultValue: 0),
                    PayrollComponentTypeId = table.Column<int>(nullable: false),
                    PayrollId = table.Column<long>(nullable: false),
                    Value = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollDeductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollDeductions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollDeductions_PayrollComponentTypes_PayrollComponentTypeId",
                        column: x => x.PayrollComponentTypeId,
                        principalTable: "PayrollComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollDeductions_Payrolls_PayrollId",
                        column: x => x.PayrollId,
                        principalTable: "Payrolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayrollEmployees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: false),
                    EmploymentId = table.Column<long>(nullable: false),
                    FromDate = table.Column<DateTime>(type: "date", nullable: true),
                    PayrollComponentTypeId = table.Column<int>(nullable: false),
                    PayrollId = table.Column<long>(nullable: false),
                    ThruDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollEmployees_Employments_EmploymentId",
                        column: x => x.EmploymentId,
                        principalTable: "Employments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollEmployees_PayrollComponentTypes_PayrollComponentTypeId",
                        column: x => x.PayrollComponentTypeId,
                        principalTable: "PayrollComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollEmployees_Payrolls_PayrollId",
                        column: x => x.PayrollId,
                        principalTable: "Payrolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendances_AttendanceActionTypeId",
                table: "EmployeeAttendances",
                column: "AttendanceActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendances_AttendanceId",
                table: "EmployeeAttendances",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendances_EmployeeId",
                table: "EmployeeAttendances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendances_PayrollComponentTypeId",
                table: "EmployeeAttendances",
                column: "PayrollComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_AttendanceId",
                table: "Payrolls",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_SalaryScaleId",
                table: "Payrolls",
                column: "SalaryScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_EmployeeId",
                table: "PayrollAdditions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_GradeGroupId",
                table: "PayrollAdditions",
                column: "GradeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_JobGradeId",
                table: "PayrollAdditions",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_PayrollComponentTypeId",
                table: "PayrollAdditions",
                column: "PayrollComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_PayrollId",
                table: "PayrollAdditions",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductions_EmployeeId",
                table: "PayrollDeductions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductions_PayrollComponentTypeId",
                table: "PayrollDeductions",
                column: "PayrollComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollDeductions_PayrollId",
                table: "PayrollDeductions",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollEmployees_EmployeeId",
                table: "PayrollEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollEmployees_EmploymentId",
                table: "PayrollEmployees",
                column: "EmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollEmployees_PayrollComponentTypeId",
                table: "PayrollEmployees",
                column: "PayrollComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollEmployees_PayrollId",
                table: "PayrollEmployees",
                column: "PayrollId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAttendances");

            migrationBuilder.DropTable(
                name: "PayrollAdditions");

            migrationBuilder.DropTable(
                name: "PayrollDeductions");

            migrationBuilder.DropTable(
                name: "PayrollEmployees");

            migrationBuilder.DropTable(
                name: "AttendanceActionTypes");

            migrationBuilder.DropTable(
                name: "PayrollComponentTypes");

            migrationBuilder.DropTable(
                name: "Payrolls");

            migrationBuilder.DropTable(
                name: "Attendances");
        }
    }
}
