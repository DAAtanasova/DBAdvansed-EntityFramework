using System;
using System.Collections.Generic;
using System.Text;
using BusTicketSystem.Models.Validation;

namespace BusTicketSystem.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public string Content { get; set; }

       //[Grade(ErrorMessage = "Invalid value")]
        public decimal Grade { get; set; }

        public DateTime PublishedOn { get; set; }

        public int BusCompanyId { get; set; }
        public BusCompany BusCompany { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
