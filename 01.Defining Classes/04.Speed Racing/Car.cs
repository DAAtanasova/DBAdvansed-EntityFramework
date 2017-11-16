using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Car
{
    public string Model { get; set; }
    public double FuelAmount { get; set; }
    public double ConsumptionForKm { get; set; }
    public double Distance { get; set; }

    public Car(string model, int fuelAmount, double consumption)
    {
        this.Model = model;
        this.FuelAmount = fuelAmount;
        this.ConsumptionForKm = consumption;
        this.Distance = 0;
    }

    public bool Move(double distance)
    {
        double fuelNeeded = distance * this.ConsumptionForKm;
        if (fuelNeeded > this.FuelAmount)
        {
            return false;
        }
        this.FuelAmount = this.FuelAmount - fuelNeeded;
        this.Distance = this.Distance + distance;
        return true;
    }
}
