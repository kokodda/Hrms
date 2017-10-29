using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HrmsModel.Migrations
{
    public partial class update21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSalaries");

            migrationBuilder.DropUniqueConstraint(
                name: "UK_Bank_SysCode",
                table: "Banks");

            migrationBuilder.AlterColumn<string>(
                name: "SysCode",
                table: "Banks",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Remunerations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowanceTypeId = table.Column<int>(type: "int", nullable: false),
                    AllowanceValue = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    EmploymentId = table.Column<long>(type: "bigint", nullable: false),
                    FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remunerations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remunerations_AllowanceTypes_AllowanceTypeId",
                        column: x => x.AllowanceTypeId,
                        principalTable: "AllowanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Remunerations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Remunerations_Employments_EmploymentId",
                        column: x => x.EmploymentId,
                        principalTable: "Employments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remunerations_AllowanceTypeId",
                table: "Remunerations",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Remunerations_EmployeeId",
                table: "Remunerations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Remunerations_EmploymentId",
                table: "Remunerations",
                column: "EmploymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remunerations");

            migrationBuilder.AlterColumn<string>(
                name: "SysCode",
                table: "Banks",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "UK_Bank_SysCode",
                table: "Banks",
                column: "SysCode");

            migrationBuilder.CreateTable(
                name: "EmployeeSalaries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowanceTypeId = table.Column<int>(nullable: true),
                    BasicSalary = table.Column<int>(nullable: true),
                    CurrencyCode = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: false),
                    EmployeePromotionId = table.Column<long>(nullable: false),
                    EmploymentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaries_AllowanceTypes_AllowanceTypeId",
                        column: x => x.AllowanceTypeId,
                        principalTable: "AllowanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaries_EmployeePromotions_EmployeePromotionId",
                        column: x => x.EmployeePromotionId,
                        principalTable: "EmployeePromotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaries_Employments_EmploymentId",
                        column: x => x.EmploymentId,
                        principalTable: "Employments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_AllowanceTypeId",
                table: "EmployeeSalaries",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmployeePromotionId",
                table: "EmployeeSalaries",
                column: "EmployeePromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmploymentId",
                table: "EmployeeSalaries",
                column: "EmploymentId");
        }
    }
}
