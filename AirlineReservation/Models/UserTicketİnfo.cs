namespace AirlineReservation.Models
{
	public class UserTicketİnfo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string SureName { get; set; }
		public string FatherName { get; set; }
		public string Fincode { get; set;}
		public string Email { get; set;}
		public Flight Flight { get; set; }
		public int FlightId { get; set;}
	}
}
