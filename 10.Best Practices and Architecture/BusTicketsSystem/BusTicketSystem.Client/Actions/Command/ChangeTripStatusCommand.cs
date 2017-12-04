using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketSystem.Client.Actions.Command
{
    public class ChangeTripStatusCommand
    {
        //change-trip-status {Trip Id} {New Status}
        public static string Execute(string[] data)
        {
            int tripId = int.Parse(data[1]);
            string newStatus = data[2];
            string result = "";

            using(var db = new BusTicketContext())
            {
                var trip = db.Trips
                    .Include(t=>t.OriginBusStation)
                    .Include(t=>t.DestinationBusStation)
                    .SingleOrDefault(t=>t.TripId==tripId);

                if (trip == null)
                {
                    throw new ArgumentException("No such trip.");
                }

                bool isStatus = Enum.IsDefined(typeof(TripStatus), newStatus);
                if (!isStatus)
                {
                    throw new ArgumentException("Status must be departed,arrived,delayed or cancelled");
                }

                if(trip.Status.ToString() == newStatus)
                {
                    throw new ArgumentException("The current status of the trip is the given one.");
                }
                var oldStatus = trip.Status.ToString();
                trip.Status = Enum.Parse<TripStatus>(newStatus);
                result = $"Trip from {trip.OriginBusStation.Name} to {trip.DestinationBusStation.Name} change {oldStatus} to {newStatus}";

                if(newStatus == TripStatus.arrived.ToString())
                {
                    var passangers = db.Tickets.Where(t => t.TripId == tripId).Count();
                    var arrivalTrip = new ArrivedTrip
                    {
                        OriginBusStationName = trip.OriginBusStation.Name,
                        DestinationBusStationName = trip.DestinationBusStation.Name,
                        PassengerCount = passangers
                    };
                    db.ArrivalTrips.Add(arrivalTrip);
                    result = result + Environment.NewLine + $"On {DateTime.Now} - {passangers} passengers arrived  at {trip.DestinationBusStation.Name} from {trip.OriginBusStation.Name}";
                }
                db.SaveChanges();
                return result;
            }
        }
    }
}
