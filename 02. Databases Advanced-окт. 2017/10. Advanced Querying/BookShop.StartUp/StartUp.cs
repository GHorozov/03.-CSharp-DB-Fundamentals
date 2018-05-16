namespace BookShop
{
    using System;
    using System.Linq;
    using BookShop.Data;
    using BookShop.Models;
    using BookShop.Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new BookShopContext();

            //var command = Console.ReadLine();
            //var inputYear = int.Parse(Console.ReadLine());
            //var input = Console.ReadLine();
            //var date = Console.ReadLine();
            //var lengthCheck = int.Parse(Console.ReadLine());



            //Console.WriteLine(GetBooksByAgeRestriction(context, command));
            //Console.WriteLine(GetGoldenBooks(context));
            //Console.WriteLine(GetBooksByPrice(context));
            //Console.WriteLine(GetBooksNotRealeasedIn(context, inputYear));
            //Console.WriteLine(GetBooksByCategory(context, input));
            //Console.WriteLine(GetBooksReleasedBefore(context, date));
            //Console.WriteLine(GetAuthorNamesEndingIn(context, input));
            //Console.WriteLine(GetBookTitlesContaining(context, input));
            //Console.WriteLine(GetBooksByAuthor(context, input));
            //Console.WriteLine(CountBooks(context, lengthCheck));
            //Console.WriteLine(CountCopiesByAuthor(context));
            //Console.WriteLine(GetTotalProfitByCategory(context));
            //Console.WriteLine(GetMostRecentBooks(context));
            Console.WriteLine(RemoveBooks(context));

        }

        //1. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var enumValue = -1;

            if (command.ToLower() == "minor")
            {
                enumValue = 0;
            }
            else if (command.ToLower() == "teen")
            {
                enumValue = 1;
            }
            else if (command.ToLower() == "adult")
            {
                enumValue = 2;
            }

            var titles = context.Books
                .Where(b => (int)b.AgeRestriction == enumValue)
                .Select(t => t.Title)
                .OrderBy(t => t)
                .ToArray();

            var result = String.Join(Environment.NewLine, titles);

            return result;
        }

        //2. Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var GoldenBooks = context.Books
                .OrderBy(b => b.BookId)
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .Select(b => b.Title)
                .ToArray();

            var result = string.Join(Environment.NewLine, GoldenBooks);

            return result;
        }

        //3. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            var booksByPrice = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:f2}")
                .ToArray();

            var result = String.Join(Environment.NewLine, booksByPrice);

            return result;
        }

        //4. Not Released In
        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            var result = String.Join(Environment.NewLine, books);

            return result;
        }

        //5. Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var inputCategories = input.ToLower().Split(new[] { " ", Environment.NewLine, "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var booksByCategory = context.Books
                    .Where(b => b.BookCategories.Any(bc => inputCategories.Contains(bc.Category.Name.ToLower())))
                    .Select(b => b.Title)
                    .OrderBy(t => t)
                    .ToArray();

            string result = String.Join(Environment.NewLine, booksByCategory);

            return result;
        }

        //6. Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var inputDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var titles = context.Books
                .Where(b => b.ReleaseDate < inputDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}")
                .ToArray();

            var result = string.Join(Environment.NewLine, titles);

            return result;
        }

        //7. Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var names = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => $"{a.FirstName} {a.LastName}")
                .OrderBy(a => a)
                .ToArray();

            var result = string.Join(Environment.NewLine, names);

            return result;
        }

        //8. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var inputString = input.ToLower();

            var titles = context.Books
                .Where(b => b.Title.ToLower().Contains(inputString))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            var result = string.Join(Environment.NewLine, titles);

            return result;
        }

        //9. Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var titles = context.Books
                .Where(a => a.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToArray();

            var result = string.Join(Environment.NewLine, titles);

            return result;
        }

        //10. Count Books
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int count = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();

            return count;
        }

        //11. Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var totalBooks = context.Authors
                .Select(a => new
                {
                    AuthorName = $"{a.FirstName} {a.LastName}",
                    TotalBookCopies = a.Books.Select(bc => bc.Copies).Sum()
                })
                .OrderByDescending(a => a.TotalBookCopies)
                .Select(a => $"{a.AuthorName} - {a.TotalBookCopies}");

            var result = string.Join(Environment.NewLine, totalBooks);

            return result;
        }

        //12. Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categoryProfit = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    CategoryProfit = c.CategoryBooks.Select(cb => cb.Book.Price * cb.Book.Copies).Sum()
                })
                .OrderByDescending(c => c.CategoryProfit)
                .ThenBy(c => c.CategoryName)
                .Select(c => $"{c.CategoryName} ${c.CategoryProfit:f2}");

            var result = string.Join(Environment.NewLine, categoryProfit);

            return result;
        }

        //13. Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var books = context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    CategoryCount = c.CategoryBooks.Select(bc => bc.Book).Count(),
                    TopThreeBooks = string.Join(Environment.NewLine , 
                                    c.CategoryBooks.Select(bc => bc.Book)
                                     .OrderByDescending(cb => cb.ReleaseDate)
                                     .Take(3)
                                     .Select(x => $"{x.Title} ({x.ReleaseDate.Value.Year})"))
                })
                .OrderBy(c => c.Name)
                .Select(c => $"--{c.Name}{Environment.NewLine}{c.TopThreeBooks}")
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        //14. Increase Prices
        public static int IncreasePrices(BookShopContext context)
        {
            var increaseSum = 5;
            var limitYear = 2010;

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < limitYear)
                .ToArray();

            foreach (var book in books)
            {
                book.Price += increaseSum;
            }

            return context.SaveChanges();
        }

        //15. Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToArray();

            var removedBooks = books.Count();

            context.Books.RemoveRange(books);
            context.SaveChanges();

            return removedBooks;
        }
    }
}