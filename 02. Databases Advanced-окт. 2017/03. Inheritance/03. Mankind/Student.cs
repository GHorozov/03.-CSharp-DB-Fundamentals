using System;
using System.Linq;
using System.Text;

public class Student : Human
{
    private string facultyNumber;

    public Student(string firstName, string lastName, string facultyNumber)
        : base(firstName, lastName)
    {
        this.FacultyNumber = facultyNumber;
    }

    public string FacultyNumber
    {
        get
        {
            return this.facultyNumber;
        }

        set
        {
            if (value.Length < 5 || value.Length > 10)
            {
                throw new ArgumentException("Invalid faculty number!");
            }

            if (value.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                throw new ArgumentException("Invalid faculty number!");
            }

            this.facultyNumber = value;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"First Name: {this.FirstName}");
        sb.AppendLine($"Last Name: {this.LastName}");
        sb.AppendLine($"Faculty number: {this.FacultyNumber}");

        return sb.ToString().Trim();
    }
}

