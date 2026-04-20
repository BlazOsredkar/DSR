// Naloga 1: Glavna entiteta "Rezervacija" (obdelava podatkov - najem avta)
// Naloga 5: Tukaj se rezervacije shranjujejo v bazo
// Vsebuje vse 4 osnovne tipe: String, DateTime, Int, Double

using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Rezervacija
    {
        // Primarni ključ
        public int Id { get; set; }

        // DateTime tipi
        [Display(Name = "Datum od")]
        public DateTime DatumOd { get; set; }

        [Display(Name = "Datum do")]
        public DateTime DatumDo { get; set; }

        // String tip
        [Display(Name = "Opombe")]
        public string Opombe { get; set; } = "";

        [Display(Name = "Status")]
        public string Status { get; set; } = "Aktivna";

        // Int tip - število dni najema
        [Display(Name = "Število dni")]
        public int SteviloDni { get; set; }

        // Double tip - skupna cena
        [Display(Name = "Skupna cena (EUR)")]
        public double SkupnaCena { get; set; }

        // Tuji ključi za EF (Naloga 5)
        public int UporabnikId { get; set; }
        public Uporabnik? Uporabnik { get; set; }

        public int AvtoId { get; set; }
        public Avto? Avto { get; set; }
    }
}
