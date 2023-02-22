using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace AirlineReservation.Models
{
	public class Flight
	{
		public int Id { get; set; }
		public string FromAirport { get; set; }
		public string ToAirport { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime AriveTime { get; set; }
		public DateTime FlightDay { get; set; }
		public int Count { get; set; }
		//public static string TicketCode { get; set; }
		//public string GetCode()
		//{
		//	Flight flight = new Flight();
		//	string NewTicketCode;
		//	 NewTicketCode= FromAirport[0].ToString() + ToAirport[0].ToString()+Count.ToString();
		//	TicketCode = NewTicketCode;
		//	return TicketCode;
		//}
	}
}
