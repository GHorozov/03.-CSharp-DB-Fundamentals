using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class Person
{
    private string name;
    private double money;
    private List<Product> products;

    public Person(string name, double money)
    {
        this.Name = name;
        this.Money = money;
        this.Products = new List<Product>();
    }

    public string Name
    {
        get => name;

        set
        {
            if (value.Length <= 0)
            {
                throw new ArgumentException("Name cannot be empty");
            }

            this.name = value;
        }
    }

    public double Money
    {
        get => money;
        set
        {
            if(value < 0)
            {
                throw new ArgumentException("Money cannot be negative");
            }

            this.money = value;
        }
    }
    public List<Product> Products { get => products; set => products = value; }

    
    public string BuyProduct(Person person, Product product)
    {
        if(person.money >= product.Price)
        {
            this.products.Add(product);
            this.money -= product.Price;
            return $"{this.Name} bought {product.Name}";        
        }
        else 
        {
            return $"{this.name} can't afford {product.Name}";
        }
    }


    public string BoughtProducts()
    {
        var sb = new StringBuilder();

        sb.Append($"{this.Name} - ");

        foreach (var p in this.products)
        {
            sb.Append($"{p.Name}, ");
        }

        if(this.products.Count == 0)
        {
            sb.Append("Nothing bought");
        }

        return sb.ToString().Trim();
    }
}

