using System;

namespace Employees.DtoModels
{
    public class EmployeeManagerDto
    {
        public string EmployeeFirstName { get; set; }

        public string EmployeeLastName { get; set; }

        public DateTime Birthday { get; set; }

        public string ManagerLastName { get; set; }

        public decimal EmployeeSalary { get; set; }
        
    }
}
