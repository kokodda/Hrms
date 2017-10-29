using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HrmsModel.Migrations
{
    public partial class update24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrgUnits_SalarySteps_SalaryStepId",
                table: "OrgUnits");

            migrationBuilder.DropIndex(
                name: "IX_OrgUnits_SalaryStepId",
                table: "OrgUnits");

            migrationBuilder.DropColumn(
                name: "SalaryStepId",
                table: "OrgUnits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaryStepId",
                table: "OrgUnits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_SalaryStepId",
                table: "OrgUnits",
                column: "SalaryStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrgUnits_SalarySteps_SalaryStepId",
                table: "OrgUnits",
                column: "SalaryStepId",
                principalTable: "SalarySteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
