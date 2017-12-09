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
    public class KickMemberCommand
    {
        //<teamName> <username>
        public static string Execute(string[] data)
        {
            Check.CheckLenght(2, data);

            //loginFirst
            AuthenticationManager.Autorized();
            var currentUser = AuthenticationManager.GetCurrentUser();

            string teamName = data[0];
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                string errorMsg = string.Format(Constants.ErrorMessages.TeamNotFound, teamName);
                throw new ArgumentException(errorMsg);
            }

            string username = data[1];
            if (!CommandHelper.IsUserExisting(username))
            {
                string errorMsg = string.Format(Constants.ErrorMessages.UserNotFound,username);
                throw new ArgumentException(errorMsg);
            }

            bool isUsernameMemberOfTeam = CommandHelper.IsMemberOfTeam(teamName, username);
            if (!isUsernameMemberOfTeam)
            {
                string errorMsg = string.Format(Constants.ErrorMessages.NotPartOfTeam, username, teamName);
                throw new ArgumentException(errorMsg);
            }

            if (!CommandHelper.IsUserCreatorOfTeam(teamName, currentUser))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            using(var db = new TeamBuilderContext())
            {
                var team = db.Teams
                    .Include(t=>t.Creator)
                    .Include(ut=>ut.UsersTeam)
                    .SingleOrDefault(t => t.Name == teamName);

                if(team.Creator.Username == username)
                {
                    string errorMsg = string.Format(Constants.ErrorMessages.CommandNotAllowed, "DisbandTeam");
                    throw new InvalidOperationException(errorMsg);
                }

                var user = db.Users.SingleOrDefault(u => u.Username == username);

                var userTeamToRemove = db.UsersTeams
                    .SingleOrDefault(u => u.UserId == user.UserId && u.TeamId == team.TeamId);

                db.UsersTeams.Remove(userTeamToRemove);
                db.SaveChanges();

                return $"User {username} was kicked from {teamName}!";
            }
        }
    }
}
