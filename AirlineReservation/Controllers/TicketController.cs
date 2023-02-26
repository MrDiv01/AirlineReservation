using AirlineReservation.Data;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace AirlineReservation.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public TicketController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index(int id)
        {
            ViewBag.Id = id;

            return View();
        }
        [HttpPost]
        public IActionResult Index(UserTicket userTicket)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string result = Url.Action("Rezerv", "Ticket", userTicket);

            return Redirect(result);
        }
        [HttpPost]
        public IActionResult Rezerv(UserTicket userTicket)
        {
            if (!ModelState.IsValid)
            {
                string result = Url.Action("Index", "Ticket");

                return Redirect(result);
            }
            Flight fligh = _applicationDbContext.Flights.Find(userTicket.FlightId);

            if (fligh == null)
                return NotFound();


            try
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential credential = new System.Net.NetworkCredential("memmedovn@outlook.com", "Nurlanaztu2003.");
                client.EnableSsl = true;
                client.Credentials = credential;
                MailMessage message = new MailMessage("memmedovn@outlook.com", userTicket.Email);
                message.Subject = "Airline Reservation";
                message.Body = "Hi " + userTicket.Name + ". " + " Have a nice trip " + "From " + fligh.FromAirport + " To " + fligh.ToAirport + " At " + fligh.DepartureTime;
                message.IsBodyHtml = false;
                client.Send(message);
                fligh.Count = fligh.Count - 1;
                UserMails userMails = new UserMails()
                {
                    UserMail = userTicket.Email,
                    UserName = userTicket.Name,
                };
               UserMails mails =  _applicationDbContext.UserMails.FirstOrDefault(x => x.UserMail.ToLower() == userTicket.Email.ToLower());
                if(mails == null)
                {
                    _applicationDbContext.UserMails.Add(userMails);
                    _applicationDbContext.SaveChanges();
                }
                _applicationDbContext.UserTickets.Add(userTicket);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
