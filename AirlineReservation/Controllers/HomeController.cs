using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AirlineReservation.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
			return View();
        }
    }
}