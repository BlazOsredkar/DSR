// Naloga 1: Domača stran aplikacije
// Prikazuje seznam avtov (Naloga 5: iz baze, prej statično)

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;

namespace RentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        // Naloga 1: Prikaže domačo stran
        // Naloga 5: Avti se naložijo iz baze
        public async Task<IActionResult> Index()
        {
            // Naloga 1: Prikažemo vse avte na domači strani
            var avti = await _db.Avti.ToListAsync();
            return View(avti);
        }
    }
}
