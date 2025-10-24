using Microsoft.EntityFrameworkCore;
using Task4.DataAccess.Data;
using Task4.Domain.Entities;

namespace Task4.DataAccess.Helpers;

public static class SeedDatabase
{
    public static async Task SeedAsync(Task4Context context)
    {
        if (!await context.Authors.AnyAsync())
        {
            var authors = new List<Author>
                    {
                        new() { Name = "J.R.R. Tolkien", DateOfBirth = new DateTime(1892, 1, 3) },
                        new() { Name = "George Orwell", DateOfBirth = new DateTime(1903, 6, 25) },
                        new() { Name = "Jane Austen", DateOfBirth = new DateTime(1775, 12, 16) },
                        new() { Name = "F. Scott Fitzgerald", DateOfBirth = new DateTime(1896, 9, 24) },
                        new() { Name = "Stephen King", DateOfBirth = new DateTime(1947, 9, 21) }
                    };
            await context.Authors.AddRangeAsync(authors);
            await context.SaveChangesAsync();
        }

        if (!await context.Books.AnyAsync())
        {
            var tolkien = await context.Authors.FirstAsync(a => a.Name == "J.R.R. Tolkien");
            var orwell = await context.Authors.FirstAsync(a => a.Name == "George Orwell");
            var austen = await context.Authors.FirstAsync(a => a.Name == "Jane Austen");
            var fitzgerald = await context.Authors.FirstAsync(a => a.Name == "F. Scott Fitzgerald");
            var king = await context.Authors.FirstAsync(a => a.Name == "Stephen King");

            var books = new List<Book>
                    {
                        new() { Title = "The Hobbit", PublishedYear = 1937, AuthorId = tolkien.Id },
                        new() { Title = "The Lord of the Rings", PublishedYear = 1954, AuthorId = tolkien.Id },
                        new() { Title = "1984", PublishedYear = 1949, AuthorId = orwell.Id },
                        new() { Title = "Animal Farm", PublishedYear = 1945, AuthorId = orwell.Id },
                        new() { Title = "Pride and Prejudice", PublishedYear = 1813, AuthorId = austen.Id },
                        new() { Title = "Sense and Sensibility", PublishedYear = 1811, AuthorId = austen.Id },
                        new() { Title = "The Great Gatsby", PublishedYear = 1925, AuthorId = fitzgerald.Id },
                        new() { Title = "It", PublishedYear = 1986, AuthorId = king.Id },
                        new() { Title = "The Shining", PublishedYear = 1977, AuthorId = king.Id },
                        new() { Title = "The Stand", PublishedYear = 1978, AuthorId = king.Id }
                    };

            await context.Books.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }
    }
}
