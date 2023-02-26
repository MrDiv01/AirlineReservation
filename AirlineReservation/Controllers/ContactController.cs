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
        public IActionResult Index(Contact contact)
        {
            ViewBag.Adress = _applicationDbContext.Adresss.ToList();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Bos Qala Bilmez");
                return View();
            }
            contact.DateTime = DateTime.Now;
            _applicationDbContext.Contacts.Add(contact);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
