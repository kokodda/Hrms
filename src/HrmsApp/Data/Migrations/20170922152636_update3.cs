using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppForm",
                table: "EmployeeLeaves",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "EmployeeLeaves",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppForm",
                table: "EmployeeLeaves");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "EmployeeLeaves");
        }
    }
}
