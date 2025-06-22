using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesFlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class corregirralbe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdReservation",
                table: "Tables");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tables");

            migrationBuilder.AddColumn<int>(
                name: "IdReservation",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
