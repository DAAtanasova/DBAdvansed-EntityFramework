using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models.Validation;

namespace BusTicketSystem.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        //[Gender]
        public Gender Gender { get; set; }

        public int HomeTownId { get; set; }
        public Town HomeTown { get; set; }

        public CustomerBankAcc CustomerBankAccount { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
