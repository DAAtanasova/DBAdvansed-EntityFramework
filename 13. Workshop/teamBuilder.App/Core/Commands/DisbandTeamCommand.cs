using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core.Commands
{
    public class DisbandTeamCommand
    {
        //<teamName>
        public static string Execute(string[] data)
        {
            Check.CheckLenght(1, data);

            AuthenticationManager.Autorized();
            var currentUser = AuthenticationManager.GetCurrentUser();

            string teamName = data[0];

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                string errorMsg = string.Format(Constants.ErrorMessages.TeamNotFound, teamName);
                throw new ArgumentException(errorMsg);
            }

            using(var db = new TeamBuilderContext())
            {
                var team = db.Teams
                    //.Include(t => t.Creator)
                    .SingleOrDefault(t => t.Name == teamName);

                if(currentUser.UserId != team.CreatorId)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
                }

                var teamAndMembers = db.UsersTeams.Where(t => t.TeamId == team.TeamId);

                db.UsersTeams.RemoveRange(teamAndMembers);
                db.Teams.Remove(team);
                db.SaveChanges();
            }

            return $"{teamName} has disbanded!";
        }
    }
}
