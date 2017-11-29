using System.Text;
using Employees.Service;

namespace Employees.Client.Command
{
    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly EmployeeService employeeService;
        public ListEmployeesOlderThanCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<age>
        public string Execute(params string[] data)
        {
            int age = int.Parse(data[0]);

            var employeesManagerDto = employeeService.ListEmployeesOlderThan(age);
            var sb = new StringBuilder();
            foreach (var emp in employeesManagerDto)
            {
                string managerName = (emp.ManagerLastName != null) ? $"{emp.ManagerLastName}" : "[no manager]";
                sb.AppendLine($"{emp.EmployeeFirstName} {emp.EmployeeLastName} - ${emp.EmployeeSalary:f2} - {managerName}");
            }
            string result = sb.ToString().Trim();
            return result;
        }
    }
}
