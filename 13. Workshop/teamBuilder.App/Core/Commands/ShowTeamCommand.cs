using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core.Commands
{
    public class ShowTeamCommand
    {
        //<teamName>
        public static string Execute(string[] data)
        {
            Check.CheckLenght(1, data);

            string teamName = data[0];
            if(!CommandHelper.IsTeamExisting(teamName))
            {
                string msg = string.Format(Constants.ErrorMessages.TeamNotFound, teamName);
                throw new ArgumentException(msg);
            }

            using(var db = new TeamBuilderContext())
            {
                var team = db.Teams
                    .Include(t => t.UsersTeam)
                    .ThenInclude(u => u.User)
                    .SingleOrDefault(t => t.Name == teamName);


                var result = $"{team.Name} {team.Acronym}" 
                    + Environment.NewLine + "Members: " 
                    + Environment.NewLine + "- " 
                    + string.Join(Environment.NewLine + "- ", team.UsersTeam.Select(u => u.User.Username));

                return result;
            }
        }
    }
}
