using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Dough
{
    private const int minWeight = 1;
    private const int maxWeight = 200;

    private string flour;
    private string bakingTech;
    private double weight;
    private double doughCalories;

    public Dough(string flour, string bakingTech, double weight)
    {
        this.Flour = flour;
        this.BakingTech = bakingTech;
        this.Weight = weight;
        this.doughCalories = GetDoughCalories();
    }

    public string Flour
    {
       get { return this.flour; }
       private set
        {
            if(value.ToLower() != "white" && value.ToLower() != "wholegrain")
            {
                throw new ArgumentException("Invalid type of dough.");
            }
            this.flour = value;
        }
    }
    public string BakingTech
    {
        get { return this.bakingTech; }
        private set
        {
            if(value.ToLower() != "crispy" && value.ToLower() != "chewy" && value.ToLower() != "homemade")
            {
                throw new ArgumentException("Invalid type of dough.");
            }
            this.bakingTech = value;
        }
    }
    public double Weight
    {
        get { return this.weight; }
        private set
        {
            if(value < minWeight || value > maxWeight)
            {
                throw new ArgumentException("Dough weight should be in the range [1..200].");
            }
            this.weight = value;
        }
    }

    public double DoughCalories
    {
        get { return this.doughCalories; }
    }

    public double GetDoughCalories()
    {
        double flourCalperGram = 0;
        double bakingTechCalperGram = 0;
        switch (flour.ToLower())
        {
            case "white": flourCalperGram = 1.5; break;
            case "wholegrain": flourCalperGram = 1.0; break;
        }
        switch (bakingTech.ToLower())
        {
            case "crispy": bakingTechCalperGram = 0.9; break;
            case "chewy": bakingTechCalperGram = 1.1; break;
            case "homemade": bakingTechCalperGram = 1.0;break;
        }
        
        double result = (2 * this.weight) * flourCalperGram * bakingTechCalperGram;
        return result;
       // Console.WriteLine($"{result:f2}");
    }
}
