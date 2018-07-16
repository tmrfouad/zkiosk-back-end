using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class RemoveVariantIdVariantValueIdFromProductPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ProductPrices");
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_Variants_VariantId",
                table: "ProductPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_VariantValues_VariantValueId",
                table: "ProductPrices");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_ProductId_VariantId_VariantValueId",
                table: "ProductPrices");

            migrationBuilder.AlterColumn<int>(
                name: "VariantValueId",
                table: "ProductPrices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "VariantId",
                table: "ProductPrices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrices_Variants_VariantId",
                table: "ProductPrices",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrices_VariantValues_VariantValueId",
                table: "ProductPrices",
                column: "VariantValueId",
                principalTable: "VariantValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_Variants_VariantId",
                table: "ProductPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_VariantValues_VariantValueId",
                table: "ProductPrices");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices");

            migrationBuilder.AlterColumn<int>(
                name: "VariantValueId",
                table: "ProductPrices",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VariantId",
                table: "ProductPrices",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId_VariantId_VariantValueId",
                table: "ProductPrices",
                columns: new[] { "ProductId", "VariantId", "VariantValueId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrices_Variants_VariantId",
                table: "ProductPrices",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrices_VariantValues_VariantValueId",
                table: "ProductPrices",
                column: "VariantValueId",
                principalTable: "VariantValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
