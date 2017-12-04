using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusTicketSystem.Client.Actions;
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketSystem.Client
{
    public class StartUp
    {
        static void Main()
        {
            //ResetDatabase();

            CommandDispatcher cmdDispatcher = new CommandDispatcher();
            Engine engine = new Engine(cmdDispatcher);
            engine.Run();

        }

        private static void ResetDatabase()
        {
            using (var db = new BusTicketContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();

                Seed();
            }
        }

        private static void Seed()
        {
            using (var db = new BusTicketContext())
            {
                var busCompanies = new List<BusCompany>
                {
                    new BusCompany { Name = "ETAP", Nationality = "BG" },
                    new BusCompany { Name = "BioMet" },
                    new BusCompany { Name = "UnionIvconi"}
                };
                db.BusCompanies.AddRange(busCompanies);

                var towns = new List<Town>
                {
                    new Town { Name = "Veliko Tyrnovo", Country = "Bulgaria"},
                    new Town { Name = "Sofia", Country = "Bulgaria"},
                    new Town { Name = "Varna", Country = "Bulgaria"},
                    new Town { Name = "Ruse", Country = "Bulgaria"},
                    new Town { Name = "Paris", Country = "France"},
                    new Town { Name = "London", Country = "England"},
                    new Town { Name = "Plovdiv", Country = "Bulgaria"}
                };
                db.Towns.AddRange(towns);

                db.SaveChanges();

                var busStations = new List<BusStation>
                {
                    new BusStation{ Name = "Avtogara Iug", TownId = 1},
                    new BusStation{ Name = "Avtogara Sever", TownId = 1},
                    new BusStation{ Name = "Central Railway Station", TownId = 2},
                    new BusStation{ Name = "Hotel Pliska", TownId = 2},
                    new BusStation{ Name = "SeaViewStop", TownId = 3},
                };
                db.BusStations.AddRange(busStations);

                db.SaveChanges();

                var trips = new List<Trip>
                {
                    new Trip {
                        DepartureTime = DateTime.Parse("30-11-2017 07:30"),
                        Status = TripStatus.arrived,
                        OriginBusStationId = 1,
                        DestinationBusStationId = 3,
                        BusCompanyId = 1  },

                     new Trip {
                         DepartureTime = DateTime.Parse("28-11-2017 12:30"),
                        Status = TripStatus.delayed,
                        OriginBusStationId = 3,
                        DestinationBusStationId = 5,
                        BusCompanyId = 3  },

                      new Trip {
                        DepartureTime = DateTime.Parse("30-11-2017 15:00"),
                        Status = TripStatus.departed,
                        OriginBusStationId = 4,
                        DestinationBusStationId = 3,
                        BusCompanyId = 2  },

                       new Trip {
                        DepartureTime = DateTime.Parse("12-12-2017 08:30"),
                        Status = TripStatus.cancelled,
                        OriginBusStationId = 1,
                        DestinationBusStationId = 4,
                        BusCompanyId = 3  },

                       new Trip {
                        DepartureTime = DateTime.Parse("30-11-2017 15:30"),
                        Status = TripStatus.arrived,
                        OriginBusStationId = 5,
                        DestinationBusStationId = 2,
                        BusCompanyId = 1  }
                };
                db.Trips.AddRange(trips);

                var customers = new List<Customer>
                {
                    new Customer {FirstName = "Pesho", LastName = "Petkov", Gender = Gender.male, HomeTownId = 1},
                    new Customer {FirstName = "Ivan", LastName = "Ivanov", HomeTownId = 2},
                    new Customer {FirstName = "Merry", LastName = "Santa", Gender = Gender.female, HomeTownId = 3},
                    new Customer {FirstName = "Katq", LastName = "Georgieva", Gender = Gender.notSpecified, HomeTownId = 4},
                    new Customer {FirstName = "Gosho", LastName = "Goshov", Gender = Gender.male, HomeTownId = 5},
                };
                db.Customers.AddRange(customers);

                var bankAccs = new List<BankAccount>
                {
                    new BankAccount{ AccountNumber = "123BB32", Balance = 56.3m},
                    new BankAccount{ AccountNumber = "9856TY0", Balance = 560m},
                    new BankAccount{ AccountNumber = "2569KT6", Balance = 15000m},
                    new BankAccount{ AccountNumber = "568KG65", Balance = 1500m},
                    new BankAccount{ AccountNumber = "567GG32", Balance = 150m}
                };
                db.BankAccounts.AddRange(bankAccs);

                db.SaveChanges();

                var tickets = new List<Ticket>
                {
                    new Ticket{ Price = 20m, Seat = 7, CustomerId = 1, TripId = 2},
                    new Ticket{ Price = 16m, Seat = 42, CustomerId = 2, TripId = 2},
                    new Ticket{ Price = 21m, Seat = 14, CustomerId = 5, TripId = 3},
                    new Ticket{ Price = 15m, Seat = 4, CustomerId = 1, TripId = 4}
                };
                db.Tickets.AddRange(tickets);

                var reviews = new List<Review>
            {
                new Review {Grade = 5.6m, BusCompanyId = 1, CustomerId = 1 },
                new Review {Grade = 10.0m, BusCompanyId = 1, CustomerId = 2 },
                new Review {Grade = 8.6m, BusCompanyId = 2, CustomerId = 3 },
                new Review {Grade = 9.0m, BusCompanyId = 3, CustomerId = 3 },
                new Review {Grade = 5.0m, BusCompanyId = 2, CustomerId = 4 },
                new Review {Grade = 8.9m, BusCompanyId = 1, CustomerId = 5 },
            };
                db.Reviews.AddRange(reviews);

                var customerBankAccs = new List<CustomerBankAcc>
                {
                    new CustomerBankAcc{CustomerId = 1,BankAccountId = 1},
                    new CustomerBankAcc{CustomerId = 2,BankAccountId = 4},
                    new CustomerBankAcc{CustomerId = 3,BankAccountId = 2},
                    new CustomerBankAcc{CustomerId = 4,BankAccountId = 3},
                    new CustomerBankAcc{CustomerId = 5,BankAccountId = 5},
                };
                db.CustomersBankAccounts.AddRange(customerBankAccs);
                
                db.SaveChanges();
            }
        }
    }
}
