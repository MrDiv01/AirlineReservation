using AirlineReservation.Data;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class GlobalMessagesController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GlobalMessagesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
           List<Messages> mes = _applicationDbContext.Messages.ToList();
            return View(mes);
        }
        [HttpGet]
        public IActionResult SendGlobal()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendGlobal(Messages messagess)
        {
            List<UserMails> userMails = _applicationDbContext.UserMails.ToList();
            try
            {

                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential credential = new System.Net.NetworkCredential("flightrezervation@outlook.com", "Nurlanaztu2003.");
                client.EnableSsl = true;
                client.Credentials = credential;
               
                foreach (var useremails in userMails)
                {
                    MailMessage message = new MailMessage("flightrezervation@outlook.com", useremails.UserMail);
                    message.Subject = "Airline Reservation Support Team";
                    message.Body = messagess.Mesage;
                    message.IsBodyHtml = false;
                    client.Send(message);
                }
            }
            catch (Exception)
            {
                throw;
            }
            messagess.SendTime = DateTime.Now;
            _applicationDbContext.Messages.Add(messagess);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "GlobalMessages");
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Messages messages = _applicationDbContext.Messages.FirstOrDefault(c=>c.Id == Id);
            if(messages == null)
                        return RedirectToAction("Errors","Error");
            _applicationDbContext.Messages.Remove(messages);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "GlobalMessages");
        }
    }
}
