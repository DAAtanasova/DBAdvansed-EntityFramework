using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main()
    {
        string[] studentInput = Console.ReadLine().Split();
        string studentFirstName = studentInput[0];
        string studentLastName = studentInput[1];
        string facultyNum = studentInput[2];

        string[] workerInput = Console.ReadLine().Split();
        string workerFirstName = workerInput[0];
        string workerLastName = workerInput[1];
        decimal weekSalary = decimal.Parse(workerInput[2]);
        decimal workHourPerDay = decimal.Parse(workerInput[3]);

        try
        {
            Student student = new Student(studentFirstName, studentLastName, facultyNum);
            Worker worker = new Worker(workerFirstName, workerLastName, weekSalary, workHourPerDay);

            Console.WriteLine(student + Environment.NewLine);
            Console.WriteLine(worker);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}