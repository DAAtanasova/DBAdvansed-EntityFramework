using Employees.DtoModels;
using Employees.Service;

namespace Employees.Client.Command
{
    public class AddEmployeeCommand : ICommand
    {
        private readonly EmployeeService employeeService;
        public AddEmployeeCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //<firstName> <LastName> <salary>
        public string Execute(params string[] args)
        {
            string firstName = args[0];
            string lastName = args[1];
            decimal salary = decimal.Parse(args[2]);

            var employeeDto = new EmployeeDto
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            employeeService.AddEmployee(employeeDto);

            string result = "Employee was added succesfully";
            return result;
        }
    }
}
