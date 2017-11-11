using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HrmsModel.Migrations
{
    public partial class update29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CalendarId",
                table: "Companies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HolidaysMultiplier",
                table: "Calendars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Calendars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OverTime1Multiplier",
                table: "Calendars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OverTime2Multiplier",
                table: "Calendars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalendarId = table.Column<long>(type: "bigint", nullable: true),
                    FromTime = table.Column<DateTime>(type: "date", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShiftTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    ThruTime = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShiftRotations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenericSubGroupId = table.Column<int>(type: "int", nullable: false),
                    NbrDays = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftRotations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftRotations_GenericSubGroups_GenericSubGroupId",
                        column: x => x.GenericSubGroupId,
                        principalTable: "GenericSubGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftRotations_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CalendarId",
                table: "Companies",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRotations_GenericSubGroupId",
                table: "ShiftRotations",
                column: "GenericSubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRotations_ShiftId",
                table: "ShiftRotations",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_CalendarId",
                table: "Shifts",
                column: "CalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Calendars_CalendarId",
                table: "Companies",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Calendars_CalendarId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "ShiftRotations");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CalendarId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "HolidaysMultiplier",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "OverTime1Multiplier",
                table: "Calendars");

            migrationBuilder.DropColumn(
                name: "OverTime2Multiplier",
                table: "Calendars");
        }
    }
}
