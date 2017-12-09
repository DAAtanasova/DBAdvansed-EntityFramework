using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class AddTeamToCommand
    {
        //<eventName> <teamName>
        public static string Execute(string[] data)
        {
            Check.CheckLenght(2, data);

            AuthenticationManager.Autorized();
            var currentUser = AuthenticationManager.GetCurrentUser();

            string eventName = data[0];
            string teamName = data[1];

            if (!CommandHelper.IsEventExisting(eventName))
            {
                string errorMsg = string.Format(Constants.ErrorMessages.EventNotFound, eventName);
                throw new ArgumentException(errorMsg);
            }

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                string errorMsg = string.Format(Constants.ErrorMessages.TeamNotFound, teamName);
                throw new ArgumentException(errorMsg);
            }

            if (!CommandHelper.IsUserCreatorOfEvent(eventName, currentUser))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            var currentEvent = CommandHelper.GetLatestEvent(eventName);

            using(var db = new TeamBuilderContext())
            {
                var team =db.Teams.SingleOrDefault(t => t.Name == teamName);

                bool isTeamAddedToEvent = db.TeamsEvents.Any(te => te.EventId == currentEvent.EventId && te.TeamId == team.TeamId);

                if (isTeamAddedToEvent)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.CannotAddSameTeamTwice);
                }

                var teamToAddToEvent = new TeamEvent
                {
                    TeamId = team.TeamId,
                    EventId = currentEvent.EventId
                };

                db.TeamsEvents.Add(teamToAddToEvent);
                db.SaveChanges();
            }
            return $"Team {teamName} added for {eventName}!";
           
        }
    }
}
