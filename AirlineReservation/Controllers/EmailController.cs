using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult SendEmail()
        {
            return View();
        }
    }
}
