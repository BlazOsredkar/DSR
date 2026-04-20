using Microsoft.EntityFrameworkCore;
using _1_test.Models;

namespace _1_test.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Naloga: Code First entitete (navodilo: "Izberite si poljuben projekt ... vsaj 3 glavne entitete").
    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Order> Orders => Set<Order>();
}
