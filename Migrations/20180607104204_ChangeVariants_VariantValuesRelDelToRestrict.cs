using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class ChangeVariants_VariantValuesRelDelToRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_Variants_VariantId",
                table: "VariantValues");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_Variants_VariantId",
                table: "VariantValues",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_Variants_VariantId",
                table: "VariantValues");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_Variants_VariantId",
                table: "VariantValues",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
