using System.Linq;
using Employees.Service;

namespace Employees.Client.Command
{
    public class SetAddressCommand : ICommand
    {
        private readonly EmployeeService employeeService;
        public SetAddressCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<employeeId> <address>
        public string Execute(params string[] data)
        {
            int employeeId = int.Parse(data[0]);
            string[] address = data.Skip(1).ToArray();

            string result = employeeService.SetAddress(employeeId, address);
            return result;
        }
    }
}
