// Naloga 1: Stran za entiteto Rezervacija
// Naloga 5: CRUD operacije za rezervacije

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class RezervacijaController : Controller
    {
        private readonly AppDbContext _db;

        public RezervacijaController(AppDbContext db)
        {
            _db = db;
        }

        // Naloga 1+5: Seznam vseh rezervacij
        public async Task<IActionResult> Index()
        {
            // Include() naloži tudi podatke o avtu in uporabniku
            var rezervacije = await _db.Rezervacije
                .Include(r => r.Avto)
                .Include(r => r.Uporabnik)
                .ToListAsync();
            return View(rezervacije);
        }

        // Naloga 5: Podrobnosti posamezne rezervacije
        public async Task<IActionResult> Details(int id)
        {
            var r = await _db.Rezervacije
                .Include(r => r.Avto)
                .Include(r => r.Uporabnik)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (r == null) return NotFound();
            return View(r);
        }

        // Naloga 5: Dodajanje rezervacije
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Posredujemo seznam avtov in uporabnikov za spustna menija
            ViewBag.Avti = await _db.Avti.ToListAsync();
            ViewBag.Uporabniki = await _db.Uporabniki.ToListAsync();
            return View(new Rezervacija());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rezervacija r)
        {
            if (ModelState.IsValid)
            {
                // Izračunamo število dni in skupno ceno
                r.SteviloDni = (int)(r.DatumDo - r.DatumOd).TotalDays;
                var avto = await _db.Avti.FindAsync(r.AvtoId);
                r.SkupnaCena = r.SteviloDni * (avto?.CenaNaDan ?? 0);

                _db.Rezervacije.Add(r);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = r.Id });
            }
            ViewBag.Avti = await _db.Avti.ToListAsync();
            ViewBag.Uporabniki = await _db.Uporabniki.ToListAsync();
            return View(r);
        }

        // Naloga 5: Brisanje rezervacije
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var r = await _db.Rezervacije
                .Include(r => r.Avto)
                .Include(r => r.Uporabnik)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (r == null) return NotFound();
            return View(r);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var r = await _db.Rezervacije.FindAsync(id);
            if (r != null)
            {
                _db.Rezervacije.Remove(r);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
