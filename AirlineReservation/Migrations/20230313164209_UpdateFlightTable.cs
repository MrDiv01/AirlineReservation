using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservation.Migrations
{
    public partial class UpdateFlightTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTickets_Flights_FlightId",
                table: "UserTickets");

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "UserTickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TripCode",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.DropColumn(
                name: "TripCode",
                table: "Flights");

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "UserTickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTickets_Flights_FlightId",
                table: "UserTickets",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");
        }
    }
}
