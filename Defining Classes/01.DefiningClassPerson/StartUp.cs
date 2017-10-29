using System;
using System.Reflection;

class StartUp
    {
        public static void Main(string[] args)
        {
        var person = new Person()
        {
            Name = "Pesho",
            Age = 25
        };

        //Required for Judge; Given from SoftUni
        //Type personType = typeof(Person);
        //PropertyInfo[] properties = personType.GetProperties
        //    (BindingFlags.Public | BindingFlags.Instance);
        //Console.WriteLine(properties.Length);

        Console.WriteLine($"{person.Name} is {person.Age} years old.");

        }

    }

