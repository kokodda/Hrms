using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HrmsModel.Migrations
{
    public partial class update23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_SalarySteps_SalaryStepId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_SalaryStepId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "SalaryStepId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "JobName",
                table: "Employments");

            migrationBuilder.DropColumn(
                name: "OthJobName",
                table: "Employments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaryStepId",
                table: "Positions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobName",
                table: "Employments",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OthJobName",
                table: "Employments",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SalaryStepId",
                table: "Positions",
                column: "SalaryStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_SalarySteps_SalaryStepId",
                table: "Positions",
                column: "SalaryStepId",
                principalTable: "SalarySteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
