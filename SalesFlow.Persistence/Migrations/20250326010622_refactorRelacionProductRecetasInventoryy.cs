using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refactorRelacionProductRecetasInventoryy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Ingredients_IdIngredient",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Products_IdProduct",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "HasIngredient",
                table: "Products",
                newName: "IsIngredient");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Products_IdIngredient",
                table: "Recipes",
                column: "IdIngredient",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Products_IdProduct",
                table: "Recipes",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Products_IdIngredient",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Products_IdProduct",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "IsIngredient",
                table: "Products",
                newName: "HasIngredient");

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UnitMeasurement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Ingredients_IdIngredient",
                table: "Recipes",
                column: "IdIngredient",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Products_IdProduct",
                table: "Recipes",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
