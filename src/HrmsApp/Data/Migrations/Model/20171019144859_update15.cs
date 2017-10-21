using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarouselItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BtnCaption = table.Column<string>(maxLength: 100, nullable: true),
                    BtnHref = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(maxLength: 450, nullable: false),
                    ImageName = table.Column<string>(maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    LastUpdated = table.Column<DateTime>(type: "date", nullable: false),
                    OthBtnCaption = table.Column<string>(maxLength: 100, nullable: true),
                    OthCaption = table.Column<string>(maxLength: 450, nullable: false),
                    SortOrder = table.Column<short>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarouselItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarouselItems");
        }
    }
}
