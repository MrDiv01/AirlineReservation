using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineReservation.Migrations
{
    public partial class UpdateUserTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "UserTickets",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "UserTickets");
        }
    }
}
