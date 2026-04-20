// Naloga 3: 4-koračni obrazec za registracijo uporabnika
// Naloga 3+4: Implementira PRG (Post-Redirect-Get) vzorec
// PRG vzorec: po POST zahtevi preusmerimo na GET, da preprečimo dvojno pošiljanje

using Microsoft.AspNetCore.Mvc;
using RentACar.Models;
using System.Text.Json;

namespace RentACar.Controllers
{
    public class RegistracijaController : Controller
    {
        // Ključ za shranjevanje podatkov v session
        private const string SessionKey = "RegistracijaData";

        // Pomočna metoda - naloži podatke iz sessiona
        private UporabnikZGesli NaloziIzSessiona()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            if (json == null) return new UporabnikZGesli();
            return JsonSerializer.Deserialize<UporabnikZGesli>(json) ?? new UporabnikZGesli();
        }

        // Pomočna metoda - shrani podatke v session
        private void ShraniVSession(UporabnikZGesli u)
        {
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(u));
        }

        // ==================== KORAK 1 ====================
        // Naloga 3: Korak 1 - Ime, priimek, rojstni datum, EMŠO, starost

        [HttpGet]
        public IActionResult Korak1()
        {
            var model = NaloziIzSessiona();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Korak1(UporabnikZGesli model)
        {
            // Preverimo samo polja tega koraka
            ModelState.Remove(nameof(model.Naslov));
            ModelState.Remove(nameof(model.PostnaStevilka));
            ModelState.Remove(nameof(model.Posta));
            ModelState.Remove(nameof(model.Drzava));
            ModelState.Remove(nameof(model.ENaslov));
            ModelState.Remove(nameof(model.Geslo));
            ModelState.Remove(nameof(model.GesloPotrdi));

            if (!ModelState.IsValid) return View(model);

            // Shranimo v session
            var obstojeciModel = NaloziIzSessiona();
            obstojeciModel.Ime = model.Ime;
            obstojeciModel.Priimek = model.Priimek;
            obstojeciModel.DatumRojstva = model.DatumRojstva;
            obstojeciModel.Emso = model.Emso;
            obstojeciModel.Starost = model.Starost;
            ShraniVSession(obstojeciModel);

            // PRG: preusmerimo na GET korak 2
            return RedirectToAction(nameof(Korak2));
        }

        // ==================== KORAK 2 ====================
        // Naloga 3: Korak 2 - Naslov, poštna številka, pošta, država

        [HttpGet]
        public IActionResult Korak2()
        {
            var model = NaloziIzSessiona();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Korak2(UporabnikZGesli model)
        {
            ModelState.Remove(nameof(model.Ime));
            ModelState.Remove(nameof(model.Priimek));
            ModelState.Remove(nameof(model.Emso));
            ModelState.Remove(nameof(model.ENaslov));
            ModelState.Remove(nameof(model.Geslo));
            ModelState.Remove(nameof(model.GesloPotrdi));

            if (!ModelState.IsValid) return View(model);

            var obstojeciModel = NaloziIzSessiona();
            obstojeciModel.Naslov = model.Naslov;
            obstojeciModel.PostnaStevilka = model.PostnaStevilka;
            obstojeciModel.Posta = model.Posta;
            obstojeciModel.Drzava = model.Drzava;
            ShraniVSession(obstojeciModel);

            return RedirectToAction(nameof(Korak3));
        }

        // ==================== KORAK 3 ====================
        // Naloga 3: Korak 3 - E-naslov in geslo (dvakrat)

        [HttpGet]
        public IActionResult Korak3()
        {
            var model = NaloziIzSessiona();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Korak3(UporabnikZGesli model)
        {
            ModelState.Remove(nameof(model.Ime));
            ModelState.Remove(nameof(model.Priimek));
            ModelState.Remove(nameof(model.Naslov));
            ModelState.Remove(nameof(model.PostnaStevilka));
            ModelState.Remove(nameof(model.Posta));
            ModelState.Remove(nameof(model.Drzava));

            if (!ModelState.IsValid) return View(model);

            // Preverimo ali se gesli ujemata
            if (model.Geslo != model.GesloPotrdi)
            {
                ModelState.AddModelError(nameof(model.GesloPotrdi), "Gesli se ne ujemata!");
                return View(model);
            }

            var obstojeciModel = NaloziIzSessiona();
            obstojeciModel.ENaslov = model.ENaslov;
            obstojeciModel.Geslo = model.Geslo;
            obstojeciModel.GesloPotrdi = model.GesloPotrdi;
            ShraniVSession(obstojeciModel);

            return RedirectToAction(nameof(Korak4));
        }

        // ==================== KORAK 4 ====================
        // Naloga 3: Korak 4 - Izpis vseh vpisanih podatkov

        [HttpGet]
        public IActionResult Korak4()
        {
            var model = NaloziIzSessiona();
            return View(model);
        }
    }
}
