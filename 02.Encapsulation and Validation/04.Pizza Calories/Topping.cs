using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Topping
{
    private const int minWeight = 1;
    private const int maxWeight = 50;
    private string type;
    private double weight;
    private double toppingCal;

    public Topping(string type, double weight)
    {
        this.Type = type;
        this.Weight = weight;
        this.toppingCal = GetToppingCalories();
    }

    public string Type
    {
        get { return this.type; }
        private set
        {
            if(value.ToLower() != "meat" && value.ToLower() != "veggies" 
                && value.ToLower() != "cheese" && value.ToLower() != "sauce")
            {
                throw new ArgumentException($"Cannot place {value} on top of your pizza.");
            }
            this.type = value;
        }
    }
    public double Weight
    {
        get { return this.weight; }
        private set
        {
            if(value < minWeight || value > maxWeight)
            {
                throw new ArgumentException($"{this.type} weight should be in the range [1..50].");
            }
            this.weight = value;
        }
    }
    public double ToppingCal
    {
        get { return this.toppingCal; }
    }

    public double GetToppingCalories()
    {
        double typeCalperGram = 0;
        switch (type.ToLower())
        {
            case "meat": typeCalperGram = 1.2; break;
            case "veggies": typeCalperGram = 0.8; break;
            case "cheese": typeCalperGram = 1.1; break;
            case "sauce": typeCalperGram = 0.9; break;
        }
        double result = 2 * this.weight * typeCalperGram;
        return result;
        //Console.WriteLine($"{result:f2}");
    }
}
