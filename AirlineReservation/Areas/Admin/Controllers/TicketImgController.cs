using AirlineReservation.Data;
using AirlineReservation.Helpers;
using AirlineReservation.Migrations;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketImgController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _env;

        public TicketImgController(ApplicationDbContext applicationDbContext, IWebHostEnvironment env)
        {
            _applicationDbContext = applicationDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<TicketImages> images = _applicationDbContext.TicketImage.ToList();
            return View(images);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TicketImages ticketImages)
        {
            if (ticketImages.ImageFile.ContentType != "image/png" && ticketImages.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            if (ticketImages.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "Sekilin Olcusu 3mb dan boyuk ola bilmez");
                return View();
            }
            string name = ticketImages.ImageFile.FileName;
            List<TicketImages> ticketImages1 = _applicationDbContext.TicketImage.Where(x => x.FAirportName == ticketImages.FAirportName &&
                                                                                  x.TAirportName == ticketImages.TAirportName).ToList();
            if(ticketImages1.Count == 0)
            {
            ticketImages.ImgName = FileMeneger3.SaveFile(_env.WebRootPath, "uploads/FlightImg", ticketImages.ImageFile);
            _applicationDbContext.TicketImage.Add(ticketImages);
            _applicationDbContext.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Bu Istiqametde artiq Sekil secmisiniz");
                return View();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
           TicketImages ticketImages = _applicationDbContext.TicketImage.Find(Id);
            return View(ticketImages);
        }
        [HttpPost]
        public IActionResult Update(TicketImages ticketImages)
        {
            TicketImages ticketImages1 = _applicationDbContext.TicketImage.Find(ticketImages.Id);
            if (ticketImages.ImageFile.ContentType != "image/png" && ticketImages.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            if (ticketImages.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "Sekilin Olcusu 3mb dan boyuk ola bilmez");
                return View();
            }
            string name = ticketImages.ImageFile.FileName;
            FileMeneger3.DeleteFile(_env.WebRootPath, "uploads/FlightImg", ticketImages1.ImgName);
            ticketImages1.ImgName = FileMeneger3.SaveFile(_env.WebRootPath, "uploads/FlightImg", ticketImages.ImageFile);
            ticketImages1.FAirportName = ticketImages.FAirportName;
            ticketImages1.TAirportName = ticketImages.TAirportName;

            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            TicketImages ticketImages = _applicationDbContext.TicketImage.Find(Id);
            _applicationDbContext.TicketImage.Remove(ticketImages);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
