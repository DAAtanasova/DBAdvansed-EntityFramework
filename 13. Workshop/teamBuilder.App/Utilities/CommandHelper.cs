using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Utilities
{
    public static class CommandHelper
    {
        public static bool IsTeamExisting(string teamName)
        {
            using(var db = new TeamBuilderContext())
            {
                return db.Teams.Any(t => t.Name == teamName);
            }
        }

        public static bool IsUserExisting(string username)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Users.Any(u => u.Username == username);
            }
        }

        //user has to be loaded
        public static bool IsInviteExisting(string teamName, User user)
        {
            using(var db = new TeamBuilderContext())
            {
                return db.Invitations.Any(i => i.Team.Name == teamName && i.InvitedUserId == user.UserId && i.IsActive);
            }
        }

        //user has to be loaded
        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            using(var db = new TeamBuilderContext())
            {
                return db.Teams.Where(t => t.Name == teamName).Any(t => t.CreatorId == user.UserId);
            }
        }

        //user has to be loaded
        public static bool IsUserCreatorOfEvent(string eventName, User user)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Events.Where(e => e.Name == eventName).Any(e => e.CreatorId == user.UserId);
            }
        }

        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (var db = new TeamBuilderContext())
            {
                var isMember = db.Teams
                    .Include(ut=>ut.UsersTeam)
                    .ThenInclude(u=>u.User)
                    .Single(t => t.Name == teamName)
                    .UsersTeam.Any(u=>u.User.Username == username);

                return isMember;
            }
        }

        public static bool IsEventExisting(string eventName)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Events.Any(e => e.Name == eventName);
            }
        }

        public static Event GetLatestEvent(string eventName)
        {
            using (var db = new TeamBuilderContext())
            {
                var eventsWithName = db.Events
                        .Where(e => e.Name == eventName);

                var latestDate = eventsWithName.Max(d => d.StartDate);

                var currentEvent = db.Events
                    .Include(e => e.EventTeams)
                    .SingleOrDefault(e => e.Name == eventName && e.StartDate == latestDate);

                return currentEvent;
            }
        }
    }
}
