using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class relateOrderDetailCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDetails_IdOrder",
                table: "OrdersDetails",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDetails_IdProduct",
                table: "OrdersDetails",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdCustomer",
                table: "Orders",
                column: "IdCustomer");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersDetails_Orders_IdOrder",
                table: "OrdersDetails",
                column: "IdOrder",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersDetails_Products_IdProduct",
                table: "OrdersDetails",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersDetails_Orders_IdOrder",
                table: "OrdersDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersDetails_Products_IdProduct",
                table: "OrdersDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrdersDetails_IdOrder",
                table: "OrdersDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrdersDetails_IdProduct",
                table: "OrdersDetails");

            migrationBuilder.DropIndex(
                name: "IX_Orders_IdCustomer",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
