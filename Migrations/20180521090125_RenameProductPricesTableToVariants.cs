using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class RenameProductPricesTableToVariants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceValues_ProductPrices_ProductPriceId",
                table: "ProductPriceValues");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "ProductPriceValues",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceValues_VariantId",
                table: "ProductPriceValues",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId",
                table: "Variants",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceValues_Variants_VariantId",
                table: "ProductPriceValues",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceValues_Variants_VariantId",
                table: "ProductPriceValues");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropIndex(
                name: "IX_ProductPriceValues_VariantId",
                table: "ProductPriceValues");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "ProductPriceValues");

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceValues_ProductPrices_ProductPriceId",
                table: "ProductPriceValues",
                column: "ProductPriceId",
                principalTable: "ProductPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
