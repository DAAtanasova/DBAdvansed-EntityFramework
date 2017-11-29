using Employees.Service;

namespace Employees.Client.Command
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;
        public EmployeePersonalInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<employeeId> 
        public string Execute(params string[] data)
        {
            int employeeId = int.Parse(data[0]);
            var result = employeeService.EmployeePersonalInfo(employeeId);
            return result;
        }
    }
}
