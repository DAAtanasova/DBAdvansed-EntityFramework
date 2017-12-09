using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateTeamCommand
    {
        //<name> <acronym> <description>
        public static string Execute(string[] data)
        {
            Check.CheckCreateTeamCommandLength(2, 3, data);

            //login first
            AuthenticationManager.Autorized();
            var currentUser = AuthenticationManager.GetCurrentUser();

            string teamName = data[0];
            if (CommandHelper.IsTeamExisting(teamName))
            {
                string errorMsg = string.Format(Constants.ErrorMessages.TeamExists, teamName);
                throw new ArgumentException(errorMsg);
            }

            string acronym = data[1];
            if(acronym.Length != 3)
            {
                string errorMsg = string.Format(Constants.ErrorMessages.InvalidAcronym, acronym);
                throw new ArgumentException(errorMsg);
            }

            string description = data.Length == 3 ? data[2] : null;

            var team = new Team
            {
                Name = teamName,
                Description = description,
                Acronym = acronym,
                CreatorId = currentUser.UserId
            };

            var usersInTeam = new UserTeam
            {
                Team = team,
                UserId = currentUser.UserId
            };


            using (var db = new TeamBuilderContext())
            {
                db.Teams.Add(team);
                db.UsersTeams.Add(usersInTeam);
                db.SaveChanges();

            }

            return $"Team {teamName} successfully created!";
            
        }
    }
}
