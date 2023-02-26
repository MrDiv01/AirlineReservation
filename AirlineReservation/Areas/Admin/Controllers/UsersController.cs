using AirlineReservation.Data;
using AirlineReservation.Helpers;
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
        public IActionResult Index(int page =1)
        {
            var query = _applicationDbContext.UserTickets.AsQueryable();
            //PaginatedList<UserTicket> PagenateduserTickets =new PaginatedList<UserTicket>(query.Skip((page-1)*10).ToList(),query.Count(),page,10);
            var PagenateduserTickets = PaginatedList<UserTicket>.Create(query, 8, page);
            return View(PagenateduserTickets);
        }
    }
}
