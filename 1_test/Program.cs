using System.Globalization;
using Microsoft.EntityFrameworkCore;
using _1_test.Data;
using _1_test.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Naloga: MVC aplikacija z DI, EF Core in PRG (navodilo: "Izdelajte spletno aplikacijo ...", "Code First").
builder.Services.AddControllersWithViews(options =>
{
    // Naloga: podpora za decimalno piko in vejico (navodilo: "V primeru tezav s tipom Double").
    options.ModelBinderProviders.Insert(0, new FlexibleDoubleModelBinderProvider());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".DSR.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var slCulture = new CultureInfo("sl-SI");
CultureInfo.DefaultThreadCurrentCulture = slCulture;
CultureInfo.DefaultThreadCurrentUICulture = slCulture;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

DbSeeder.Seed(app);


app.Run();
