using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateEventCommand
    {
        // <name> <description> <startDate> <endDate>
        public static string Execute(string[] data)
        {
            Check.CheckLenght(6, data);

            //login first
            AuthenticationManager.Autorized();

            string[] inputStartDateArr = data.Skip(2).Take(2).ToArray();
            string inputStartDate = string.Join(" ", inputStartDateArr);

            string[] inputEndDateArr = data.Skip(4).Take(2).ToArray();
            string inputEndDate = string.Join(" ", inputEndDateArr);

            DateTime startDate;
            DateTime endDate;
            var isValidStartDate = DateTime.TryParseExact(inputStartDate, Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

            var isValidEndDate = DateTime.TryParseExact(inputEndDate, Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);

            if (!isValidStartDate || !isValidEndDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            if(startDate > endDate)
            {
                throw new ArgumentException("Start date should be before end date.");
            }

            string name = data[0];
            string description = data[1];
            var currentUser = AuthenticationManager.GetCurrentUser();

            var newEvent = new Event
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                CreatorId = currentUser.UserId
            };

            using(var db = new TeamBuilderContext())
            {
                db.Events.Add(newEvent);
                db.SaveChanges();
            }

            return $"Event {name} was created successfully!";
        }
    }
}
