using AirlineReservation.Data;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

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
        public IActionResult Index()
        {
          List<Contact> contact = _aaplicationDbContext.Contacts.ToList();
            return View(contact);
        }
        [HttpGet]
        public IActionResult Ansver(int Id)
        {
            ViewBag.Id=Id;
            return View();
        }
        [HttpPost]
        public IActionResult Ansver(AdminAnsver adminAnsver)
        {
            Contact contact =  _aaplicationDbContext.Contacts.Find(adminAnsver.ContactID);
            adminAnsver.SendTime = DateTime.Now;
            _aaplicationDbContext.AdminAnswer.Add(adminAnsver);
            _aaplicationDbContext.SaveChanges();
            try
            {

				SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
				client.Port = 587;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.UseDefaultCredentials = false;
				System.Net.NetworkCredential credential = new System.Net.NetworkCredential("memmedovn@outlook.com","Nurlanaztu2003.");
				client.EnableSsl = true;
				client.Credentials = credential;
				MailMessage message = new MailMessage("memmedovn@outlook.com", contact.Email);
				message.Subject = "Airline Reservation Support Team";
				message.Body = "Salam Dəyərli Müştıri Göndərdiyiniz Sualla bağlı Komandamızız Cavabı belədir." + " " + adminAnsver.Messages;
				message.IsBodyHtml = false;
				client.Send(message);
			}
			catch (Exception)
            {
                throw;
            }
            _aaplicationDbContext.Remove(contact);
            _aaplicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Mail");
        }
    }
}
