using AirlineReservation.Data;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ContactController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext= applicationDbContext;
        }
        public IActionResult Index()
        {
           ViewBag.Adress= _applicationDbContext.Adresss.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddMessages(Contact contact)
        {
            _applicationDbContext.Contacts.Add(contact);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
