using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class MakeValuesIdInVariantsTableRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ValuesId",
                table: "Variants",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ValuesId",
                table: "Variants",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
