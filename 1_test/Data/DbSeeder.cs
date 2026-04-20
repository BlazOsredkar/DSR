using Microsoft.EntityFrameworkCore;
using _1_test.Models;

namespace _1_test.Data;

public static class DbSeeder
{
    public static void Seed(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Naloga: Code First baza (navodilo: "Za shranjevanje podatkov ... postavite podatkovno bazo").
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User
                {
                    FirstName = "Ana",
                    LastName = "Kralj",
                    BirthDate = new DateTime(2000, 5, 12),
                    Age = 24,
                    Emso = "1205000500008",
                    BirthPlace = "Ljubljana",
                    Address = "Slovenska cesta 1",
                    PostalCode = 1000,
                    City = "Ljubljana",
                    Country = "Slovenija",
                    Email = "ana.kralj@example.com"
                },
                new User
                {
                    FirstName = "Miha",
                    LastName = "Novak",
                    BirthDate = new DateTime(1998, 11, 3),
                    Age = 26,
                    Emso = "0311998500007",
                    BirthPlace = "Maribor",
                    Address = "Gosposka ulica 5",
                    PostalCode = 2000,
                    City = "Maribor",
                    Country = "Slovenija",
                    Email = "miha.novak@example.com"
                }
            );
        }

        if (!context.Books.Any())
        {
            context.Books.AddRange(
                new Book
                {
                    Title = "Osnove C#",
                    Author = "Tina Vidmar",
                    Price = 29.90,
                    PageCount = 280,
                    PublishDate = new DateTime(2023, 9, 1),
                    Stock = 12
                },
                new Book
                {
                    Title = "MVC v praksi",
                    Author = "Gregor Zupan",
                    Price = 34.50,
                    PageCount = 320,
                    PublishDate = new DateTime(2024, 2, 15),
                    Stock = 8
                }
            );
        }

        context.SaveChanges();

        if (!context.Orders.Any())
        {
            var user = context.Users.First();
            var book = context.Books.First();

            context.Orders.AddRange(
                new Order
                {
                    UserId = user.Id,
                    BookId = book.Id,
                    OrderDate = DateTime.Today,
                    Quantity = 1,
                    Total = book.Price,
                    Status = "Novo"
                }
            );
        }

        context.SaveChanges();
    }
}
