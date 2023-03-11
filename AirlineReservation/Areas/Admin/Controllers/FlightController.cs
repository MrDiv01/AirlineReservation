using AirlineReservation.Data;
using Microsoft.AspNetCore.Mvc;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Authorization;
using AirlineReservation.Helpers;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _env;

        public FlightController(ApplicationDbContext applicationDbContext, IWebHostEnvironment env)
        {
            _applicationDbContext = applicationDbContext;
            _env = env;
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
        
            if (flight.ImageFile.ContentType != "image/png" && flight.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            if (flight.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "Sekilin Olcusu 3mb dan boyuk ola bilmez");
                return View();
            }
            string name = flight.ImageFile.FileName;

            flight.Images = FileMeneger3.SaveFile(_env.WebRootPath, "uploads/FlightImg", flight.ImageFile);
            _applicationDbContext.Flights.Add(flight);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index");

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
            if (flight.ImageFile.ContentType != "image/png" && flight.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            if (flight.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "Sekilin Olcusu 3mb dan boyuk ola bilmez");
                return View();
            }
            string name = flight.ImageFile.FileName;
            FileMeneger3.DeleteFile(_env.WebRootPath, "uploads/FlightImg", flight1.Images);
            flight1.Images = FileMeneger3.SaveFile(_env.WebRootPath, "uploads/FlightImg", flight.ImageFile);
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
            List<UserTicket> userTickets = _applicationDbContext.UserTickets.Where(x=>x.FlightId == Id).ToList();
            foreach(UserTicket userTicket in userTickets)
            {
                _applicationDbContext.UserTickets.Remove(userTicket);
            }
            _applicationDbContext.Flights.Remove(flight);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
