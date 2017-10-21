using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayrollAdditions_JobGrades_JobGradeId",
                table: "PayrollAdditions");

            migrationBuilder.DropIndex(
                name: "IX_PayrollAdditions_JobGradeId",
                table: "PayrollAdditions");

            migrationBuilder.DropColumn(
                name: "JobGradeId",
                table: "PayrollAdditions");

            migrationBuilder.AddColumn<int>(
                name: "FromJobGradeId",
                table: "PayrollAdditions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThruJobGradeId",
                table: "PayrollAdditions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_FromJobGradeId",
                table: "PayrollAdditions",
                column: "FromJobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_ThruJobGradeId",
                table: "PayrollAdditions",
                column: "ThruJobGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollAdditions_JobGrades_FromJobGradeId",
                table: "PayrollAdditions",
                column: "FromJobGradeId",
                principalTable: "JobGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollAdditions_JobGrades_ThruJobGradeId",
                table: "PayrollAdditions",
                column: "ThruJobGradeId",
                principalTable: "JobGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayrollAdditions_JobGrades_FromJobGradeId",
                table: "PayrollAdditions");

            migrationBuilder.DropForeignKey(
                name: "FK_PayrollAdditions_JobGrades_ThruJobGradeId",
                table: "PayrollAdditions");

            migrationBuilder.DropIndex(
                name: "IX_PayrollAdditions_FromJobGradeId",
                table: "PayrollAdditions");

            migrationBuilder.DropIndex(
                name: "IX_PayrollAdditions_ThruJobGradeId",
                table: "PayrollAdditions");

            migrationBuilder.DropColumn(
                name: "FromJobGradeId",
                table: "PayrollAdditions");

            migrationBuilder.DropColumn(
                name: "ThruJobGradeId",
                table: "PayrollAdditions");

            migrationBuilder.AddColumn<int>(
                name: "JobGradeId",
                table: "PayrollAdditions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAdditions_JobGradeId",
                table: "PayrollAdditions",
                column: "JobGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollAdditions_JobGrades_JobGradeId",
                table: "PayrollAdditions",
                column: "JobGradeId",
                principalTable: "JobGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
