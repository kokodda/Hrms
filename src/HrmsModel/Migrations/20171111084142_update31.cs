using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HrmsModel.Migrations
{
    public partial class update31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftRotations_GenericSubGroups_GenericSubGroupId",
                table: "ShiftRotations");

            migrationBuilder.DropIndex(
                name: "IX_ShiftRotations_GenericSubGroupId",
                table: "ShiftRotations");

            migrationBuilder.DropColumn(
                name: "FromTime",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ShiftTypeCode",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ThruTime",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "GenericSubGroupId",
                table: "ShiftRotations");

            migrationBuilder.DropColumn(
                name: "NbrDays",
                table: "ShiftRotations");

            migrationBuilder.AddColumn<string>(
                name: "OthName",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromTime",
                table: "ShiftRotations",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShiftTypeCode",
                table: "ShiftRotations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ThruTime",
                table: "ShiftRotations",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShiftGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenericSubGroupId = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftGroups_GenericSubGroups_GenericSubGroupId",
                        column: x => x.GenericSubGroupId,
                        principalTable: "GenericSubGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftGroups_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShiftGroups_GenericSubGroupId",
                table: "ShiftGroups",
                column: "GenericSubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftGroups_ShiftId",
                table: "ShiftGroups",
                column: "ShiftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftGroups");

            migrationBuilder.DropColumn(
                name: "OthName",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "FromTime",
                table: "ShiftRotations");

            migrationBuilder.DropColumn(
                name: "ShiftTypeCode",
                table: "ShiftRotations");

            migrationBuilder.DropColumn(
                name: "ThruTime",
                table: "ShiftRotations");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromTime",
                table: "Shifts",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShiftTypeCode",
                table: "Shifts",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Shifts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThruTime",
                table: "Shifts",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenericSubGroupId",
                table: "ShiftRotations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NbrDays",
                table: "ShiftRotations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRotations_GenericSubGroupId",
                table: "ShiftRotations",
                column: "GenericSubGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftRotations_GenericSubGroups_GenericSubGroupId",
                table: "ShiftRotations",
                column: "GenericSubGroupId",
                principalTable: "GenericSubGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
