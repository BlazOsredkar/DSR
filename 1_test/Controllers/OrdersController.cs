using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _1_test.Data;
using _1_test.Infrastructure;

namespace _1_test.Controllers;

[Authorize(Roles = AppRoles.Admin)]
public class OrdersController : Controller
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    // Naloga: tretja entiteta za obdelavo podatkov (navodilo: "nakup izdelka, rezervacija, najem ...").
    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.Book)
            .ToListAsync();

        return View(orders);
    }
}
