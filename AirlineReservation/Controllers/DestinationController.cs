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
			foreach(var a in _applicationDbContext.flights.ToList())
			{
				if (search.TAirport == a.ToAirport && search.FAirport == a.FromAirport)
				{
					DestinationViewModel destinationViewModel = new DestinationViewModel()
					{
						flightsı = _applicationDbContext.flights.Where(x => x.FromAirport == search.FAirport).Where(x => x.ToAirport == search.TAirport).ToList(),
					};
					return View(destinationViewModel);
				}
			}
			return RedirectToAction("Errors","Error");
		}
	}
}
