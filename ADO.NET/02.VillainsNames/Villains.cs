using System;
using System.Collections.Generic;
using System.Text;

public class Villains
{
    public string Name { get; set; }
    public int MinionsCount { get; set; }

    public override string ToString()
    {
        var result = $"{this.Name} - {this.MinionsCount}";
        return result;
    }
}
