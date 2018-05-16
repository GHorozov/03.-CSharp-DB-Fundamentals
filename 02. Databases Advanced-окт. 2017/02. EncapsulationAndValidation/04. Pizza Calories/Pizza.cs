using System;
using System.Collections.Generic;
using System.Linq;

public class Pizza
{
    private string name;
    private Dough dough;
    private List<Topping> toppings;

    public Pizza(string name)
    {
        this.Name = name;
        this.Dough = dough;
        this.toppings = new List<Topping>();
    }

    public string Name
    {
        get => this.name;

        private set
        {
            if(string.IsNullOrEmpty(value) || value.Length > 15)
            {
                throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
            }

            this.name = value;
        }
    }
    public Dough Dough { get => this.dough; set => this.dough = value; }

    public int ToppingsCount => this.toppings.Count;

    public void AddToppings(Topping topping)
    {
        if (this.toppings.Count >= 10)
        {
            throw new ArgumentException("Number of toppings should be in range [0..10].");
        }

        this.toppings.Add(topping);
    }

    public double TotalCaloriesInPizza()
    {
        var caloriesInDough = this.dough.GetCaloriesPerDough();
        var caloriesInToppings = this.toppings.Sum(x => x.GetToppingCalories());

        return (caloriesInDough + caloriesInToppings);
    }
}

