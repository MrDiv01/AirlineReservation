using AirlineReservation.Models;

namespace AirlineReservation.DTOs.ReturnTicketDto
{
    public class ReturnTicketDto
    {
        public int Id { get; set; }
        public string FromAirport { get; set; }
        public string ToAirport { get; set; }
        public string FAirportName { get; set; }
        public string TAirportName { get; set; }
        public DateTime DepartureTime { get; set; }
        public string FinCode { get; set; }
        public string TripCode { get; set; }

    }
}
