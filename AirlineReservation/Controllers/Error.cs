using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Controllers
{
	public class Error : Controller
	{
		public IActionResult Errors()
		{
			return View();
		}
		public IActionResult SearchUser()
		{
			return View();
		}
	}
}
