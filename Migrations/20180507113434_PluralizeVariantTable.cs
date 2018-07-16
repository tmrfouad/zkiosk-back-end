using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class PluralizeVariantTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Variant_Products_ProductId",
                table: "Variant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Variant",
                table: "Variant");

            migrationBuilder.RenameTable(
                name: "Variant",
                newName: "Variants");

            migrationBuilder.RenameIndex(
                name: "IX_Variant_ProductId_Name",
                table: "Variants",
                newName: "IX_Variants_ProductId_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Variants",
                table: "Variants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Variants_Products_ProductId",
                table: "Variants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Variants_Products_ProductId",
                table: "Variants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Variants",
                table: "Variants");

            migrationBuilder.RenameTable(
                name: "Variants",
                newName: "Variant");

            migrationBuilder.RenameIndex(
                name: "IX_Variants_ProductId_Name",
                table: "Variant",
                newName: "IX_Variant_ProductId_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Variant",
                table: "Variant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Variant_Products_ProductId",
                table: "Variant",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
