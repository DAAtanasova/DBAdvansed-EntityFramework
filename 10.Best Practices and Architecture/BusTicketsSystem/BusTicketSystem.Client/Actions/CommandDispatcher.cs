using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketSystem.Client.Actions.Command;

namespace BusTicketSystem.Client.Actions
{
    public class CommandDispatcher
    {
        public string DispatchCommand(string[] cmdParams)
        {
            string command = cmdParams[0].ToLower();
            string[] data = cmdParams.ToArray();
            string result = "";

            switch (command)
            {
                case "print-info":
                    result = PrintInfoCommand.Execute(cmdParams);
                    break;
                case "buy-ticket":
                    result = BuyTicketCommand.Execute(cmdParams);
                    break;
                case "publish-review":
                    result = PublishReviewCommand.Execute(cmdParams);
                    break;
                case "print-reviews":
                    result = PrintReviewCommand.Execute(cmdParams);
                    break;
                case "exit":
                    result = ExitCommand.Execute();
                    break;
                case "change-trip-status":
                    result = ChangeTripStatusCommand.Execute(cmdParams);
                    break;
                default: result = "Invalid Command!";
                    break;
            }
            return result;
        }
    }
}
