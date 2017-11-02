using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Human
{
    private const int minLenght = 3;

    protected string firstName;
    protected string lastName;

    public Human(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public string FirstName
    {
        get { return this.firstName; }
        set
        {
            // char firstLetter = char.Parse(value.Substring(0, 1));
            if (!Char.IsUpper(value, 0))
            {
                throw new ArgumentException($"Expected upper case letter! Argument: firstName");
            }
            if (value.Length <= minLenght)
            {
                throw new ArgumentException($"Expected length at least 4 symbols! Argument: firstName");
            }
            this.firstName = value;
        }
    }

    public string LastName
    {
        get { return this.lastName; }
        set
        {
            if (!Char.IsUpper(value, 0))
            {
                throw new ArgumentException($"Expected upper case letter! Argument: lastName");
            }
            if (value.Length <= minLenght - 1)
            {
                throw new ArgumentException($"Expected length at least 3 symbols! Argument: lastName");
            }
            this.lastName = value;
        }
    }
    public override string ToString()
    {
        var result = $"First Name: {this.FirstName}" + Environment.NewLine + $"Last Name: {this.LastName}";
        return result;
    }
}
