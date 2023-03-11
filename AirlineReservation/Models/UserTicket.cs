using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservation.Models
{
    public class UserTicket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string FatherName { get; set; }
        public string Fincode { get; set; }
        public string Email { get; set; }
        public int? FlightId { get; set; }
        public Flight Flight { get; set; }

    }
}
