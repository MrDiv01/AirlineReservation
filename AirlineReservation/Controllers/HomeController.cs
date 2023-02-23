using AirlineReservation.Data;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Speech.Recognition;
namespace AirlineReservation.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public HomeController(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public IActionResult Index()
		{
			List<Flight> flights = _applicationDbContext.Flights.ToList();
			List<UserTicket> userTickets = _applicationDbContext.UserTickets.ToList();
			foreach (Flight flight in flights)
			{
				if (flight.DepartureTime < DateTime.Now || flight.Count == 0)
				{
					_applicationDbContext.Flights.Remove(flight);
					_applicationDbContext.SaveChanges();
				}
			}
			ViewBag.titl = _applicationDbContext.Titiles.ToList();
			ViewBag.HomeDec = _applicationDbContext.HeaderDecs.ToList();
			return View();
		}

		[HttpPost]
		public IActionResult Index(Search search)
		{
			ViewBag.titl = _applicationDbContext.Titiles.ToList();
            ViewBag.HomeDec = _applicationDbContext.HeaderDecs.ToList();

            DateTime dateTime = DateTime.Now;
			DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
			DateTime dateTime1 = search.DateTimeAirdate;
			DateOnly dateOnly1 = DateOnly.FromDateTime(dateTime1);
			if (dateOnly1 < dateOnly)
			{
				ModelState.AddModelError("DateTimeAirdate", "Secilen Tarix Cari Tarixden Evvel Ola bilmez");
			}
			if (!ModelState.IsValid) return View();


			string result = Url.Action("Index", "Destination", search);

			return Redirect(result);
		}
	}
}