using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class realcionreservacionmesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdCustomer",
                table: "Tables",
                newName: "IdReservation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdCustomer",
                table: "Reservations",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdTable",
                table: "Reservations",
                column: "IdTable");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_IdCustomer",
                table: "Reservations",
                column: "IdCustomer",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_IdTable",
                table: "Reservations",
                column: "IdTable",
                principalTable: "Tables",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_IdCustomer",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_IdTable",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_IdCustomer",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_IdTable",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "IdReservation",
                table: "Tables",
                newName: "IdCustomer");
        }
    }
}
