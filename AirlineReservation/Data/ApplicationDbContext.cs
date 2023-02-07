﻿using AirlineReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Flight> Flights { get; set; }
		public DbSet<UserTicket> UserTickets { get; set; }
   //     protected override void OnModelCreating(ModelBuilder modelBuilder)
   //     {
			//modelBuilder.Entity<UserTicketİnfo>(c => c.Property(e => e.Id).UseIdentityColumn());
   //     }
    }
}
