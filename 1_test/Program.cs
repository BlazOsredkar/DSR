using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using _1_test.Data;
using _1_test.Infrastructure;
using _1_test.Models;

var builder = WebApplication.CreateBuilder(args);

// Naloga: MVC aplikacija z DI, EF Core in PRG (navodilo: "Izdelajte spletno aplikacijo ...", "Code First").
builder.Services.AddControllersWithViews(options =>
{
    // Naloga: datum mora biti dd.MM.yyyy (navodilo: "Datum mora biti v pravilnem formatu").
    options.ModelBinderProviders.Insert(0, new StrictDateModelBinderProvider());
    // Naloga: podpora za decimalno piko in vejico (navodilo: "V primeru tezav s tipom Double").
    options.ModelBinderProviders.Insert(1, new FlexibleDoubleModelBinderProvider());
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
if (!string.IsNullOrWhiteSpace(googleClientId) && !string.IsNullOrWhiteSpace(googleClientSecret))
{
    builder.Services.AddAuthentication()
        .AddGoogle(options =>
        {
            options.ClientId = googleClientId;
            options.ClientSecret = googleClientSecret;
        });
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".DSR.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddHttpClient();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

DbSeeder.Seed(app);
await IdentitySeeder.SeedAsync(app.Services);


app.Run();
