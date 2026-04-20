// Naloga 4: Obrazec za dodajanje in ogled avta
// Naloga 4: Uporablja EditorForModel() za urejanje in DisplayForModel() za prikaz
// Naloga 4: Implementira PRG vzorec

using Microsoft.AspNetCore.Mvc;
using RentACar.Models;
using System.Text.Json;

namespace RentACar.Controllers
{
    public class AvtoObrazecController : Controller
    {
        private const string SessionKey = "AvtoData";

        private Avto NaloziIzSessiona()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            if (json == null) return new Avto();
            return JsonSerializer.Deserialize<Avto>(json) ?? new Avto();
        }

        // Naloga 4: GET - prikaže obrazec z EditorForModel()
        [HttpGet]
        public IActionResult Dodaj()
        {
            return View(NaloziIzSessiona());
        }

        // Naloga 4: POST - validacija + PRG preusmeritev na prikaz
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dodaj(Avto model)
        {
            if (!ModelState.IsValid) return View(model);

            // Shranimo v session in preusmerimo (PRG)
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(model));
            return RedirectToAction(nameof(Prikaz));
        }

        // Naloga 4: GET - prikaže podatke z DisplayForModel()
        [HttpGet]
        public IActionResult Prikaz()
        {
            var model = NaloziIzSessiona();
            return View(model);
        }
    }
}
