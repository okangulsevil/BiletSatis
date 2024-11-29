using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiletSatis.Migrations
{
    /// <inheritdoc />
    public partial class AddAirlineLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Airline",
                table: "Flights",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AirlineLogoPath",
                table: "Flights",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Airline",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "AirlineLogoPath",
                table: "Flights");
        }
    }
}
