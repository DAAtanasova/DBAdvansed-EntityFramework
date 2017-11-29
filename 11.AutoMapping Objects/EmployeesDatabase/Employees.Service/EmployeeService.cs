using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Employees.Data;
using Employees.DtoModels;
using Employees.Models;

namespace Employees.Service
{
    public class EmployeeService
    {
        private readonly EmployeesContext db;

        public EmployeeService(EmployeesContext db)
        {
            this.db = db;
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee = db.Employees.Find(employeeId);
            var employeeDto = Mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            bool hasEmployee = db.Employees.Any(e => e.FirstName == employeeDto.FirstName && e.LastName == employeeDto.LastName);
            if (hasEmployee)
            {
                throw new ArgumentException("There is already an employee with given names.");
            }
            var employee = Mapper.Map<Employee>(employeeDto);

            db.Employees.Add(employee);

            db.SaveChanges();
        }

        public string SetBirthday(int employeeId, DateTime birthday)
        {
            var employee = db.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("No employee with given Id");
            }

            employee.Birthday = birthday;
            db.SaveChanges();

            string result = $"{employee.FirstName} {employee.LastName} birthday is set to {birthday:dd-MM-yyyy}";
            return result;
        }

        public string SetAddress(int employeeId, string[] address)
        {
            var employee = db.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("No employee with given Id");
            }

            employee.Address = string.Join(" ", address);
            db.SaveChanges();
            string result = $"{employee.FirstName} {employee.LastName} address is set to {employee.Address}";
            return result;
        }

        public string EmployeeInfo(int employeeId)
        {
            var employee = db.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("No employee with given Id");
            }

            var employeeDto = Mapper.Map<EmployeeDto>(employee);
            string result = employeeDto.ToString();
            return result;
        }

        public string EmployeePersonalInfo(int id)
        {
            var employee = db.Employees.Find(id);

            if (employee == null)
            {
                throw new ArgumentException("No employee with given Id");
            }

            var employeePesonalDto = Mapper.Map<EmployeePersonalDto>(employee);
            string result = employeePesonalDto.ToString();
            return result;
        }

        public string SetManager(int employeeId, int managerId)
        {
            var employee = db.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new ArgumentException("No employee with given Id");
            }

            var manager = db.Employees.Find(managerId);
            if (manager == null)
            {
                throw new ArgumentException("No employee with given Id");
            }

            var employeeDTO = Mapper.Map<EmployeeDto>(employee);
            var managerDTO = Mapper.Map<ManagerDto>(manager);
            var managedEmployees = db.Employees
                 .Where(m => m.ManagerId == managerId)
                 .ProjectTo<EmployeeDto>()
                 .ToList();

            bool hasEmployee = managedEmployees.Any(e => e.EmployeeId == employeeDTO.EmployeeId);
            if (hasEmployee)
            {
                throw new ArgumentException($"{managerDTO.FirstName} {managerDTO.LastName} already is manager to {employeeDTO.FirstName} {employeeDTO.LastName}");
            }

            //managedEmployees.Add(employeeDTO);
            //managerDTO.ManagedEmployees.AddRange(managedEmployees);

            //employee.ManagerId = managerId;
            //db.SaveChanges();

            employee.Manager = manager;
            db.SaveChanges();

            string result = $"{managerDTO.FirstName} {managerDTO.LastName} successfully added as manager to {employeeDTO.FirstName} {employeeDTO.LastName}";
            return result;
        }

        public ManagerDto ManagerInfo(int managerId)
        {
            var manager = db.Employees.Find(managerId);
            var managerDto = Mapper.Map<ManagerDto>(manager);
            var managedEmployees = db.Employees.Where(m => m.ManagerId == managerId)
                .ProjectTo<EmployeeDto>()
                .ToArray();
            if (managerDto.ManagedEmployees.Count == 0)
            {
            managerDto.ManagedEmployees.AddRange(managedEmployees);
            }
            
            return managerDto;
        }

        public List<EmployeeManagerDto> ListEmployeesOlderThan (int age)
        {
            List<EmployeeManagerDto> allOlderThanEmployees = new List<EmployeeManagerDto>();

            var currentDate = DateTime.Today;
            var employees = db.Employees
                .ProjectTo<EmployeeManagerDto>()
                .ToArray();

            foreach (var employee in employees)
            {
                var emplAge = currentDate.Year - employee.Birthday.Year;
                if(currentDate< (employee.Birthday.AddYears(emplAge)))
                {
                    emplAge--;
                }
                if (emplAge >= age)
                {
                    allOlderThanEmployees.Add(employee);
                }
            }

            return allOlderThanEmployees;
        }
    }
}
