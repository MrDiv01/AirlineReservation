using AirlineReservation.Data;
using AirlineReservation.Models;
using AirlineReservation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Controllers
{
	public class DestinationController : Controller
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public DestinationController(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public IActionResult Index(Search search)
		{
			foreach(var a in _applicationDbContext.Flights.ToList())
			{
				DateTime dateTime = a.FlightDay;
				DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
				DateTime dateTime1 = search.DateTimeAirdate;
				DateOnly dateOnly1=DateOnly.FromDateTime(dateTime1);
				if (search.TAirport == a.ToAirport && search.FAirport == a.FromAirport && dateOnly == dateOnly1)
				{
					DestinationViewModel destinationViewModel = new DestinationViewModel()
					{
						flightsı = _applicationDbContext.Flights.Where(x => x.FromAirport == search.FAirport && x.ToAirport == search.TAirport && x.FlightDay == search.DateTimeAirdate).ToList(),
					};
					return View(destinationViewModel);
				}
			}
			return RedirectToAction("Errors","Error");
		}
	}
}
