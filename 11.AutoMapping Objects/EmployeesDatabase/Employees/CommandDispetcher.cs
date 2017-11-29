using System;
using System.Linq;
using System.Reflection;
using Employees.Client.Command;

namespace Employees.Client
{
    internal class CommandDispetcher
    {
        public static ICommand Parse(IServiceProvider serviceProvider, string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                .ToArray();
           
            var commandType = commandTypes
                .Where(t => t.Name.ToLower() == $"{commandName}command")
                .SingleOrDefault();

            if(commandType == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            var constructor = commandType.GetConstructors()
                .FirstOrDefault();

            var constructorParams = constructor
                .GetParameters()
                .Select(pi => pi.ParameterType);

            var constructorArgs = constructorParams
                .Select(p => serviceProvider.GetService(p))
                .ToArray();

            var command = (ICommand)constructor.Invoke(constructorArgs);

            return command;
        }
    }
}
