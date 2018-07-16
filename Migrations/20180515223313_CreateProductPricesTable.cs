using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class CreateProductPricesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    VariantId = table.Column<int>(nullable: false),
                    VariantValueId = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_ProductPrices_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductPrices_VariantValues_VariantValueId",
                        column: x => x.VariantValueId,
                        principalTable: "VariantValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_VariantId",
                table: "ProductPrices",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_VariantValueId",
                table: "ProductPrices",
                column: "VariantValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId_VariantId_VariantValueId",
                table: "ProductPrices",
                columns: new[] { "ProductId", "VariantId", "VariantValueId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPrices");
        }
    }
}
