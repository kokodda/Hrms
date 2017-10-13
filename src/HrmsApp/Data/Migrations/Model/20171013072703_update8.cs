using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "HolidayVariations");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "HolidayVariations");

            migrationBuilder.AddColumn<string>(
                name: "Narration",
                table: "HolidayVariations",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Narration",
                table: "HolidayVariations");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "HolidayVariations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "HolidayVariations",
                nullable: false,
                defaultValue: 0);
        }
    }
}
