// Naloga 1: Glavna entiteta "Avto" (izdelek/storitev v našem sistemu)
// Naloga 5: Tukaj se avti shranjujejo v bazo s CRUD operacijami
// Vsebuje vse 4 osnovne tipe: String, DateTime, Int, Double

using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Avto
    {
        // Primarni ključ
        public int Id { get; set; }

        // String tipi
        [Display(Name = "Znamka")]
        public string Znamka { get; set; } = "";

        [Display(Name = "Model")]
        public string Model { get; set; } = "";

        [Display(Name = "Barva")]
        public string Barva { get; set; } = "";

        [Display(Name = "Registrska št.")]
        public string RegistrskaStevilka { get; set; } = "";

        // Int tip
        [Display(Name = "Letnik")]
        public int Letnik { get; set; }

        [Display(Name = "Število sedežev")]
        public int SteviloSedezev { get; set; }

        // Double tip
        [Display(Name = "Cena na dan (EUR)")]
        public double CenaNaDan { get; set; }

        [Display(Name = "Prostornina motorja (L)")]
        public double ProstorninaMotorja { get; set; }

        // DateTime tip
        [Display(Name = "Datum zadnjega servisa")]
        public DateTime DatumZadnjegaServisa { get; set; }

        // Navigacijska lastnost za EF (Naloga 5)
        public List<Rezervacija> Rezervacije { get; set; } = new();
    }
}
