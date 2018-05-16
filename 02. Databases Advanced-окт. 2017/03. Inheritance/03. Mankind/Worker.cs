using System;
using System.Text;

public class Worker : Human
{
    private decimal weekSalary;
    private double workHoursPerDay;

    public Worker(string firstName, string lastName, decimal weekSalary, double workHoursPerDay)
        : base(firstName, lastName)
    {
        this.WeekSalary = weekSalary;
        this.WorkHoursPerDay = workHoursPerDay;
    }

    public decimal WeekSalary
    {
        get => weekSalary;

        set
        {
            if (value <= 10)
            {
                throw new ArgumentException("Expected value mismatch!Argument: weekSalary");
            }

            this.weekSalary = value;
        }
    }

    public double WorkHoursPerDay
    {
        get => this.workHoursPerDay;

        set
        {
            if (value < 1 || value > 12)
            {
                throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
            }

            this.workHoursPerDay = value;
        }
    }

    public decimal salaryPerHour => (this.WeekSalary / 5) / (decimal)this.workHoursPerDay;

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"First Name: {this.FirstName}");
        sb.AppendLine($"Last Name: {this.LastName}");
        sb.AppendLine($"Week Salary: {this.WeekSalary:f2}");
        sb.AppendLine($"Hours per day: {this.workHoursPerDay:f2}");
        sb.AppendLine($"Salary per hour: {this.salaryPerHour:f2}");

        return sb.ToString().Trim();
    }
}

