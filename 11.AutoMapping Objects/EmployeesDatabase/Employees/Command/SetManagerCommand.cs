using Employees.Service;

namespace Employees.Client.Command
{
    public class SetManagerCommand : ICommand
    {
        private readonly EmployeeService employeeService;
        public SetManagerCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<employeeId> <managerId>
        public string Execute(params string[] data)
        {
            int employeeId = int.Parse(data[0]);
            int managerId = int.Parse(data[1]);

            string result = employeeService.SetManager(employeeId, managerId);
            return result;
        }
    }
}
