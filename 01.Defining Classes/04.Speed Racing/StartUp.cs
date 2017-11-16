using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    static void Main()
    {
        int inputCars = int.Parse(Console.ReadLine());
        var allCars = new List<Car>();
        for (int i = 0; i < inputCars; i++)
        {
            var inputCar = Console.ReadLine().Split();
            string model = inputCar[0];
            int fuelAmount = int.Parse(inputCar[1]);
            double consumption = double.Parse(inputCar[2]);
            if (!(allCars.Any(c => c.Model == model)))
            {
                var newCar = new Car(model, fuelAmount, consumption);
                allCars.Add(newCar);
            }
        }
        string input;
        while((input = Console.ReadLine()) != "End")
        {
            var args = input.Split();
            var model = args[1];
            double distanceToMove = double.Parse(args[2]);

            var car = allCars.Find(c => c.Model == model);
            var isMoved = car.Move(distanceToMove);
            if(isMoved == false)
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
        }
        foreach (var car in allCars)
        {
            Console.WriteLine($"{car.Model} {car.FuelAmount:f2} {car.Distance}");
        }
    }
}

