using AirlineReservation.Data;
using AirlineReservation.Helpers;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class HomeTitlController : Controller
	{
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _env;

        public HomeTitlController(ApplicationDbContext applicationDbContext, IWebHostEnvironment env)
        {
            _applicationDbContext = applicationDbContext;
            _env = env;
        }
        public IActionResult Index()
		{
			List<HeaderDec> headerDecs =_applicationDbContext.HeaderDecs.ToList();
			return View(headerDecs);
		}
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(HeaderDec homedec)
        {
            if (homedec.ImageFile.ContentType != "image/png" && homedec.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            if (homedec.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "Sekilin Olcusu 3mb dan boyuk ola bilmez");
                return View();
            }
            string name = homedec.ImageFile.FileName;

            homedec.Images = FileMeneger2.SaveFile(_env.WebRootPath, "uploads/HomeSection1", homedec.ImageFile);
            foreach (var oldTitl in _applicationDbContext.HeaderDecs)
            {
                if (oldTitl.Name != homedec.Name)
                {
                    _applicationDbContext.HeaderDecs.Remove(oldTitl);
                }
            }
            _applicationDbContext.HeaderDecs.Add(homedec);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
            HeaderDec homedec = _applicationDbContext.HeaderDecs.Find(Id);
            return View(homedec);
        }
        [HttpPost]
        public IActionResult Update(HeaderDec homedec)
        {
            HeaderDec exsTitl = _applicationDbContext.HeaderDecs.FirstOrDefault(x => x.Id == homedec.Id);
            if (homedec.ImageFile.ContentType != "image/png" && homedec.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            if (homedec.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "Sekilin Olcusu 3mb dan boyuk ola bilmez");
                return View();
            }
            string name = homedec.ImageFile.FileName;
            FileMeneger2.DeleteFile(_env.WebRootPath, "uploads/HomeSection1", exsTitl.Images);
            exsTitl.Images = FileMeneger2.SaveFile(_env.WebRootPath, "uploads/HomeSection1", homedec.ImageFile);
            exsTitl.Name = homedec.Name;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            HeaderDec homedec = _applicationDbContext.HeaderDecs.Find(id);
            _applicationDbContext.HeaderDecs.Remove(homedec);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
