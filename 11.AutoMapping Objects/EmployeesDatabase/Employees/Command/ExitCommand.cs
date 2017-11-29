using System;

namespace Employees.Client.Command
{
    class ExitCommand : ICommand
    {
        public string Execute(params string[] data)
        {
            Environment.Exit(0);
            return "Goodbye";
        }
    }
}
