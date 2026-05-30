using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;
using _1_test.Controllers;
using _1_test.Data;
using _1_test.Models;
using _1_test.Models.Dto;

namespace _1_test.Tests.Controllers;

public class PurchasesApiControllerTests
{
    private static AppDbContext GetInMemoryContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        return new AppDbContext(options);
    }

    private static PurchasesApiController CreateController(AppDbContext context, string userId)
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        var userManager = new Mock<UserManager<ApplicationUser>>(
            store.Object, null, null, null, null, null, null, null, null);

        userManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);

        var controller = new PurchasesApiController(context, userManager.Object);

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, "testuser")
        }, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };

        return controller;
    }

    [Fact]
    public async Task GetMyPurchases_VrneSeznamNakupov()
    {
        var context = GetInMemoryContext(nameof(GetMyPurchases_VrneSeznamNakupov));
        var userId = "user-123";

        var book = new Book { Id = 1, Title = "Test", Author = "A", Price = 10, PageCount = 100, Stock = 10 };
        context.Books.Add(book);
        context.Purchases.Add(new Purchase
        {
            UserId = userId,
            DeliveryAddress = "Testna 1",
            ContactPhone = "041000000",
            TotalPrice = 10,
            Items = new List<PurchaseItem>
            {
                new PurchaseItem { BookId = 1, Quantity = 1, UnitPrice = 10 }
            }
        });
        context.Purchases.Add(new Purchase
        {
            UserId = "other",
            DeliveryAddress = "Druga 2",
            ContactPhone = "040000000",
            TotalPrice = 20,
            Items = new List<PurchaseItem>
            {
                new PurchaseItem { BookId = 1, Quantity = 2, UnitPrice = 10 }
            }
        });
        await context.SaveChangesAsync();

        var controller = CreateController(context, userId);

        var result = await controller.GetMyPurchases();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var list = Assert.IsAssignableFrom<IEnumerable<PurchaseReadDto>>(okResult.Value);
        Assert.Single(list);
    }

    [Fact]
    public async Task CreatePurchase_VeljavniDto_Vrne201()
    {
        var context = GetInMemoryContext(nameof(CreatePurchase_VeljavniDto_Vrne201));
        var userId = "user-456";

        context.Books.Add(new Book { Id = 1, Title = "Nova knjiga", Author = "Test", Price = 15, PageCount = 150, Stock = 5 });
        await context.SaveChangesAsync();

        var controller = CreateController(context, userId);

        var dto = new PurchaseCreateDto
        {
            DeliveryAddress = "Ljubljana 1",
            ContactPhone = "041111111",
            Items = new List<PurchaseItemDto>
            {
                new PurchaseItemDto { BookId = 1, Quantity = 2 }
            }
        };

        var result = await controller.CreatePurchase(dto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        Assert.Single(context.Purchases);
    }
}
