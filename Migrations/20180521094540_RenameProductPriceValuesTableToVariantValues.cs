using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class RenameProductPriceValuesTableToVariantValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPriceValues");

            migrationBuilder.CreateTable(
                name: "VariantValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OptionId = table.Column<int>(nullable: false),
                    ValueId = table.Column<int>(nullable: false),
                    VariantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantValues_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VariantValues_OptionValues_ValueId",
                        column: x => x.ValueId,
                        principalTable: "OptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VariantValues_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_OptionId",
                table: "VariantValues",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ValueId",
                table: "VariantValues",
                column: "ValueId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_VariantId_ValueId",
                table: "VariantValues",
                columns: new[] { "VariantId", "ValueId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariantValues");

            migrationBuilder.CreateTable(
                name: "ProductPriceValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductPriceId = table.Column<int>(nullable: false),
                    VariantId = table.Column<int>(nullable: true),
                    VariantValueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPriceValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPriceValues_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPriceValues_OptionValues_VariantValueId",
                        column: x => x.VariantValueId,
                        principalTable: "OptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceValues_VariantId",
                table: "ProductPriceValues",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceValues_VariantValueId",
                table: "ProductPriceValues",
                column: "VariantValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceValues_ProductPriceId_VariantValueId",
                table: "ProductPriceValues",
                columns: new[] { "ProductPriceId", "VariantValueId" },
                unique: true);
        }
    }
}
