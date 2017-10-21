using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompliancePercentage",
                table: "EmployeeAttendances",
                nullable: false,
                defaultValue: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "Attendances",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullMonth",
                table: "Attendances",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThruDate",
                table: "Attendances",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompliancePercentage",
                table: "EmployeeAttendances");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "IsFullMonth",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "ThruDate",
                table: "Attendances");
        }
    }
}
