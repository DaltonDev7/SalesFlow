using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class relacionInvetarioProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inventory_IdProduct",
                table: "Inventory",
                column: "IdProduct",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Products_IdProduct",
                table: "Inventory",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Products_IdProduct",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_IdProduct",
                table: "Inventory");
        }
    }
}
