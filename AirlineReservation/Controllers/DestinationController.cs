﻿using AirlineReservation.Areas.Admin.Controllers;
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
            ViewBag.TicketImg = _applicationDbContext.TicketImage.Where(x => x.FAirportName == search.FAirport &&
                                                                  x.TAirportName == search.TAirport).ToList();
			if (!ModelState.IsValid)
			{
				string result = Url.Action("Index", "Home");

				return Redirect(result);
			}
			foreach (var a in _applicationDbContext.Flights.ToList())
            {
                DateTime dateTime = a.FlightDay;
                DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
                DateTime dateTime1 = search.DateTimeAirdate;
                DateOnly dateOnly1 = DateOnly.FromDateTime(dateTime1);

                if (search.TAirport.ToLower() == a.ToAirport.ToLower() && 
                                            search.FAirport.ToLower() == a.FromAirport.ToLower() && 
                                            dateOnly == dateOnly1 && a.Count > 0 &&
                                            0 < DateTime.Compare(a.DepartureTime, DateTime.Now))
                {
                    DestinationViewModel destinationViewModel = new DestinationViewModel()
                    {
                        flightsı = _applicationDbContext.Flights.Where(x => x.FromAirport.ToLower() == search.FAirport.ToLower() && 
                                                        x.ToAirport.ToLower() == search.TAirport.ToLower() && 
                                                        x.FlightDay == search.DateTimeAirdate && 
                                                        0 < DateTime.Compare(x.DepartureTime, DateTime.Now)).ToList(),
                    };
                    return View(destinationViewModel);
                }
            }
            return RedirectToAction("Errors", "Error");
        }
    }
}