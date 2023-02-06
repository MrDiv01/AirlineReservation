using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservation.Models
{
	public class Search
	{
		[NotMapped]
		public int Id { get; set; }
		[NotMapped]
		public string TAirport { get; set; }
		[NotMapped]
		public string FAirport { get; set; }
		[NotMapped]
		public DateTime DateTimeAirdate { get; set; }

	}
}
