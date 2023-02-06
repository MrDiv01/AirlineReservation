using AirlineReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Flight> flights { get; set; }
		public DbSet<UserTicketİnfo> UserTicketİnfos { get; set; }
	}
}
