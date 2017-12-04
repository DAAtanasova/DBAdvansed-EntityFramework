using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketSystem.Models
{
    public class BusStation
    {
        public int BusStationId { get; set; }

        public string Name { get; set; }
        
        public int TownId { get; set; }
        public Town Town { get; set; }

        public ICollection<Trip> TripsArrivedAt { get; set; } = new List<Trip>();

        public ICollection<Trip> TripsGoFrom { get; set; } = new List<Trip>();
    }
}
