﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Box
{
    private decimal length;
    private decimal width;
    private decimal height;

    public Box(decimal length, decimal width, decimal height)
    {
        this.Length = length;
        this.Width = width;
        this.Height = height;
    }

    public decimal Length
    {
        get { return this.length; }
        private set
        {
            if(value <= 0)
            {
                throw new ArgumentException("Length cannot be zero or negative.");
            }
            this.length = value;
        }
    }
    public decimal Width
    {
        get { return this.width; }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Width cannot be zero or negative.");
            }
            this.width = value;
        }
    }
    public decimal Height
    {
        get { return this.height; }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Height cannot be zero or negative.");
            }
            this.height = value;
        }
    }

    public void SurfaceArea()
    {
        decimal surfArea = 2 * this.length * this.width + 2 * this.length * this.height + 2 * this.width * this.height;
        Console.WriteLine($"Surface Area - {surfArea:f2}");
        //return SurfArea;
    }
    public void LateralArea()
    {
        decimal latArea = 2 * this.length * this.height + 2 * this.width * this.height;
        Console.WriteLine($"Lateral Surface Area - {latArea:f2}");
        //return LatArea;
    }
    public void Volume()
    {
        decimal volume = this.length * this.height * this.width;
        Console.WriteLine($"Volume - {volume:f2}");
        //return Volume;
    }
}


