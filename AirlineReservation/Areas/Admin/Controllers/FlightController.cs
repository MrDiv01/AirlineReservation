using AirlineReservation.Data;
using Microsoft.AspNetCore.Mvc;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Authorization;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FlightController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
           List<Flight> flights = _applicationDbContext.Flights.ToList();
            return View(flights);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Flight flight)
        {
            _applicationDbContext.Flights.Add(flight);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
            Flight flight = _applicationDbContext.Flights.Find(Id);
            return View(flight);
        }
        [HttpPost]
        public IActionResult Update(Flight flight)
        {
           Flight flight1 = _applicationDbContext.Flights.Find(flight.Id);
            flight1.FromAirport= flight.FromAirport;
            flight1.ToAirport= flight.ToAirport;
            flight1.DepartureTime= flight.DepartureTime;
             flight1.AriveTime= flight.AriveTime ;
             flight1.FlightDay= flight.FlightDay ;
             flight1.Count = flight.Count;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            Flight flight = _applicationDbContext.Flights.Find(Id);
            _applicationDbContext.Flights.Remove(flight);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
