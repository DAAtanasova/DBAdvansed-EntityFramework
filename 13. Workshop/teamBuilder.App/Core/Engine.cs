using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.App.Core
{
    public class Engine
    {
        private CommandDispatcher commandDispatcher;
        public Engine(CommandDispatcher cmdDisp)
        {
            this.commandDispatcher = cmdDisp;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string result = CommandDispatcher.Dispatch(input);
                    Console.WriteLine(result);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.GetBaseException().Message);
                }
            }
        }
    }
}
