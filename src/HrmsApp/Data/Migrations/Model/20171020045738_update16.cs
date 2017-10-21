using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrmsApp.Data.Migrations.Model
{
    public partial class update16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageText",
                table: "CarouselItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OthImageText",
                table: "CarouselItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageText",
                table: "CarouselItems");

            migrationBuilder.DropColumn(
                name: "OthImageText",
                table: "CarouselItems");
        }
    }
}
