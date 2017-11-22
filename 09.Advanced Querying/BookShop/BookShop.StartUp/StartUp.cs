namespace BookShop
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using BookShop.Data;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            //For 01.
            //string command = Console.ReadLine();

            //For 04.
            //int year = int.Parse(Console.ReadLine());

            //For 05.
            //string input = Console.ReadLine().ToLower();

            //For 06.
            //string date = Console.ReadLine();

            //For GetAuthorNameEndingIn(07.), GetBookTitlesContaining(08.), GetBooksByAuthor(09.)
            //string letters = Console.ReadLine();

            //For 10.
            //int requiredLength = int.Parse(Console.ReadLine());
            
            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);

                //Console.WriteLine(GetBooksByAgeRestriction(db, command)); 

                //Console.WriteLine(GetGoldenBooks(db));

                //Console.WriteLine(GetBooksByPrice(db));

                //Console.WriteLine(GetBooksNotRealeasedIn(db,year));

                //Console.WriteLine(GetBooksByCategory(db,input));

                //Console.WriteLine(GetBooksReleasedBefore(db,date));

                //Console.WriteLine(GetAuthorNamesEndingIn(db,letters));

                //Console.WriteLine(GetBookTitlesContaining(db,letters));

                //Console.WriteLine(GetBooksByAuthor(db,letters));

                //Console.WriteLine(CountBooks(db,requiredLength));

                //Console.WriteLine(CountCopiesByAuthor(db));

                //Console.WriteLine(GetTotalProfitByCategory(db));

                //Console.WriteLine(GetMostRecentBooks(db)); 

                //IncreasePrices(db);

                //Console.WriteLine($"{RemoveBooks(db)} books were deleted");
            }
    }
        //15.
        public static int RemoveBooks(BookShopContext db)
        {
            var books = db.Books
                .Where(b => b.Copies < 4200)
                .ToArray();
            db.Books.RemoveRange(books);
            db.SaveChanges();
            return books.Count();
        }

        //14.
        public static void IncreasePrices(BookShopContext db)
        {
            var books = db.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToArray();
            foreach (var book in books)
            {
                book.Price = book.Price + 5;
            }
            db.SaveChanges();
        }

        //13.
        public static string GetMostRecentBooks(BookShopContext db)
        {
            var booksCategories = db.Categories  
                
                .Select(c => new
                {
                   c.Name,
                   categoryBookCount = c.CategoryBooks.Count(),
                   CategoryBooks = c.CategoryBooks
                                    .OrderByDescending(cb=>cb.Book.ReleaseDate.Value)
                                    .Take(3)
                                    .Select(b=> new { b.Book.Title, b.Book.ReleaseDate.Value.Year})
                                    .OrderByDescending(y => y.Year)
                })
                .OrderBy(c => c.Name)
                .ToArray();

            var result = string.Join(Environment.NewLine, booksCategories
                .Select(x => $"--{x.Name}" 
                    + Environment.NewLine 
                    + string.Join(Environment.NewLine, x.CategoryBooks.Select(cb => $"{cb.Title} ({cb.Year})"))));

            return result;
        }

        //12.
        public static string GetTotalProfitByCategory(BookShopContext db)
        {
            var categories = db.Categories
                .Select(c => new
                {
                    c.Name,
                    Sum = c.CategoryBooks.Sum(a => a.Book.Price * a.Book.Copies)
                })
                .OrderByDescending(s => s.Sum)
                .ThenBy(n => n.Name)
                .ToArray();
            var result = string.Join(Environment.NewLine, categories.Select(a => $"{a.Name} ${a.Sum:f2}"));
            return result;
        }

        //11.
        public static string CountCopiesByAuthor(BookShopContext db)
        {
            var autorCopies = db.Books
                .Select(b => new
                {
                    AuthorName = b.Author.FirstName + " " + b.Author.LastName,
                    b.Copies
                })
                .GroupBy(b => b.AuthorName)
                .OrderByDescending(a => a.Sum(x => x.Copies))
                .ToArray();

            var result = string.Join(Environment.NewLine, autorCopies.Select(x=>x.Key + " - " + x.Sum(y=>y.Copies)));
            return result;
        }

        //10.
        public static int CountBooks(BookShopContext db, int lengthCheck)
        {
            var booksCount = db.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();
            return booksCount;
        }

        //09.
        public static string GetBooksByAuthor(BookShopContext db, string letters)
        {
            var pattern = $@"\b{letters.ToLower()}\w+";
            Regex regex = new Regex(pattern);
            var books = db.Books
                .Where(a => regex.Match(a.Author.LastName.ToLower()).Success)
                .OrderBy(b=>b.BookId)
                .Select(b=> $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToArray();
            var result = string.Join(Environment.NewLine, books);
            return result;
        }

        //08.
        public static string GetBookTitlesContaining(BookShopContext db, string letters)
        {
            var pattern = $@"\w*{letters.ToLower()}\w*";
            Regex regex = new Regex(pattern);
            var titles = db.Books
                .Where(t => regex.Match(t.Title.ToLower()).Success)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();
            var result = string.Join(Environment.NewLine, titles);
            return result;
        }

        //07.
        public static string GetAuthorNamesEndingIn(BookShopContext db, string letters)
        {
            var pattern = $@"\b\w+{letters}\b";
            Regex regex = new Regex(pattern);
            var authors = db.Authors
                .Where(a => regex.Match(a.FirstName).Success)
                .Select(a => $"{a.FirstName} {a.LastName}")
                .OrderBy(a => a)
                .ToArray();
            var result = string.Join(Environment.NewLine, authors);
            return result;
        }

        //06.
        public static string GetBooksReleasedBefore(BookShopContext db, string date)
        {
            var givenDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var books = db.Books
                .Where(b => b.ReleaseDate < givenDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price
                }).ToArray();

            var allBooks = books.Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}");
            string result = string.Join(Environment.NewLine, allBooks);
            return result;

        }

        //05.
        public static string GetBooksByCategory(BookShopContext db, string input)
        {
            string[] categories = input.Split(new[] { "\t", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
           
            var titles = db.Books
                .Where(b => b.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();
            string result = string.Join(Environment.NewLine, titles);

            return result;
        }

        //04.
        public static string GetBooksNotRealeasedIn(BookShopContext db, int year)
        {
            var books = db.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b=>b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        //03.
        public static string GetBooksByPrice(BookShopContext db)
        {
            var books = db.Books
                .Where(b => b.Price > 40)
                .Select(b=> new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b=>b.Price)
                .ToArray();
            var result = books.Select(t => $"{t.Title} - ${t.Price:f2}");
            return string.Join(Environment.NewLine, result);
        }

        //02.
        public static string GetGoldenBooks(BookShopContext db)
        {
            var goldenTitles = db.Books
                .OrderBy(b=>b.BookId)
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .Select(t => t.Title)
                .ToArray();
            return string.Join(Environment.NewLine, goldenTitles);
        }

        //01.
        public static string GetBooksByAgeRestriction(BookShopContext db, string command)
        {
            //var books = db.Books
            //    .OrderBy(b => b.Title)
            //    .Where(b => String.Equals(command, b.AgeRestriction.ToString(), StringComparison.InvariantCultureIgnoreCase))
            //    .Select(b => b.Title)
            //    .ToList();
            //var titlesResult = string.Join(Environment.NewLine, books);
            //return titlesResult;

            var enumValue = -1;
            switch (command.ToLower())
            {
                case "minor":
                    enumValue = 0;
                    break;
                case "teen":
                    enumValue = 1;
                    break;
                case "adult":
                    enumValue = 2;
                    break;
            }
            var books = db.Books
                .OrderBy(b => b.Title)
                .Where(b => b.AgeRestriction == (AgeRestriction)enumValue)
                .Select(b => b.Title)
                .ToList();
            var titlesResult = string.Join(Environment.NewLine, books);
            return titlesResult;
        }
    }
}
