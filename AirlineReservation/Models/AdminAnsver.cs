using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservation.Models
{
    public class AdminAnsver
    {
        public int Id { get; set; }
        public string Messages { get; set; }
        public DateTime SendTime { get; set; }
        [NotMapped]
        public int ContactID { get; set; }
    }
}
