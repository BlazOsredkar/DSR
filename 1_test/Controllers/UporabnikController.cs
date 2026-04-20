// Naloga 1: Stran za entiteto Uporabnik
// Naloga 5: CRUD operacije za uporabnike

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class UporabnikController : Controller
    {
        private readonly AppDbContext _db;

        public UporabnikController(AppDbContext db)
        {
            _db = db;
        }

        // Naloga 1+5: Seznam vseh uporabnikov
        public async Task<IActionResult> Index()
        {
            var uporabniki = await _db.Uporabniki.ToListAsync();
            return View(uporabniki);
        }

        // Naloga 5: Podrobnosti posameznega uporabnika
        public async Task<IActionResult> Details(int id)
        {
            var u = await _db.Uporabniki.FindAsync(id);
            if (u == null) return NotFound();
            return View(u);
        }

        // Naloga 5: Obrazec za dodajanje uporabnika
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Uporabnik());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Uporabnik u)
        {
            if (ModelState.IsValid)
            {
                _db.Uporabniki.Add(u);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = u.Id });
            }
            return View(u);
        }

        // Naloga 5: Urejanje uporabnika
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var u = await _db.Uporabniki.FindAsync(id);
            if (u == null) return NotFound();
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Uporabnik u)
        {
            if (id != u.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _db.Uporabniki.Update(u);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(u);
        }

        // Naloga 5: Brisanje uporabnika
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var u = await _db.Uporabniki.FindAsync(id);
            if (u == null) return NotFound();
            return View(u);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var u = await _db.Uporabniki.FindAsync(id);
            if (u != null)
            {
                _db.Uporabniki.Remove(u);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
