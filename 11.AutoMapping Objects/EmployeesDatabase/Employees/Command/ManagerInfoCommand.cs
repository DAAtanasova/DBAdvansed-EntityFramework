using Employees.DtoModels;
using Employees.Service;

namespace Employees.Client.Command
{
    public class ManagerInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;
        public ManagerInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<employeeId> 
        public string Execute(params string[] data)
        {
            int managerId = int.Parse(data[0]);
            ManagerDto managerDto = employeeService.ManagerInfo(managerId);

            string result = managerDto.ToString();
            return result;
        }
    }
}
