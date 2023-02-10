using AirlineReservation.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Speech.Recognition;
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
            DateTime dateTime= DateTime.Now;
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            DateTime dateTime1 = search.DateTimeAirdate;
            DateOnly dateOnly1 = DateOnly.FromDateTime(dateTime1);
            if (dateOnly1 < dateOnly)
            {
                ModelState.AddModelError("DateTimeAirdate", "Secilen Tarix Cari Tarixden Evvel Ola bilmez");
            }
            if (!ModelState.IsValid) return View();


            string result = Url.Action("Index", "Destination",search);

            return Redirect(result);
        }
    }
}