using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HrmsModel.Migrations
{
    public partial class update27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "SalaryScales",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LegalTypeCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    OthShortName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_OrgUnits_Id",
                        column: x => x.Id,
                        principalTable: "OrgUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgUnitId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAccounts_OrgUnits_OrgUnitId",
                        column: x => x.OrgUnitId,
                        principalTable: "OrgUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MotherOrgUnitId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OthName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyGroups_OrgUnits_MotherOrgUnitId",
                        column: x => x.MotherOrgUnitId,
                        principalTable: "OrgUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyGroupAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyAccountId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyGroupId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyGroupAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyGroupAccounts_CompanyAccounts_CompanyAccountId",
                        column: x => x.CompanyAccountId,
                        principalTable: "CompanyAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyGroupAccounts_CompanyGroups_CompanyGroupId",
                        column: x => x.CompanyGroupId,
                        principalTable: "CompanyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyGroupMembers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyGroupId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    JoinDate = table.Column<DateTime>(type: "date", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyGroupMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyGroupMembers_CompanyGroups_CompanyGroupId",
                        column: x => x.CompanyGroupId,
                        principalTable: "CompanyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyGroupMembers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryScales_CompanyId",
                table: "SalaryScales",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAccounts_OrgUnitId",
                table: "CompanyAccounts",
                column: "OrgUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGroupAccounts_CompanyAccountId",
                table: "CompanyGroupAccounts",
                column: "CompanyAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGroupAccounts_CompanyGroupId",
                table: "CompanyGroupAccounts",
                column: "CompanyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGroupMembers_CompanyGroupId",
                table: "CompanyGroupMembers",
                column: "CompanyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGroupMembers_CompanyId",
                table: "CompanyGroupMembers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyGroups_MotherOrgUnitId",
                table: "CompanyGroups",
                column: "MotherOrgUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryScales_Companies_CompanyId",
                table: "SalaryScales",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryScales_Companies_CompanyId",
                table: "SalaryScales");

            migrationBuilder.DropTable(
                name: "CompanyGroupAccounts");

            migrationBuilder.DropTable(
                name: "CompanyGroupMembers");

            migrationBuilder.DropTable(
                name: "CompanyAccounts");

            migrationBuilder.DropTable(
                name: "CompanyGroups");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_SalaryScales_CompanyId",
                table: "SalaryScales");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "SalaryScales");
        }
    }
}
