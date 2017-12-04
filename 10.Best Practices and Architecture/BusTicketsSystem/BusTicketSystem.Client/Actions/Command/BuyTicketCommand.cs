using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketSystem.Client.Actions.Command
{
    public class BuyTicketCommand
    {
        //buy-ticket {customer ID} {Trip ID} {Price} {Seat}
        public static string Execute(string[] data)
        {
            int customerId = int.Parse(data[1]);
            int tripId = int.Parse(data[2]);
            decimal price = decimal.Parse(data[3]);
            int seat = int.Parse(data[4]);

            using(var db = new BusTicketContext())
            {
                var customer = db.Customers.Find(customerId);

                if (customer == null)
                {
                    throw new ArgumentException("No customer with given Id!");
                }

                var trip = db.Trips.Find(tripId);
                if(trip == null)
                {
                    throw new ArgumentException("No trip with given Id!");
                }

                bool isSeatFull = db.Tickets.Any(t => t.TripId == tripId && t.Seat == seat);
                if (isSeatFull)
                {
                    throw new ArgumentException("The seat is already sold! Pick another seat.");
                }

                var customerBankAccount = db.CustomersBankAccounts
                    .Include(c=>c.BankAccount)
                    .SingleOrDefault(c => c.CustomerId == customerId);

                if(customerBankAccount == null)
                {
                    throw new ArgumentException("The customer has not got a bank account. Cannot buy ticket!");
                }

                if(customerBankAccount.BankAccount.Balance < price)
                {
                    throw new ArgumentException("Insufficient funds!");
                }
                customerBankAccount.BankAccount.Balance -= price; 

                var ticket = new Ticket
                {
                    Price = price,
                    Seat = seat,
                    TripId = tripId,
                    Customer = customer
                };
                db.Tickets.Add(ticket);
                db.SaveChanges();

                return "Successfully bought ticket.";
            }
        }
    }
}
