using System;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;

namespace P02_DatabaseFirst
{
    class Program
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            
            //03. 
            //GetEmployeesFullInfo(context);

            //04.
            //GetEmployeesWithSalaryMoreThan(context);

            //05.
            //EmployeesFromResearchAndDevelopment(context);

            //06.
            //AddAddressUpdateEmployee(context);

            //07.
            //EmployeesAndProjects(context);

            //08.
            //AddressesByTown(context);

            //09.
            //Employee147(context);

            //10.
            //DepWithMoreThan5Employees(context);

            //11.
            //Last10StartedProjects(context);

            //12.
            //IncreaseSalaries(context);

            //13.
            //FirstNamesWithSa(context);

            //14.
            //RemoveProject(context);

            //15.
            //RemoveTown(context);

        }
        //03.
        private static void GetEmployeesFullInfo(SoftUniContext context)
        {
            using (context)
            {
                var employees = context.Employees
                    .Select(e => new
                    {
                        e.EmployeeId,
                        e.FirstName,
                        e.LastName,
                        e.MiddleName,
                        e.JobTitle,
                        e.Salary
                    })
                    .OrderBy(a => a.EmployeeId)
                    .ToList();
                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
                }
            }
        }

        //04.
        private static void GetEmployeesWithSalaryMoreThan (SoftUniContext context)
        {
            using (context)
            {
                var employees = context.Employees
                    .Where(e => e.Salary > 50000)
                    .Select(e => e.FirstName)
                    .OrderBy(e => e)
                    .ToList();

                foreach (var e in employees)
                {
                    Console.WriteLine(e);
                }
            }
        }

        //05.
        private static void EmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            using (context)
            {
                var employees = context.Employees
                    .Where(e => e.Department.Name == "Research and Development")
                    .OrderBy(e=>e.Salary)
                    .ThenByDescending(e=>e.FirstName)
                    .Select(e=> new
                    {
                        e.Department.Name,
                        e.FirstName,
                        e.LastName,
                        e.Salary
                    })
                    .ToList();

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} from {e.Name} - ${e.Salary:f2}");
                }
            }
        }

        //06.
        private static void AddAddressUpdateEmployee(SoftUniContext context)
        {
            using (context)
            {
                var address = new Address()
                {
                    AddressText = "Vitoshka 15",
                    TownId = 4
                };

                Employee employee = context.Employees
                    .FirstOrDefault(e=>e.LastName=="Nakov");
                employee.Address = address;
                context.SaveChanges();

                var employeesAddress = context.Employees
                    .OrderByDescending(a => a.AddressId)
                    .Take(10)
                    .Select(e => e.Address.AddressText);

                foreach (var a in employeesAddress)
                {
                    Console.WriteLine(a);
                }
            }
        }

        //07.
        private static void EmployeesAndProjects(SoftUniContext context)
        {
            using (context)
            {
                var employees = context.Employees.Include(e => e.EmployeesProjects)
                    .ThenInclude(ep => ep.Project)
                    .Where(ep => ep.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                    .Take(30)
                    .Select(e=> new
                    {
                        e.FirstName,
                        e.LastName,
                        e.Manager,
                        e.EmployeesProjects
                    })
                    .ToList();

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} - Manager: {e.Manager.FirstName} {e.Manager.LastName}");
                    foreach (var p in e.EmployeesProjects)
                    {
                        //var startDate = p.Project.StartDate.ToString("M/d/yyyy",CultureInfo.InvariantCulture);
                        //var endDate = p.Project.EndDate.ToString();

                        //if (string.IsNullOrWhiteSpace(endDate))
                        //{
                        //    endDate = "not finished";
                        //}
                        //else
                        //{
                        //    endDate = p.Project.EndDate.Value.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
                        //}

                        string startDate = $"{p.Project.StartDate:M'/'d'/'yyyy h:mm:ss tt}";
                        string endDate = $"{p.Project.EndDate:M'/'d'/'yyyy h:mm:ss tt}";

                        if (string.IsNullOrWhiteSpace(endDate))
                        {
                            endDate = "not finished";
                        }
                        Console.WriteLine($"--{p.Project.Name} - {startDate} - {endDate}");
                    }
                }
            }
        }

        //08.
        private static void AddressesByTown(SoftUniContext context)
        {
            using (context)
            {
                var addresses = context.Addresses
                    .Include(e=>e.Employees)
                    .Include(t=>t.Town)
                    .OrderByDescending(a => a.Employees.Count)
                    .ThenBy(t=>t.Town.Name)
                    .ThenBy(a=>a.AddressText)
                    .Select(a=>new
                    {
                        a.AddressText,
                        tonwName = a.Town.Name,
                        a.Employees.Count
                    })
                    .Take(10)
                    .ToList();

                foreach (var a in addresses)
                {
                    Console.WriteLine($"{a.AddressText},{a.tonwName} - {a.Count} employees");
                }
            }
        }

        //09.
        private static void Employee147 (SoftUniContext context)
        {
            using (context)
            {
                var employee = context.Employees
                    .Include(ep=>ep.EmployeesProjects)
                    .ThenInclude(p=>p.Project)
                    .Select(e => new
                    {
                        e.EmployeeId,
                        e.FirstName,
                        e.LastName,
                        e.JobTitle,
                        e.EmployeesProjects
                    })
                    .FirstOrDefault(id => id.EmployeeId == 147);

                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

                foreach (var pro in employee.EmployeesProjects.OrderBy(n=>n.Project.Name))
                {
                    Console.WriteLine($"{pro.Project.Name}");
                }
            }
        }

        //10.
        private static void DepWithMoreThan5Employees(SoftUniContext context)
        {
            using (context)
            {
                var deparments = context.Departments
                    .Where(d => d.Employees.Count > 5)
                    .Select(d => new
                    {
                        deparmentName = d.Name,
                        d.Manager,
                        d.Employees
                    })
                    .OrderBy(e => e.Employees.Count)
                    .ThenBy(d => d.deparmentName)
                    .ToList();

                foreach (var d in deparments)
                {
                    Console.WriteLine($"{d.deparmentName} - {d.Manager.FirstName} {d.Manager.LastName}");
                    foreach (var e in d.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                    {
                        Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                    }
                    Console.WriteLine(new string('-', 10));
                }
            }
        }

        //11.
        private static void Last10StartedProjects(SoftUniContext context)
        {
            using (context)
            {
                var projects = context.Projects
                    .OrderByDescending(p => p.StartDate)
                    .Take(10)
                    .OrderBy(p=>p.Name)
                    .ToList();

                foreach (var p in projects)
                {
                    Console.WriteLine(p.Name);
                    Console.WriteLine(p.Description);
                    Console.WriteLine(p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
                }
            }
        }

        //12.
        private static void IncreaseSalaries(SoftUniContext context)
        {
            using (context)
            {
                var employees = context.Employees
                    .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design"
                    || e.Department.Name == "Marketing" || e.Department.Name == "Information Services")
                    .OrderBy(e=>e.FirstName)
                    .ThenBy(e=>e.LastName)
                    .ToList();

                foreach (var e in employees)
                {
                    e.Salary = e.Salary * 1.12m;
                    
                }
                context.SaveChanges();

                foreach (var e in employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
                }
            }
        }

        //13.
        private static void FirstNamesWithSa(SoftUniContext context)
        {
            using (context)
            {
                var names = context.Employees
                    .Where(e => e.FirstName.Substring(0, 2) == "Sa")
                    .OrderBy(e=>e.FirstName)
                    .ThenBy(e=>e.LastName)
                    .ToList();
                foreach (var e in names)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
                }    
            }
        }

        //14.
        private static void RemoveProject (SoftUniContext context)
        {
            using (context)
            {
                var project = context.Projects.Find(2);
                var projectsId2 = context.EmployeesProjects.Where(p => p.ProjectId == 2).ToList();

                context.EmployeesProjects.RemoveRange(projectsId2);
                context.Projects.Remove(project);

                context.SaveChanges();

                var outputProjects = context.Projects
                    .Take(10)
                    .Select(p => p.Name)
                    .ToList();
                
                foreach (var p in outputProjects)
                {
                    Console.WriteLine(p);
                }
            }
        }

        //15.
        private static void RemoveTown(SoftUniContext context)
        {
            string inputTown = Console.ReadLine();
            using (context)
            {
                var town = context.Towns.SingleOrDefault(t => t.Name == inputTown);
                var townAddresses = context.Addresses
                    .Where(a => a.Town == town)
                    .ToList();
                var count = townAddresses.Count();

                var employees = context.Employees
                    .Where(e => e.Address.AddressId != null && townAddresses.Any(a => a.AddressId == e.Address.AddressId))
                    .ToList();

                foreach (var e in employees)
                {
                    e.Address = null;
                }

                context.Addresses.RemoveRange(townAddresses);
                context.Towns.Remove(town);
                context.SaveChanges();

                string wordAddress = (count == 1) ? "adress" : "adresses";

                Console.WriteLine($"{count} {wordAddress} in {inputTown} was deleted.");
            }
        }
    }
}
