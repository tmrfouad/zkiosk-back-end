using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class RenameVariantValuesTableToOptionValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceValues_VariantValues_VariantValueId",
                table: "ProductPriceValues");

            migrationBuilder.DropTable(
                name: "VariantValues");

            migrationBuilder.CreateTable(
                name: "OptionValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OptionId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionValues_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionValues_OptionId_Value",
                table: "OptionValues",
                columns: new[] { "OptionId", "Value" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceValues_OptionValues_VariantValueId",
                table: "ProductPriceValues",
                column: "VariantValueId",
                principalTable: "OptionValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceValues_OptionValues_VariantValueId",
                table: "ProductPriceValues");

            migrationBuilder.DropTable(
                name: "OptionValues");

            migrationBuilder.CreateTable(
                name: "VariantValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    VariantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantValues_Options_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_VariantId_Value",
                table: "VariantValues",
                columns: new[] { "VariantId", "Value" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceValues_VariantValues_VariantValueId",
                table: "ProductPriceValues",
                column: "VariantValueId",
                principalTable: "VariantValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
