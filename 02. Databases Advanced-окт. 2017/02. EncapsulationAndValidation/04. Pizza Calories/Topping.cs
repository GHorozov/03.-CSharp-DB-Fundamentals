using System;

public class Topping
{
    private const double meat = 1.2;
    private const double veggies = 0.8;
    private const double cheese = 1.1;
    private const double sauce = 0.9;

    private string toppingType;
    private double weight;

    public Topping(string toppingType, double weight)
    {
        this.ToppingType = toppingType;
        this.Weight = weight;
    }


    private string ToppingType
    {
        get => toppingType;

        set
        {
            if(value.ToLower() != "meat" && value.ToLower() != "veggies" && value.ToLower() != "cheese" && value.ToLower() != "sauce")
            {
                throw new ArgumentException($"Cannot place {value} on top of your pizza.");
            }

            this.toppingType = value;
        }
    }
    private double Weight
    {
        get => weight;

        set
        {
            if(value < 1 || value > 50)
            {
                throw new ArgumentException($"{this.ToppingType} weight should be in the range[1..50].");
            }

            this.weight = value;
        }
    }

    public double GetToppingCalories()
    {
        var result = 0.0;

        if(this.toppingType.ToLower() == "meat")
        {
            result = (2 * this.weight) * meat;
        }
        else if (this.toppingType.ToLower() == "veggies")
        {
            result = (2 * this.weight) * veggies;
        }
        else if (this.toppingType.ToLower() == "cheese")
        {
            result = (2 * this.weight) * cheese;
        }
        else if (this.toppingType.ToLower() == "sauce")
        {
            result = (2 * this.weight) * sauce;
        }

        return result;
    }
}

