using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Core.Commands;

namespace TeamBuilder.App.Core
{
    public class CommandDispatcher
    {
        public static string Dispatch(string input)
        {
            string result = "";
            string[] inputArgs = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var command = inputArgs.Length > 0 ? inputArgs[0].ToLower() : string.Empty;
            string[] data = inputArgs.Skip(1).ToArray();

            switch (command)
            {
                case "exit":
                    result = Exitcommand.Execute(data);
                    break;
                case "registeruser":
                    result = RegisterUserCommand.Execute(data);
                    break;
                case "login":
                    result = LoginCommand.Execute(data);
                    break;
                case "logout":
                    result = LogoutCommand.Execute(data);
                    break;
                case "deleteuser":
                    result = DeleteUserCommand.Execute(data);
                    break;
                case "createevent":
                    result = CreateEventCommand.Execute(data);
                    break;
                case "createteam":
                    result = CreateTeamCommand.Execute(data);
                    break;
                case "invitetoteam":
                    result = InviteToTeamCommand.Execute(data);
                    break;
                case "acceptinvite":
                    result = AcceptInviteCommand.Execute(data);
                    break;
                case "declaneinvite":
                    result = DeclineInviteCommand.Execute(data);
                    break;
                case "kickmember":
                    result = KickMemberCommand.Execute(data);
                    break;
                case "disbandteam":
                    result = DisbandTeamCommand.Execute(data);
                    break;
                case "addteamto":
                    result = AddTeamToCommand.Execute(data);
                    break;
                case "showevent":
                    result = ShowEventCommand.Execute(data);
                    break;
                case "showteam":
                    result = ShowTeamCommand.Execute(data);
                    break;
                default:
                    throw new NotSupportedException($"Command {command} not valid!");
            }
            return result;
        }
    }
}
