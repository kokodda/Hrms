using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePromotions_SalaryScaleTypes_SalaryScaleTypeId",
                table: "EmployeePromotions");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePromotions_SalaryScaleTypeId",
                table: "EmployeePromotions");

            migrationBuilder.DropColumn(
                name: "SalaryScaleTypeId",
                table: "EmployeePromotions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaryScaleTypeId",
                table: "EmployeePromotions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePromotions_SalaryScaleTypeId",
                table: "EmployeePromotions",
                column: "SalaryScaleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePromotions_SalaryScaleTypes_SalaryScaleTypeId",
                table: "EmployeePromotions",
                column: "SalaryScaleTypeId",
                principalTable: "SalaryScaleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
