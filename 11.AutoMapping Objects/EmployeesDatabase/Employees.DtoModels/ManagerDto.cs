using System.Collections.Generic;
using System.Text;

namespace Employees.DtoModels
{
    public class ManagerDto
    {
        public int ManagerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<EmployeeDto> ManagedEmployees { get; set; }

        public int ManagedEmployeesCount { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.FirstName} {this.LastName} | Employees: {this.ManagedEmployees.Count}");
            foreach (var empl in this.ManagedEmployees)
            {
                sb.AppendLine($"-{empl.FirstName} {empl.LastName} - ${empl.Salary:f2}");
            }
            
            return sb.ToString().Trim();
        }
    }
}
