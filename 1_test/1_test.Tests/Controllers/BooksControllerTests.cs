using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using _1_test.Controllers;
using _1_test.Data;
using _1_test.Models;

namespace _1_test.Tests.Controllers;

public class BooksControllerTests
{
    private static AppDbContext GetInMemoryContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task Index_VrneViewZSeznamomKnjig()
    {
        var context = GetInMemoryContext(nameof(Index_VrneViewZSeznamomKnjig));
        context.Books.AddRange(
            new Book { Id = 1, Title = "Hamlet", Author = "Shakespeare", Price = 9.99, PageCount = 250, Stock = 3 },
            new Book { Id = 2, Title = "Macbeth", Author = "Shakespeare", Price = 8.99, PageCount = 220, Stock = 4 }
        );
        await context.SaveChangesAsync();

        var controller = new BooksController(context);

        var result = await controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public void Create_GET_VrneView()
    {
        var context = GetInMemoryContext(nameof(Create_GET_VrneView));
        var controller = new BooksController(context);

        var result = controller.Create();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Create_POST_VeljavniModel_Preusmeri()
    {
        var context = GetInMemoryContext(nameof(Create_POST_VeljavniModel_Preusmeri));
        var controller = new BooksController(context);

        var book = new Book
        {
            Title = "Nova knjiga",
            Author = "Test",
            Price = 12.5,
            PageCount = 120,
            Stock = 5
        };

        var result = await controller.Create(book);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Single(context.Books);
    }

    [Fact]
    public async Task Create_POST_NeveljavniModel_VrneView()
    {
        var context = GetInMemoryContext(nameof(Create_POST_NeveljavniModel_VrneView));
        var controller = new BooksController(context);
        controller.ModelState.AddModelError("Title", "Naziv je obvezen.");

        var book = new Book();

        var result = await controller.Create(book);

        Assert.IsType<ViewResult>(result);
        Assert.False(controller.ModelState.IsValid);
    }
}
