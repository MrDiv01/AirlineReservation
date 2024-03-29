﻿using AirlineReservation.Data;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class FindUsersController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FindUsersController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            List<Flight> flights = _applicationDbContext.Flights.ToList();
            return View(flights);
        }
        public IActionResult Passengers(int id)
        {
            List<UserTicket> passengers = _applicationDbContext.UserTickets.Where(x => x.FlightId == id).ToList();
            if(passengers.Count == 0)
            {
                RedirectToAction("SearchUser", "Errors");
            }

            return View(passengers);
        }
    }
}
