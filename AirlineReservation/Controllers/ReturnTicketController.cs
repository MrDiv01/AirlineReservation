using AirlineReservation.Data;
using AirlineReservation.DTOs.ReturnTicketDto;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace AirlineReservation.Controllers
{
    public class ReturnTicketController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReturnTicketController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ReturnTicketDto returnTicketDto)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Boş Qala Bilməz");
                return View();
            }
            if(returnTicketDto.DepartureTime < DateTime.Now)
            {
                ModelState.AddModelError("DepartureTime", "Secilen Tarix Cari Tearicden Kicik ola bilmez");
                return View();
            }
            Flight flight = _applicationDbContext.Flights.FirstOrDefault(c=>c.DepartureTime == returnTicketDto.DepartureTime && 
                                                            c.FAirportName.ToLower() == returnTicketDto.FAirportName.ToLower() &&
                                                            c.TAirportName.ToLower() == returnTicketDto.TAirportName.ToLower() &&
                                                            c.FromAirport.ToLower() == returnTicketDto.FromAirport.ToLower() &&
                                                            c.ToAirport.ToLower() == returnTicketDto.ToAirport.ToLower());
            if (flight == null)
            {
                return RedirectToAction("Errors", "Error");
            }
            UserTicket userTicket = _applicationDbContext.UserTickets.FirstOrDefault(c=>c.Fincode.ToLower() == returnTicketDto.FinCode.ToLower() &&
                                                                                                        c.FlightId == flight.Id);
            if(userTicket == null)
            {
                return RedirectToAction("Errors","Error");
            }
            _applicationDbContext.UserTickets.Remove(userTicket);
            flight.Count++;
            _applicationDbContext.SaveChanges();
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
                message.Body = "Hi " + userTicket.Name + " " +"Return The Ticket";
                message.IsBodyHtml = false;
                client.Send(message);
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
