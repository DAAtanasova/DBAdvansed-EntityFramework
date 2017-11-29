using System;

namespace Employees.DtoModels
{
    public class EmployeePersonalDto
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            string result = $"ID: {this.EmployeeId} - {this.FirstName} {this.LastName} - ${this.Salary:f2}" + Environment.NewLine + $"Birthday: {this.Birthday:dd-MM-yyyy}" + Environment.NewLine
                    + $"Address: {this.Address}";
            return result;
        }
    }
}
