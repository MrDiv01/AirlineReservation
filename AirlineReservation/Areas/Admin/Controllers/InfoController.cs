using AirlineReservation.Data;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class InfoController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public InfoController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            List<Adres> adres = _applicationDbContext.Adresss.ToList();
            return View(adres);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Adres adres)
        {
            _applicationDbContext.Adresss.Add(adres);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Info");

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Adres adres = _applicationDbContext.Adresss.Find(id);
            return View(adres);
        }
        [HttpPost]
        public IActionResult Update(Adres adres)
        {
            Adres OldAdres = _applicationDbContext.Adresss.Find(adres.Id);
            OldAdres.Address = adres.Address;
            OldAdres.AdminMail = adres.AdminMail;
            OldAdres.Phone= adres.Phone;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Info");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Adres adres =_applicationDbContext.Adresss.Find(id);
            _applicationDbContext.Adresss.Remove(adres);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Info");
        }
    }
}
