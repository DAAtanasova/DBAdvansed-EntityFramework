using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketSystem.Models
{
    public class ArrivedTrip
    {
        public int ArrivalTripId { get; set; }

        public DateTime ActualArrivalTime { get; set; }

        //public int OriginBusStationId { get; set; }
        //public BusStation OriginBusStation { get; set; }

        //public int DestinationBusStationId { get; set; }
        //public BusStation DestinationBusStation { get; set; }

        public string OriginBusStationName { get; set; }

        public string DestinationBusStationName { get; set; }

        public int PassengerCount { get; set; }
    }
}
