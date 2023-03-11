using AirlineReservation.Data;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        static int Id = 0;
        [HttpGet]
        public IActionResult Index(int id)
        {
            Id = id;
            return View();
        }
        [HttpPost]
        public IActionResult Index(UserTicket userTicket)
        {
            userTicket.FlightId = Id;
            if (userTicket.Name == null || userTicket.SureName == null || userTicket.FatherName == null || userTicket.Fincode == null || userTicket.Email == null)
            {
                 ModelState.AddModelError("","ss");
                return View();
            }
            List<UserTicket> userTickets = _applicationDbContext.UserTickets.Where(x => x.Fincode == userTicket.Fincode && x.FlightId == userTicket.FlightId).ToList();
            if (userTickets.Count != 0)
            {
                return RedirectToAction("SearchUser", "Error");
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
                System.Net.NetworkCredential credential = new System.Net.NetworkCredential("flightrezervation@outlook.com", "Nurlanaztu2003.");
                client.EnableSsl = true;
                client.Credentials = credential;
                MailMessage message = new MailMessage("flightrezervation@outlook.com", userTicket.Email);
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
                UserMails mails = _applicationDbContext.UserMails.FirstOrDefault(x => x.UserMail.ToLower() == userTicket.Email.ToLower());
                if (mails == null)
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

        //[HttpPost]
        //public IActionResult Rezerv(UserTicket userTicket)
        //{
        //    userTicket.FlightId = Id;
        //    List<UserTicket> userTickets = _applicationDbContext.UserTickets.Where(x => x.Fincode == userTicket.Fincode && x.FlightId == userTicket.FlightId).ToList();
        //    if (userTickets.Count != 0)
        //    {
        //        return RedirectToAction("SearchUser", "Error");
        //    }
        //    Flight fligh = _applicationDbContext.Flights.Find(userTicket.FlightId);
        //    if (fligh == null)
        //        return NotFound();
        //    try
        //    {
        //        SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
        //        client.Port = 587;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = false;
        //        System.Net.NetworkCredential credential = new System.Net.NetworkCredential("flightrezervation@outlook.com", "Nurlanaztu2003.");
        //        client.EnableSsl = true;
        //        client.Credentials = credential;
        //        MailMessage message = new MailMessage("flightrezervation@outlook.com", userTicket.Email);
        //        message.Subject = "Airline Reservation";
        //        message.Body = "Hi " + userTicket.Name + ". " + " Have a nice trip " + "From " + fligh.FromAirport + " To " + fligh.ToAirport + " At " + fligh.DepartureTime;
        //        message.IsBodyHtml = false;
        //        client.Send(message);
        //        fligh.Count = fligh.Count - 1;
        //        UserMails userMails = new UserMails()
        //        {
        //            UserMail = userTicket.Email,
        //            UserName = userTicket.Name,
        //        };
        //        UserMails mails = _applicationDbContext.UserMails.FirstOrDefault(x => x.UserMail.ToLower() == userTicket.Email.ToLower());
        //        if (mails == null)
        //        {
        //            _applicationDbContext.UserMails.Add(userMails);
        //            _applicationDbContext.SaveChanges();
        //        }
        //        _applicationDbContext.UserTickets.Add(userTicket);
        //        _applicationDbContext.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

    }
}
