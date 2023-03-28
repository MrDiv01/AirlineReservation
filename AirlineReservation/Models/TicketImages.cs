using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservation.Models
{
    public class TicketImages
    {
        public int Id { get; set; }
        public string FAirportName { get; set; }
        public string TAirportName { get; set; }

        public string ImgName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
