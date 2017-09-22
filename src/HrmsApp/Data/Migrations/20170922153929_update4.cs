using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "EmployeeLeaves",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualThruDate",
                table: "EmployeeLeaves",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualFromDate",
                table: "EmployeeLeaves",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedBy",
                table: "EmployeeLeaves",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualThruDate",
                table: "EmployeeLeaves",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActualFromDate",
                table: "EmployeeLeaves",
                nullable: true);
        }
    }
}
