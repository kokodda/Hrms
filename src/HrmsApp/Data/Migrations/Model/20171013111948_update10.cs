using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "FromHour",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "ThruHour",
                table: "Calendars");

            migrationBuilder.AddColumn<bool>(
                name: "IsPayrollIncluded",
                table: "Employments",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromTime",
                table: "Calendars",
                type: "datetime",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThruTime",
                table: "Calendars",
                type: "datetime",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPayrollIncluded",
                table: "Employments");

            migrationBuilder.DropColumn(
                name: "FromTime",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "ThruTime",
                table: "Calendars");

            migrationBuilder.AddColumn<long>(
                name: "CalendarId",
                table: "Holidays",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromHour",
                table: "Calendars",
                type: "datetime",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThruHour",
                table: "Calendars",
                type: "datetime",
                nullable: false);

            
        }
    }
}
