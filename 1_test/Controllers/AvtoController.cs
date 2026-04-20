// Naloga 1: Stran za entiteto Avto
// Naloga 5: CRUD operacije za avte (dodaj, uredi, briši, prikaži)

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class AvtoController : Controller
    {
        private readonly AppDbContext _db;

        public AvtoController(AppDbContext db)
        {
            _db = db;
        }

        // Naloga 1+5: Prikaže vse avte
        public async Task<IActionResult> Index()
        {
            var avti = await _db.Avti.ToListAsync();
            return View(avti);
        }

        // Naloga 5: Prikaže podrobnosti enega avta
        public async Task<IActionResult> Details(int id)
        {
            var avto = await _db.Avti.FindAsync(id);
            if (avto == null) return NotFound();
            return View(avto);
        }

        // Naloga 4+5: Prikaže obrazec za dodajanje avta (EditorForModel)
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Avto());
        }

        // Naloga 4+5: Shrani nov avto v bazo (PRG vzorec)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Avto avto)
        {
            if (ModelState.IsValid)
            {
                _db.Avti.Add(avto);
                await _db.SaveChangesAsync();
                // PRG vzorec - preusmerimo po POST-u
                return RedirectToAction(nameof(Details), new { id = avto.Id });
            }
            return View(avto);
        }

        // Naloga 5: Prikaže obrazec za urejanje avta
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var avto = await _db.Avti.FindAsync(id);
            if (avto == null) return NotFound();
            return View(avto);
        }

        // Naloga 5: Shrani spremembe avta v bazo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Avto avto)
        {
            if (id != avto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _db.Avti.Update(avto);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avto);
        }

        // Naloga 5: Prikaže stran za potrditev brisanja
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var avto = await _db.Avti.FindAsync(id);
            if (avto == null) return NotFound();
            return View(avto);
        }

        // Naloga 5: Izbriše avto iz baze
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avto = await _db.Avti.FindAsync(id);
            if (avto != null)
            {
                _db.Avti.Remove(avto);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
