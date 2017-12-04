using System;
using System.Collections.Generic;
using System.Linq;

namespace BusTicketSystem.Models
{
    public class BusCompany
    {
        public int BusCompanyId { get; set; }

        public string Name { get; set; }

        public string Nationality { get; set; }

        private decimal? rating;

        public decimal? Rating
        {
            get
            {
                this.rating = this.Reviews.Count > 0 ? this.Reviews.Average(a => a.Grade) : 0.0m;
                return this.rating;
            }
            set { this.rating = value; }
        }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
