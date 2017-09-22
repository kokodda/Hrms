using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HrmsApp.Data.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualEntitlement",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "BalanceDays",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "BalanceHours",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "CarryForward",
                table: "EmployeeLeaves");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualFromDate",
                table: "EmployeeLeaves",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualThruDate",
                table: "EmployeeLeaves",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "EmployeeLeaves",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "EmployeeLeaves",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEndorsed",
                table: "EmployeeLeaves",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "EmployeeLeaves",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedDate",
                table: "EmployeeLeaves",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ThruDate",
                table: "EmployeeLeaves",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedBy",
                table: "EmployeeLeaves",
                maxLength: 100,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveBalances_EmployeeId",
                table: "EmployeeLeaveBalances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveBalances_LeaveTypeId",
                table: "EmployeeLeaveBalances",
                column: "LeaveTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualFromDate",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "ActualThruDate",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "IsEndorsed",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "ThruDate",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "EmployeeLeaves");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveBalances");

            migrationBuilder.AddColumn<int>(
                name: "AnnualEntitlement",
                table: "EmployeeLeaves",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BalanceDays",
                table: "EmployeeLeaves",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BalanceHours",
                table: "EmployeeLeaves",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarryForward",
                table: "EmployeeLeaves",
                nullable: false,
                defaultValue: 0);
        }
    }
}
