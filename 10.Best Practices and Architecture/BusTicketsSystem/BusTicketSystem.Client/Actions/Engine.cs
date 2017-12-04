using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketSystem.Client.Actions
{
    public class Engine
    {
        private readonly CommandDispatcher commandDispatcher;
        public Engine(CommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine().Trim(); ;
                    string[] data = input.Split(' ');
                    var result = this.commandDispatcher.DispatchCommand(data);
                    Console.WriteLine(result);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
