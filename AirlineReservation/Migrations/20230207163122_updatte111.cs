using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservation.Migrations
{
    public partial class updatte111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "UserTickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserTickets_FlightId",
                table: "UserTickets",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTickets_Flights_FlightId",
                table: "UserTickets",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTickets_Flights_FlightId",
                table: "UserTickets");

            migrationBuilder.DropIndex(
                name: "IX_UserTickets_FlightId",
                table: "UserTickets");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "UserTickets");
        }
    }
}
