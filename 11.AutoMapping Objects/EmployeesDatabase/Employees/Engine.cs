using System;
using System.Linq;

namespace Employees.Client.Core
{
    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine().Trim();
                    string[] data = input.Split(' ');
                    string commandName = data[0].ToLower();
                    string[] args = data.Skip(1).ToArray();

                    var command = CommandDispetcher.Parse(serviceProvider, commandName);

                    var result = command.Execute(args);

                    Console.WriteLine(result);
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}
