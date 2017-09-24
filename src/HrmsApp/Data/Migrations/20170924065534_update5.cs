using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HrmsApp.Data.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSalary",
                table: "EmployeePositions");

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
                    EmployeePositionId = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: false),
                    IsApproved = table.Column<bool>(nullable: false, defaultValue: false),
                    IsCancelled = table.Column<bool>(nullable: false, defaultValue: false),
                    IsIncreasePercentage = table.Column<bool>(nullable: false, defaultValue: false),
                    JobGradeId = table.Column<int>(nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    Remarks = table.Column<string>(maxLength: 256, nullable: true),
                    SalaryIncreaseValue = table.Column<int>(nullable: false, defaultValue: 0),
                    SalaryScaleTypeId = table.Column<long>(nullable: true),
                    SalaryStepId = table.Column<int>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePromotions_EmployeePositions_EmployeePositionId",
                        column: x => x.EmployeePositionId,
                        principalTable: "EmployeePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePromotions_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalTable: "JobGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePromotions_SalaryScales_SalaryScaleTypeId",
                        column: x => x.SalaryScaleTypeId,
                        principalTable: "SalaryScales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePromotions_SalarySteps_SalaryStepId",
                        column: x => x.SalaryStepId,
                        principalTable: "SalarySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<int>(
                name: "BasicSalary",
                table: "EmployeePositions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobGradeId",
                table: "EmployeePositions",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ThruDate",
                table: "SalaryScales",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "SalaryScales",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "OrgUnits",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "OrgUnits",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "LeavePolicies",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "LeavePolicies",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "EmployeeSalarys",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "EmployeeSalarys",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "EmployeePositions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePositions_JobGradeId",
                table: "EmployeePositions",
                column: "JobGradeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "EmployeeFamilies",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "EmployeeDocuments",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "EmployeeDocuments",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "EmployeeDocuments",
                maxLength: 256,
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "Employees",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Employees",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Competencies",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Competencies",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Candidates",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Candidates",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "AllowancePolicies",
                type: "date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AllowancePolicies",
                type: "date",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotions_EmployeePositionId",
                table: "EmployeePromotions",
                column: "EmployeePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotions_JobGradeId",
                table: "EmployeePromotions",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotions_SalaryScaleTypeId",
                table: "EmployeePromotions",
                column: "SalaryScaleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotions_SalaryStepId",
                table: "EmployeePromotions",
                column: "SalaryStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositions_JobGrades_JobGradeId",
                table: "EmployeePositions",
                column: "JobGradeId",
                principalTable: "JobGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePositions_JobGrades_JobGradeId",
                table: "EmployeePositions");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePositions_JobGradeId",
                table: "EmployeePositions");

            migrationBuilder.DropColumn(
                name: "BasicSalary",
                table: "EmployeePositions");

            migrationBuilder.DropColumn(
                name: "JobGradeId",
                table: "EmployeePositions");

            migrationBuilder.DropTable(
                name: "EmployeePromotions");

            migrationBuilder.AddColumn<int>(
                name: "TotalSalary",
                table: "EmployeePositions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ThruDate",
                table: "SalaryScales",
                type: "Date",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "SalaryScales",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "OrgUnits",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "OrgUnits",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "LeavePolicies",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "LeavePolicies",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "EmployeeSalarys",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FromDate",
                table: "EmployeeSalarys",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "EmployeePositions",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                table: "EmployeeFamilies",
                type: "Date",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "EmployeeDocuments",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "EmployeeDocuments",
                type: "Date",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "EmployeeDocuments",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "Employees",
                type: "Date",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Employees",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Competencies",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Competencies",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Candidates",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Candidates",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "AllowancePolicies",
                type: "Date",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AllowancePolicies",
                type: "Date",
                nullable: false);
        }
    }
}
