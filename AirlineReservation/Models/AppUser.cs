using Microsoft.AspNetCore.Identity;

namespace AirlineReservation.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }

    }
}
