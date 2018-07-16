using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class CreateVariantValuesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Values",
                table: "Variants");

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
                        name: "FK_VariantValues_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_VariantId_Value",
                table: "VariantValues",
                columns: new[] { "VariantId", "Value" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariantValues");

            migrationBuilder.AddColumn<string>(
                name: "Values",
                table: "Variants",
                type: "nvarchar(500)",
                nullable: false,
                defaultValue: "");
        }
    }
}
