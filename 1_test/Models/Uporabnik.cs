// Naloga 1: Glavna entiteta "Uporabnik"
// Naloga 3+4: Ta razred se uporablja tudi v obrazcu za registracijo
// Naloga 5: POMEMBNO - ta razred ne vsebuje gesel (kot piše v navodilu naloge 5)
//           Razred vsebuje vse 4 osnovne tipe: String, DateTime, Int, Double

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models
{
    public class Uporabnik
    {
        // Primarni ključ za bazo (Naloga 5)
        public int Id { get; set; }

        // String tipi
        [Display(Name = "Ime")]
        public string Ime { get; set; } = "";

        [Display(Name = "Priimek")]
        public string Priimek { get; set; } = "";

        [Display(Name = "Naslov")]
        public string Naslov { get; set; } = "";

        [Display(Name = "Poštna številka")]
        public string PostnaStevilka { get; set; } = "";

        [Display(Name = "Pošta")]
        public string Posta { get; set; } = "";

        [Display(Name = "Država")]
        public string Drzava { get; set; } = "";

        [Display(Name = "E-naslov")]
        public string ENaslov { get; set; } = "";

        [Display(Name = "EMŠO")]
        public string Emso { get; set; } = "";

        // DateTime tip
        [Display(Name = "Datum rojstva")]
        public DateTime DatumRojstva { get; set; }

        // Int tip
        [Display(Name = "Starost")]
        public int Starost { get; set; }

        // Double tip - npr. skupni znesek najemov
        [Display(Name = "Skupni znesek najemov (EUR)")]
        public double SkupniZnesek { get; set; }

        // Navigacijska lastnost za EF (Naloga 5)
        public List<Rezervacija> Rezervacije { get; set; } = new();
    }

    // Naloga 5: Razred z gesli - NI shranjen v bazo ([NotMapped])
    // Naloga 3+4: Ta razred se uporablja v obrazcu za registracijo
    [NotMapped]
    public class UporabnikZGesli : Uporabnik
    {
        [Display(Name = "Geslo")]
        public string Geslo { get; set; } = "";

        [Display(Name = "Potrdi geslo")]
        public string GesloPotrdi { get; set; } = "";
    }
}
