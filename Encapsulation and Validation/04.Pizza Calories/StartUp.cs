using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main()
    {
        string input;
        var allToppings = new List<Topping>();
        try
        {
            var pizzaInput = Console.ReadLine().Split();
            var pizzaName = pizzaInput[1];
            var pizza = new Pizza(pizzaName);

            var doughIngredients = Console.ReadLine().Split();
            string flour = doughIngredients[1];
            string bakingTech = doughIngredients[2];
            double weight = double.Parse(doughIngredients[3]);
            var dough = new Dough(flour, bakingTech, weight);
            pizza.Dough = dough;
          
            while ((input = Console.ReadLine()) != "END")
            {
                var toppingIngredients = input.Split();
                string type = toppingIngredients[1];
                double weightTopping = double.Parse(toppingIngredients[2]);
                var topping = new Topping(type, weightTopping);
                allToppings.Add(topping);
            }
            pizza.Toppings = allToppings;
            //var pizza = new Pizza(pizzaName, dough, allToppings);
            pizza.GetTotalCalories();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

