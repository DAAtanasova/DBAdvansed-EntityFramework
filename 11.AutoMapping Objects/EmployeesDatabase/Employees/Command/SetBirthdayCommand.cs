using System;
using System.Globalization;
using Employees.Service;

namespace Employees.Client.Command
{
    public class SetBirthdayCommand : ICommand
    {
        private readonly EmployeeService employeeService;
        public SetBirthdayCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        //<employeeId> <date - "dd-MM-yyyy">
        public string Execute(params string[] data)
        {
            int employeeId = int.Parse(data[0]);
            DateTime birhtday = DateTime.ParseExact(data[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            string result = employeeService.SetBirthday(employeeId, birhtday);
            return result;
        }
    }
}
