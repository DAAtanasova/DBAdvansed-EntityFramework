﻿namespace Employees.DtoModels
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public override string ToString()
        {
            return $"ID: {this.EmployeeId} - {this.FirstName} {this.LastName} - ${this.Salary:f2}";
        }
    }
}
