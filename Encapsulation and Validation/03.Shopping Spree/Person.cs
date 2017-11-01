using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Person
{
    private string name;
    private decimal money;
    private List<Product> bag;

    public Person(string name, decimal money)
    {
        this.Name = name;
        this.Money = money;
        this.bag = new List<Product>();
    }

    public string Name
    {
        get { return this.name; }
        set
        {
            if(value == null)
            {
                throw new ArgumentException("Name cannot be empty");
            }
            this.name = value;
        }
    }

    public decimal Money
    {
        get { return this.money; }
        set
        {
            if(value < 0)
            {
                throw new ArgumentException("Money cannot be negative");
            }
            this.money = value;
        }
    }

    public List<Product> Bag
    {
        get { return new List<Product>(this.bag); }
        set { this.bag = new List<Product>(); }
    }

    public void AddProduct(Product product)
    {
        if(product.Price <= this.money)
        {
            this.money = this.money - product.Price;
            this.bag.Add(product);
            Console.WriteLine($"{this.Name} bought {product.Name}");
        }
        else
        {
            Console.WriteLine($"{this.Name} can't afford {product.Name}");
        }
    }

    public override string ToString()
    {
        string result = "";
        if(this.bag.Count == 0)
        {
           result = $"{this.Name} - Nothing bought";
        }
        else
        {
            result = $"{this.Name} - {string.Join(", ", this.bag)}";
        }

        return result;
    }
}
