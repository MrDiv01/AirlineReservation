using AirlineReservation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Flight> Flights { get; set; }
		
		public DbSet<UserTicket> UserTickets { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Adres> Adresss { get; set; }
		public DbSet<AdminAnsver> AdminAnswer { get; set; }
		public DbSet<Titile> Titiles { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<UserMails> UserMails { get; set; }
		public DbSet<HeaderDec> HeaderDecs { get; set; }
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<TicketImages> TicketImage { get; set; }

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	modelBuilder.Entity<UserTicket>(c => c.Property(e => e.Id).UseIdentityColumn());
		//}
	}
}
