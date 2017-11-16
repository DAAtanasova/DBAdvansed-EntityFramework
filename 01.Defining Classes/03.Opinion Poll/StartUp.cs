using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

class StartUp
{
    public static void Main(string[] args)
    {
        int lines = int.Parse(Console.ReadLine());
        var allPeople = new List<Person>();
        for(int i = 0;i<lines;i++)
        {
            var input = Console.ReadLine().Split();
            string name = input[0];
            int age = int.Parse(input[1]);
            var person = new Person(name,age);
            allPeople.Add(person);
        }
        List<Person> result = allPeople.Where(p => p.Age > 30).OrderBy(p => p.Name).ToList();

        foreach (var person in result)
        {
            Console.WriteLine($"{person.Name} - {person.Age}");
        }

    }


}



