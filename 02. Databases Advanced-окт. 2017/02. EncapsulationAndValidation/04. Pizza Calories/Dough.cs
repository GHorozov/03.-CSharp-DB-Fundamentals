using System;

public class Dough
{
    private const double white = 1.5;
    private const double wholegrain = 1.0;
    private const double crispy = 0.9;
    private const double chewy = 1.1;
    private const double homemade = 1.0;


    private string flourType;
    private string bakingTechnique;
    private double weight;

    public Dough(string flourType, string bakingTechique, double weight)
    {
        this.FlourType = flourType;
        this.BakingTechnique = bakingTechique;
        this.Weight = weight;
    }

    private string FlourType
    {
        set
        {
            if (value.ToLower() != "white" && value.ToLower() != "wholegrain")
            {
                throw new ArgumentException("Invalid type of dough.");
            }

            this.flourType = value;
        }
    }

    private string BakingTechnique
    {
        set
        {
            if (value.ToLower() != "crispy" && value.ToLower() != "chewy" && value.ToLower() != "homemade")
            {
                throw new ArgumentException("Invalid type of dough.");
            }

            this.bakingTechnique = value;
        }
    }

    private double Weight
    {
        set
        {
            if (value < 1 || value > 200)
            {
                throw new ArgumentException("Dough weight should be in the range [1..200].");
            }

            this.weight = value;
        }
    }

    public double GetCaloriesPerDough()
    {
        var result = 0.0;

        if (this.flourType.ToLower() == "white")
        {
            if (this.bakingTechnique.ToLower() == "crispy")
            {
                result = ((2 * this.weight) * white * crispy);
            }
            else if (this.bakingTechnique.ToLower() == "chewy")
            {
                result = ((2 * this.weight) * white * chewy);
            }
            else if (this.bakingTechnique.ToLower() == "homemade")
            {
                result = ((2 * this.weight) * white * homemade);
            }
        }
        else if (this.flourType.ToLower() == "wholegrain")
        {
            if (this.bakingTechnique.ToLower() == "crispy")
            {
                result = ((2 * this.weight) * wholegrain * crispy);
            }
            else if (this.bakingTechnique.ToLower() == "chewy")
            {
                result = ((2 * this.weight) * wholegrain * chewy);
            }
            else if (this.bakingTechnique.ToLower() == "homemade")
            {
                result = ((2 * this.weight) * wholegrain * homemade);
            }
        }

        return result;
    }
}

