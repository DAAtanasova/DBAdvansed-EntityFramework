using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BusTicketSystem.Client.Actions.Command
{
    public class PrintInfoCommand
    {
        //print information for trips for a given bus station –  Print a list of arrivals and departures buses for given bus station id 
        //print-info <busStationId>
        public static string Execute(string[] data)
        {
            int busStationId = int.Parse(data[1]);

            using(var db = new BusTicketContext())
            {
                var busStation = db.BusStations
                    .Include(b=>b.Town)
                    .Include(b=>b.TripsArrivedAt)
                    .ThenInclude(bs=>bs.OriginBusStation)
                    .ThenInclude(obs=>obs.Town)
                    .Include(b=>b.TripsGoFrom)
                    .ThenInclude(bs=>bs.DestinationBusStation)
                    .ThenInclude(dbs=>dbs.Town)
                    .SingleOrDefault(b=>b.BusStationId == busStationId);

                if (busStation == null)
                {
                    throw new ArgumentException("No BusStation with the given Id!");
                }

                var sb = new StringBuilder();
                sb.AppendLine($"{busStation.Name}, {busStation.Town.Name}");
                if (busStation.TripsArrivedAt.Count != 0)
                {
                    sb.AppendLine("Arrivals: ");
                    foreach (var trip in busStation.TripsArrivedAt)
                    {
                        string arrivalTime = trip.ArrivalTime == null ? "[no information]" : trip.ArrivalTime.Value.ToShortTimeString();
                        sb.AppendLine($"From: {trip.OriginBusStation.Name} ({trip.OriginBusStation.Town.Name}) | Arrive at: {arrivalTime} | Status: {trip.Status}");
                    }
                }

                if (busStation.TripsGoFrom.Count != 0)
                {
                    sb.AppendLine("Departures:");
                    foreach (var trip in busStation.TripsGoFrom)
                    {
                        var departureTime = trip.DepartureTime.ToShortTimeString();
                        var destStation = trip.DestinationBusStation.Name;
                        sb.AppendLine($"To: {trip.DestinationBusStation.Name} ({trip.DestinationBusStation.Town.Name}) | Depart at: {departureTime} | Status: {trip.Status}");
                    }
                }
                return sb.ToString().Trim(); 
            }
        }
    }
}
