using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompensateNbrDays",
                table: "HolidayVariations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "NbrDays",
                table: "HolidayVariations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "HolidayId",
                table: "HolidayVariations",
                nullable: false);

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "Holidays",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompensateNbrDays",
                table: "HolidayVariations");

            migrationBuilder.AlterColumn<int>(
                name: "NbrDays",
                table: "HolidayVariations",
                nullable: false);

            migrationBuilder.AlterColumn<long>(
                name: "HolidayId",
                table: "HolidayVariations",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CalendarId",
                table: "Holidays",
                nullable: false);
        }
    }
}
