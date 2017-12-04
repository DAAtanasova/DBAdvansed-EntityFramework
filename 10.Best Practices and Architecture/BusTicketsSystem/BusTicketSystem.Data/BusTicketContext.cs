using System;
using BusTicketSystem.Data.Configuration;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketSystem.Data
{
    public class BusTicketContext : DbContext
    {
        public DbSet<BusCompany> BusCompanies { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<BusStation> BusStations { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<CustomerBankAcc> CustomersBankAccounts { get; set; }

        public DbSet<ArrivedTrip> ArrivalTrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(ServerConfig.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BusCompanyConfiguration());

            builder.ApplyConfiguration(new TicketConfiguration());

            builder.ApplyConfiguration(new CustomerConfiguration());

            builder.ApplyConfiguration(new TripConfiguration());

            builder.ApplyConfiguration(new BusStationConfiguration());

            builder.ApplyConfiguration(new TownConfiguration());

            builder.ApplyConfiguration(new ReviewConfiguration());

            builder.ApplyConfiguration(new BankAccountConfiguration());

            builder.ApplyConfiguration(new CustomerBankAccConfiguration());

            builder.ApplyConfiguration(new ArrivalTripConfiguration());
        }
    }
}
