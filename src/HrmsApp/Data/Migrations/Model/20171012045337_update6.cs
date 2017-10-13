using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BreakMinutes = table.Column<int>(nullable: false, defaultValue: 0),
                    EffectiveFromDate = table.Column<DateTime>(type: "date", nullable: false),
                    FirstWeekDay = table.Column<string>(nullable: true),
                    FlexStartMinutes = table.Column<int>(nullable: false, defaultValue: 0),
                    FromHour = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    NbrWorkingDays = table.Column<int>(nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false),
                    ThruHour = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalendarId = table.Column<long>(nullable: false),
                    FromDay = table.Column<int>(nullable: false),
                    FromMonth = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    IsHijri = table.Column<bool>(nullable: false, defaultValue: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    NbrDays = table.Column<int>(nullable: false),
                    OthName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holidays_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolidayVariations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalendarId = table.Column<long>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(type: "date", nullable: false),
                    HolidayId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    NbrDays = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayVariations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayVariations_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HolidayVariations_Holidays_HolidayId",
                        column: x => x.HolidayId,
                        principalTable: "Holidays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CalendarId",
                table: "Holidays",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayVariations_CalendarId",
                table: "HolidayVariations",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayVariations_HolidayId",
                table: "HolidayVariations",
                column: "HolidayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HolidayVariations");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Calendars");
        }
    }
}
