using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Pizza
{
    private string name;
    private List<Topping> toppings;
    private Dough dough;

    public Pizza(string name)
    {
        this.Name = name;
    }
    public Pizza(string name, Dough dough, List<Topping> toppings)
    {
        this.Name = name;
        this.Dough = dough;
        this.Toppings = new List<Topping>(toppings);
    }

    public string Name
    {
        get { return this.name; }
        set
        {
            if(value=="" || value.Length > 15)
            {
                throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
            }
            this.name = value;
        }
    }
    public List<Topping> Toppings
    {
        get { return this.toppings; }
        set
        {
            if(value.Count < 0 || value.Count>10)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }
            this.toppings = value;
        }
    }

    public Dough Dough
    {
        get { return this.dough; }
        set { this.dough = value; }
    }

    public void GetTotalCalories()
    {
        double allToppingCal = 0;
        foreach (var topping in this.toppings)
        {
            var toppingCal = topping.ToppingCal;
            allToppingCal = allToppingCal + toppingCal;
        }
        var result = this.dough.DoughCalories + allToppingCal;
        Console.WriteLine($"{this.name} - {result:f2} Calories.");
    }
}
