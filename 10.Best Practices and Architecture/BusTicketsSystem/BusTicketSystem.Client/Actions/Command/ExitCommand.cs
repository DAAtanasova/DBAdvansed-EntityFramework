using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketSystem.Client.Actions.Command
{
    public class ExitCommand
    {
        public static string Execute()
        {
            Environment.Exit(0);
            return "Bye!";
        }
    }
}
