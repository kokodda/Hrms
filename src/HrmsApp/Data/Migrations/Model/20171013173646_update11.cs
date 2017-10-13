using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPayrollIncluded",
                table: "Employments");

            migrationBuilder.AddColumn<bool>(
                name: "IsPayrollExcluded",
                table: "Employments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPayrollExcluded",
                table: "Employments");

            migrationBuilder.AddColumn<bool>(
                name: "IsPayrollIncluded",
                table: "Employments",
                nullable: false,
                defaultValue: true);
        }
    }
}
