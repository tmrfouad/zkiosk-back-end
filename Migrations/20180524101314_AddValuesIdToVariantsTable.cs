using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class AddValuesIdToVariantsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValuesId",
                table: "Variants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValuesId",
                table: "Variants");
        }
    }
}
