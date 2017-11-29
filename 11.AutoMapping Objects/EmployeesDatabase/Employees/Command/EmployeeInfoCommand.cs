using Employees.Service;

namespace Employees.Client.Command
{
    public class EmployeeInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;
        public EmployeeInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<employeeId>
        public string Execute(params string[] data)
        {
            int employeeId = int.Parse(data[0]);

            string result = employeeService.EmployeeInfo(employeeId);
            return result;
        }
    }
}
