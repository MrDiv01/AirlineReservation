using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservation.Models
{
	public class HeaderDec
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Images { get; set; }
		[NotMapped]
		public IFormFile? ImageFile { get; set; }
	}
}
