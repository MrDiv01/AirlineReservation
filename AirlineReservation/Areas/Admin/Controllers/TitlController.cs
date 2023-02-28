using AirlineReservation.Data;
using AirlineReservation.Helpers;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class TitlController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _env;

        public TitlController(ApplicationDbContext applicationDbContext,IWebHostEnvironment env)
        {
            _applicationDbContext = applicationDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Titile> title = _applicationDbContext.Titiles.ToList();
            return View(title);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Titile titile)
        {
            if(titile.ImageFile.ContentType !="image/png" && titile.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile","Ancaq PNG ve JPG ola biler");
                return View();
            }
            if (titile.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "Sekilin Olcusu 3mb dan boyuk ola bilmez");
                return View();
            }
            string name =titile.ImageFile.FileName;
           
          titile.Images =  FileMeneger.SaveFile(_env.WebRootPath,"uploads/HomeSection2",titile.ImageFile);
             foreach(var oldTitl in _applicationDbContext.Titiles)
            {
                if(oldTitl.Name != titile.Name)
                {
                    _applicationDbContext.Titiles.Remove(oldTitl);
                }
            }
            _applicationDbContext.Titiles.Add(titile);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
            Titile titile = _applicationDbContext.Titiles.Find(Id);
            return View(titile);
        }
        [HttpPost]
        public IActionResult Update(Titile titile)
        {
            Titile exsTitl = _applicationDbContext.Titiles.FirstOrDefault(x=>x.Id == titile.Id);
            if (titile.ImageFile.ContentType != "image/png" && titile.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Ancaq PNG ve JPG ola biler");
                return View();
            }
            if (titile.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "Sekilin Olcusu 3mb dan boyuk ola bilmez");
                return View();
            }
            string name = titile.ImageFile.FileName;
            FileMeneger.DeleteFile(_env.WebRootPath, "uploads/HomeSection2", exsTitl.Images);
            exsTitl.Images = FileMeneger.SaveFile(_env.WebRootPath, "uploads/HomeSection2", titile.ImageFile);
            exsTitl.Name = titile.Name;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Titile titile = _applicationDbContext.Titiles.Find(id);
            _applicationDbContext.Titiles.Remove(titile);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
