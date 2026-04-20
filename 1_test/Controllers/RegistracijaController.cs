// Naloga 3: 4-koračni obrazec za registracijo uporabnika
// Naloga 3+4: Implementira PRG (Post-Redirect-Get) vzorec
// Naloga 4: Strogo tipiziran obrazec z validacijo (Data Annotations)
// PRG vir: https://dotnettutorials.net/lesson/post-redirect-get-prg-pattern-example-in-asp-net-core/

using Microsoft.AspNetCore.Mvc;
using RentACar.Models;
using System.Text.Json;

namespace RentACar.Controllers
{
    public class RegistracijaController : Controller
    {
        private const string SessionKey = "RegistracijaData";

        // Pomočna metoda - naloži celoten model iz sessiona
        private RegistracijaSessionModel NaloziIzSessiona()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            if (json == null) return new RegistracijaSessionModel();
            return JsonSerializer.Deserialize<RegistracijaSessionModel>(json) ?? new RegistracijaSessionModel();
        }

        // Pomočna metoda - shrani v session
        private void ShraniVSession(RegistracijaSessionModel model)
        {
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(model));
        }

        // Naloga 4: Seznam krajev za padajoči meni (vsaj 5 krajev)
        private static readonly List<string> Kraji = new()
        {
            "Ljubljana", "Maribor", "Celje", "Kranj", "Koper",
            "Novo Mesto", "Velenje", "Ptuj", "Murska Sobota", "Slovenj Gradec"
        };

        // ==================== KORAK 1 ====================
        // Naloga 3+4: Korak 1 - ime, priimek, datum rojstva, kraj, EMŠO, starost

        [HttpGet]
        public IActionResult Korak1()
        {
            ViewBag.Kraji = Kraji; // Za padajoči meni
            return View(new Korak1Model());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Korak1(Korak1Model model)
        {
            ViewBag.Kraji = Kraji;
            if (!ModelState.IsValid) return View(model);

            // Shranimo podatke tega koraka v session
            var session = NaloziIzSessiona();
            session.Ime = model.Ime;
            session.Priimek = model.Priimek;
            session.DatumRojstva = model.DatumRojstva;
            session.KrajRojstva = model.KrajRojstva;
            session.Emso = model.Emso;
            session.Starost = model.Starost;
            ShraniVSession(session);

            // PRG: preusmerimo na GET zahtevo naslednjega koraka
            return RedirectToAction(nameof(Korak2));
        }

        // ==================== KORAK 2 ====================
        // Naloga 3+4: Korak 2 - naslov, poštna številka, pošta, država

        [HttpGet]
        public IActionResult Korak2()
        {
            return View(new Korak2Model());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Korak2(Korak2Model model)
        {
            if (!ModelState.IsValid) return View(model);

            var session = NaloziIzSessiona();
            session.Naslov = model.Naslov;
            session.PostnaStevilka = model.PostnaStevilka;
            session.Posta = model.Posta;
            session.Drzava = model.Drzava;
            ShraniVSession(session);

            return RedirectToAction(nameof(Korak3));
        }

        // ==================== KORAK 3 ====================
        // Naloga 3+4: Korak 3 - e-naslov in geslo

        [HttpGet]
        public IActionResult Korak3()
        {
            return View(new Korak3Model());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Korak3(Korak3Model model)
        {
            if (!ModelState.IsValid) return View(model);

            var session = NaloziIzSessiona();
            session.ENaslov = model.ENaslov;
            session.Geslo = model.Geslo;
            ShraniVSession(session);

            return RedirectToAction(nameof(Korak4));
        }

        // ==================== KORAK 4 ====================
        // Naloga 3: Korak 4 - izpis vseh vpisanih podatkov

        [HttpGet]
        public IActionResult Korak4()
        {
            var session = NaloziIzSessiona();
            return View(session);
        }
    }
}
