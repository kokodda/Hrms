using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations
{
    public partial class update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AlterColumn<int>(
                name: "SalaryScaleTypeId",
                table: "EmployeePromotions",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SalaryScaleTypeId",
                table: "EmployeePositions",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositions_SalaryScaleTypes_SalaryScaleTypeId",
                table: "EmployeePositions",
                column: "SalaryScaleTypeId",
                principalTable: "SalaryScaleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePromotions_SalaryScaleTypes_SalaryScaleTypeId",
                table: "EmployeePromotions",
                column: "SalaryScaleTypeId",
                principalTable: "SalaryScaleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AlterColumn<long>(
                name: "SalaryScaleTypeId",
                table: "EmployeePromotions",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SalaryScaleTypeId",
                table: "EmployeePositions",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePositions_SalaryScales_SalaryScaleTypeId",
                table: "EmployeePositions",
                column: "SalaryScaleTypeId",
                principalTable: "SalaryScales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePromotions_SalaryScales_SalaryScaleTypeId",
                table: "EmployeePromotions",
                column: "SalaryScaleTypeId",
                principalTable: "SalaryScales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
