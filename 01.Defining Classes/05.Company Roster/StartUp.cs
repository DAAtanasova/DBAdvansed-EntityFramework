using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StartUp
{
    static void Main()
    {
        int lines = int.Parse(Console.ReadLine());
        var allDepartments = new Dictionary<string, List<Employee>>();
        for (int i = 0; i < lines; i++)
        {
            var input = Console.ReadLine().Split();
            string name = input[0];
            double salary = double.Parse(input[1]);
            string position = input[2];
            string department = input[3];
            var employee = new Employee(name, salary, position, department);

            if (input.Length == 5)
            {
                var fifthArg = input[4];
                try
                {
                    int age = int.Parse(fifthArg);
                    employee.Age = age;
                }
                catch
                {
                    string email = fifthArg;
                    employee.Email = email;
                }
            }
            else if (input.Length == 6)
            {
                string email = input[4];
                int age = int.Parse(input[5]);
                employee.Email = email;
                employee.Age = age;
            }

            if(!allDepartments.ContainsKey(department))
            {
                allDepartments.Add(department, new List<Employee>());
            }
            allDepartments[department].Add(employee);
        }

        double highestAvgSalary = 0;
        string highestSalaryDep="";

        foreach (var d in allDepartments)
        {
            double depSalary = 0;
            foreach (var e in d.Value)
            {
                depSalary += e.Salary;
            }
            double avgSalary = depSalary / d.Value.Count();
            if(avgSalary>highestAvgSalary)
            {
                highestAvgSalary = avgSalary;
                highestSalaryDep = d.Key;
            }
        }
        Console.WriteLine($"Highest Average Salary: {highestSalaryDep}");
        foreach (var e in allDepartments[highestSalaryDep].OrderByDescending(e => e.Salary))
        {
            Console.WriteLine($"{e.Name} {e.Salary:f2} {e.Email} {e.Age}");
        }

    }
}
