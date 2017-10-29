using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HrmsModel.Migrations
{
    public partial class update20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalarys_Employees_EmployeeId",
                table: "EmployeeSalarys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSalarys",
                table: "EmployeeSalarys");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "EmployeeSalarys");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "EmployeeSalarys");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EmployeeSalarys");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "EmployeeSalarys");

            migrationBuilder.DropColumn(
                name: "TotalSalary",
                table: "EmployeeSalarys");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "EmployeeSalarys");

            migrationBuilder.RenameTable(
                name: "EmployeeSalarys",
                newName: "EmployeeSalaries");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSalarys_EmployeeId",
                table: "EmployeeSalaries",
                newName: "IX_EmployeeSalaries_EmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "AllowanceTypeId",
                table: "EmployeeSalaries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BasicSalary",
                table: "EmployeeSalaries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyCode",
                table: "EmployeeSalaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "EmployeePromotionId",
                table: "EmployeeSalaries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmploymentId",
                table: "EmployeeSalaries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IBAN",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSalaries",
                table: "EmployeeSalaries",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OthName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    SysCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.UniqueConstraint("UK_Bank_SysCode", x => x.SysCode);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_AllowanceTypeId",
                table: "EmployeeSalaries",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmployeePromotionId",
                table: "EmployeeSalaries",
                column: "EmployeePromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmploymentId",
                table: "EmployeeSalaries",
                column: "EmploymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaries_AllowanceTypes_AllowanceTypeId",
                table: "EmployeeSalaries",
                column: "AllowanceTypeId",
                principalTable: "AllowanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaries_Employees_EmployeeId",
                table: "EmployeeSalaries",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaries_EmployeePromotions_EmployeePromotionId",
                table: "EmployeeSalaries",
                column: "EmployeePromotionId",
                principalTable: "EmployeePromotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaries_Employments_EmploymentId",
                table: "EmployeeSalaries",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaries_AllowanceTypes_AllowanceTypeId",
                table: "EmployeeSalaries");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaries_Employees_EmployeeId",
                table: "EmployeeSalaries");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaries_EmployeePromotions_EmployeePromotionId",
                table: "EmployeeSalaries");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaries_Employments_EmploymentId",
                table: "EmployeeSalaries");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSalaries",
                table: "EmployeeSalaries");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaries_AllowanceTypeId",
                table: "EmployeeSalaries");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaries_EmployeePromotionId",
                table: "EmployeeSalaries");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaries_EmploymentId",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "AllowanceTypeId",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "BasicSalary",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "EmployeePromotionId",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "EmploymentId",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Branch",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IBAN",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "EmployeeSalaries",
                newName: "EmployeeSalarys");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalarys",
                newName: "IX_EmployeeSalarys_EmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "EmployeeSalarys",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "EmployeeSalarys",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EmployeeSalarys",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "EmployeeSalarys",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalSalary",
                table: "EmployeeSalarys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "EmployeeSalarys",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSalarys",
                table: "EmployeeSalarys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalarys_Employees_EmployeeId",
                table: "EmployeeSalarys",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
