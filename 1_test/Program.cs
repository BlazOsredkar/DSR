// Naloga 1-5: Vstopna točka aplikacije
// Tukaj nastavimo vse storitve in middleware za ASP.NET Core MVC

using Microsoft.EntityFrameworkCore;
using RentACar.Data;

var builder = WebApplication.CreateBuilder(args);

// Dodamo MVC storitve (kontrolerji + pogledi)
builder.Services.AddControllersWithViews();

// Naloga 5: Dodamo Entity Framework Core z SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodamo session za PRG vzorec (Naloga 3 in 4)
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Napake v produkciji
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Za CSS, JS, slike
app.UseRouting();
app.UseSession(); // Vklopimo session
app.UseAuthorization();

// Privzeta pot: /Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
