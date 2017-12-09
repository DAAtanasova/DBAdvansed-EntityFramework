using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core.Commands
{
    public class ShowEventCommand
    {
        //<eventName>
        public static string Execute(string[] data)
        {
            Check.CheckLenght(1, data);

            string eventName = data[0];
            if (!CommandHelper.IsEventExisting(eventName))
            {
                string errorMsg = string.Format(Constants.ErrorMessages.EventNotFound,eventName);
                throw new ArgumentException(errorMsg);
            }

            var result = "";
            using(var db = new TeamBuilderContext())
            {
                var events = db.Events
                    .Include(e => e.EventTeams)
                    .ThenInclude(et => et.Team)
                    .Where(e => e.Name == eventName).ToArray();
                
                var sb = new StringBuilder();
                foreach (var e in events)
                {
                    var teams = e.EventTeams.Count() == 0 ? "[no teams]" : string.Join(Environment.NewLine + "- ", e.EventTeams.Select(t => t.Team.Name));

                    sb.AppendLine($"{e.Name} {e.StartDate}  {e.EndDate}"
                        + Environment.NewLine + e.Description
                        + Environment.NewLine + "Teams: " + Environment.NewLine + "- " + teams);

                }
               result = sb.ToString().Trim();
            }
            return result;
        }
    }
}
