using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class RenameVariantsTableToOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Variants");
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_Variants_VariantId",
                table: "ProductPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_VariantValues_VariantValueId",
                table: "ProductPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_Variants_VariantId",
                table: "VariantValues");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_VariantId",
                table: "ProductPrices");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_VariantValueId",
                table: "ProductPrices");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "ProductPrices");

            migrationBuilder.DropColumn(
                name: "VariantValueId",
                table: "ProductPrices");

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPriceValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductPriceId = table.Column<int>(nullable: false),
                    VariantValueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPriceValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPriceValues_ProductPrices_ProductPriceId",
                        column: x => x.ProductPriceId,
                        principalTable: "ProductPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPriceValues_VariantValues_VariantValueId",
                        column: x => x.VariantValueId,
                        principalTable: "VariantValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductId_Name",
                table: "Options",
                columns: new[] { "ProductId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceValues_VariantValueId",
                table: "ProductPriceValues",
                column: "VariantValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceValues_ProductPriceId_VariantValueId",
                table: "ProductPriceValues",
                columns: new[] { "ProductPriceId", "VariantValueId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_Options_VariantId",
                table: "VariantValues",
                column: "VariantId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_Options_VariantId",
                table: "VariantValues");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "ProductPriceValues");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices");

            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "ProductPrices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VariantValueId",
                table: "ProductPrices",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    ProductId = table.Column<int>(nullable: false)
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
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_VariantId",
                table: "ProductPrices",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_VariantValueId",
                table: "ProductPrices",
                column: "VariantValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId_Name",
                table: "Variants",
                columns: new[] { "ProductId", "Name" },
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
