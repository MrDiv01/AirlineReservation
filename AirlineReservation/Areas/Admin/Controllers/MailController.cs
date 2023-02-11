using AirlineReservation.Data;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        private readonly ApplicationDbContext _aaplicationDbContext;
        public MailController(ApplicationDbContext applicationDbContext)
        {
            _aaplicationDbContext= applicationDbContext;
        }
        [HttpGet]
        public IActionResult Index(int Id)
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Index(int Id)
        //{
        //    ViewBag.Contact = _aaplicationDbContext.Contacts.Find(Id);
        //    return View();
        //}
    }
}
