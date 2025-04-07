using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class relateTableUserOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmploye",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdEmploye",
                table: "Orders",
                column: "IdEmploye");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_IdEmploye",
                table: "Orders",
                column: "IdEmploye",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_IdEmploye",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_IdEmploye",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IdEmploye",
                table: "Orders");
        }
    }
}
