using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketSystem.Models
{
    public class Town
    {
        public int TownId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public ICollection<BusStation> BusStations { get; set; } = new List<BusStation>();

        public ICollection<Customer> Customers { get; set; }
    }
}
