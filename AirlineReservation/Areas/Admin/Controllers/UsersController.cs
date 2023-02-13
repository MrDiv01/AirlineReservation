using AirlineReservation.Data;
using AirlineReservation.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UsersController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
          List<UserTicket> userTicket = _applicationDbContext.UserTickets.ToList();
            return View(userTicket);
        }
    }
}
