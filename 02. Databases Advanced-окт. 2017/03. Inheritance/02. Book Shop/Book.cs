using System;
using System.Text;

public class Book
{
    private string title;
    private string author;
    private decimal price;

    public Book(string author, string title, decimal price)
    {
        this.Author = author;
        this.Title = title;
        this.Price = price;
    }

    public string Title
    {
        get
        {
            return this.title;
        }

        private set
        {
            if(value.Length < 3)
            {
                throw new ArgumentException("Title not valid!");
            }

            this.title = value;
        }
    }

    public string Author
    {
        get
        {
            return this.author;
        }

        private set
        {
            var lastIndexOfSpace = value.Trim().LastIndexOf(' ');
            if (lastIndexOfSpace > 0 && char.IsDigit(value.Substring(lastIndexOfSpace + 1, 1)[0]))
            {
                throw new ArgumentException("Author not valid!");
            }

            this.author = value;
        }
    }

    public virtual decimal Price
    {
        get
        {
            return this.price;
        }

        protected set
        {
            if(value <= 0)
            {
                throw new ArgumentException("Price not valid!");
            }

            this.price = value;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Type: {this.GetType().Name}")
            .AppendLine($"Title: {this.Title}")
            .AppendLine($"Author: {this.Author}")
            .AppendLine($"Price: {this.Price:F2}");

        return sb.ToString().TrimEnd();
    }

}

