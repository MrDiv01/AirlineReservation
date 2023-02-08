using AirlineReservation.Models;
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

        [HttpPost]
        public IActionResult Index(Search search)
        {
            if (!ModelState.IsValid) return View();


            string result = Url.Action("Index", "Destination",search);

            return Redirect(result);
        }
    }
}