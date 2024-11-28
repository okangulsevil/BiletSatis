using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiletSatis.Migrations
{
    /// <inheritdoc />
    public partial class FixNavigationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Customers_CustomerId1",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Flights_FlightId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CustomerId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_FlightId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "FlightId1",
                table: "Tickets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Tickets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightId1",
                table: "Tickets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CustomerId1",
                table: "Tickets",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId1",
                table: "Tickets",
                column: "FlightId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Customers_CustomerId1",
                table: "Tickets",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Flights_FlightId1",
                table: "Tickets",
                column: "FlightId1",
                principalTable: "Flights",
                principalColumn: "Id");
        }
    }
}
