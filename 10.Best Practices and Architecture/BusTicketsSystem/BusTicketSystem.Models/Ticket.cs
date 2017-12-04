using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketSystem.Models
{
    public class Ticket
    {
        public Ticket() { }

        //public Ticket(decimal price,int seat,Customer customer, Trip trip)
        //{
        //    this.Price = price;
        //    this.Seat = seat;
        //    this.Customer = customer;
        //    this.Trip = trip;
        //}

        public int TicketId { get; set; }

        public decimal Price { get; set; }

        public int Seat { get; set; }

        //has one customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        //is for one trip
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
