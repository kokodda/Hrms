using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HrmsModel.Migrations
{
    public partial class update18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Candidates",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalScore",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedDate",
                table: "Candidates",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "FinalScore",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "Candidates");
        }
    }
}
