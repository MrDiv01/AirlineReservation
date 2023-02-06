using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Controllers
{
	public class TicketController : Controller
	{
		public IActionResult Index(int id)
		{
			return View();
		}
	}
}
