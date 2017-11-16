using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Book
{
    private const int minimumLenght = 3;

    protected string title;
    protected string author;
    protected decimal price;

    public Book( string author,string title, decimal price)
    {
        this.Author = author;
        this.Title = title;
        this.Price = price;
    }

    public string Title
    {
        get { return this.title; }
        set
        {
            if(value.Length < minimumLenght)
            {
                throw new ArgumentException("Title not valid!");
            }
            this.title = value;
        }
    }

    public string Author
    {
        get { return this.author; }
        set
        {
            string[] input = value.Split();
            if (input.Length == 2)
            {
                var secondName = input[1];
                var firstLetter = secondName.Substring(0, 1);
                if (int.TryParse(firstLetter, out int result))
                {
                    throw new ArgumentException("Author not valid!");
                }
            }
            this.author = value;
        }
    }

    public decimal Price
    {
        get { return this.price; }
        set
        {
            if(value <= 0)
            {
                throw new ArgumentException("Price not valid!");
            }
            this.price = value;
        }
    }

    public override string ToString()
    {
        var output = $"Type: {this.GetType().Name}" + Environment.NewLine + $"Title: {this.Title}" +
            Environment.NewLine + $"Author: {this.Author}" + Environment.NewLine + $"Price: {this.Price:f2}";
        return output;
    }

}
