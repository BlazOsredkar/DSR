# Vaja 8 — Testiranje krmilnika in poslovne logike (Unit testi)

Cilj vaje je napisati unit teste za MVC kontrolerje in Web API logiko.

## 1) Kaj testiramo

- **MVC kontroler**: vrajanje pogledov (View/Redirect) in pravilni modeli.
- **Web API**: pravilni HTTP rezultati in poslovna pravila.

Testi so v projektu: [1_test/1_test.Tests](1_test/1_test.Tests)

## 2) Priprava testnega projekta

Testni projekt je ustvarjen z xUnit in ima dodane pakete:

```bash
cd 1_test

dotnet new xunit -o 1_test.Tests

dotnet add 1_test.Tests/1_test.Tests.csproj reference 1_test.csproj

dotnet add 1_test.Tests/1_test.Tests.csproj package Microsoft.EntityFrameworkCore.InMemory

dotnet add 1_test.Tests/1_test.Tests.csproj package Moq
```

## 3) Testiranje BooksController

Datoteka: [1_test/1_test.Tests/Controllers/BooksControllerTests.cs](1_test/1_test.Tests/Controllers/BooksControllerTests.cs)

Primer testa:

```csharp
[Fact]
public async Task Create_POST_VeljavniModel_Preusmeri()
{
    var context = GetInMemoryContext("Create_POST");
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
```

Kaj preverjamo:

- ali se vrne `RedirectToActionResult`
- ali je akcija `Index`
- ali se zapis res shrani v bazo

## 4) Testiranje Web API (PurchasesApiController)

Datoteka: [1_test/1_test.Tests/Controllers/PurchasesApiControllerTests.cs](1_test/1_test.Tests/Controllers/PurchasesApiControllerTests.cs)

Tu moramo simulirati prijavljenega uporabnika. Za to uporabimo `UserManager` + mock:

```csharp
var store = new Mock<IUserStore<ApplicationUser>>();
var userManager = new Mock<UserManager<ApplicationUser>>(
    store.Object, null, null, null, null, null, null, null, null);

userManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(userId);
```

Zakaj to potrebujemo?

- API bere `UserId` iz `ClaimsPrincipal`.
- brez tega metoda `GetMyPurchases()` ne bi vedela, kdo je prijavljen.

Primer testa:

```csharp
[Fact]
public async Task CreatePurchase_VeljavniDto_Vrne201()
{
    var context = GetInMemoryContext("CreatePurchase");
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
}
```

## 5) Zagon testov

```bash
cd 1_test

dotnet test 1_test.Tests
```

Pricakovan izpis:

```
Passed!  - Failed: 0, Passed: X, Skipped: 0
```

## 6) AAA vzorec (Arrange, Act, Assert)

- **Arrange**: pripravi podatke
- **Act**: poklici metodo
- **Assert**: preveri rezultat

To je najbolj berljiv nacin pisanja testov.

## 7) Pogoste napake

- Pozabljen `SaveChangesAsync()` pred testom.
- Manjkajo `Required` polja v testnih modelih.
- Pozabljen `ClaimsPrincipal` pri API testih.
