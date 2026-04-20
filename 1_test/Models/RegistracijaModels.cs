// Naloga 3: Modeli za 4-koračni registracijski obrazec
// Naloga 4: Strogo tipiziran obrazec z validacijo
// Naloga 4: Validacijo implementiramo z Data Annotations (NE z JavaScriptom!)

using System.ComponentModel.DataAnnotations;
using RentACar.Validation;

namespace RentACar.Models
{
    // Naloga 4: Korak 1 - osebni podatki z validacijo
    public class Korak1Model
    {
        // Naloga 4: Samo črkovna znaki dovoljeni
        [Required(ErrorMessage = "Ime je obvezno!")]
        [RegularExpression(@"^[a-zA-ZščžŠČŽ]+$", ErrorMessage = "Ime sme vsebovati le črkee!")]
        [Display(Name = "Ime")]
        public string Ime { get; set; } = "";

        [Required(ErrorMessage = "Priimek je obvezen!")]
        [RegularExpression(@"^[a-zA-ZščžŠČŽ]+$", ErrorMessage = "Priimek sme vsebovati le črke!")]
        [Display(Name = "Priimek")]
        public string Priimek { get; set; } = "";

        // Naloga 4: Datum rojstva z validacijo formata
        [Required(ErrorMessage = "Datum rojstva je obvezen!")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum rojstva")]
        public DateTime DatumRojstva { get; set; }

        // Naloga 4: Kraj rojstva - padajoči meni (vsaj 5 krajev)
        [Required(ErrorMessage = "Kraj rojstva je obvezen!")]
        [Display(Name = "Kraj rojstva")]
        public string KrajRojstva { get; set; } = "";

        // Naloga 4: EMŠO - 13 zaporednih številk + lastna validacija
        [Required(ErrorMessage = "EMŠO je obvezen!")]
        [ValidEmso(ErrorMessage = "EMŠO ni veljaven! Mora vsebovati točno 13 številk in imeti pravilno kontrolno številko.")]
        [Display(Name = "EMŠO")]
        public string Emso { get; set; } = "";

        [Required(ErrorMessage = "Starost je obvezna!")]
        [Range(18, 100, ErrorMessage = "Starost mora biti med 18 in 100 let!")]
        [Display(Name = "Starost")]
        public int Starost { get; set; }
    }

    // Naloga 3+4: Korak 2 - naslov
    public class Korak2Model
    {
        [Required(ErrorMessage = "Naslov je obvezen!")]
        [Display(Name = "Naslov")]
        public string Naslov { get; set; } = "";

        [Required(ErrorMessage = "Poštna številka je obvezna!")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Poštna številka mora biti 4-številke!")]
        [Display(Name = "Poštna številka")]
        public string PostnaStevilka { get; set; } = "";

        [Required(ErrorMessage = "Pošta je obvezna!")]
        [Display(Name = "Pošta")]
        public string Posta { get; set; } = "";

        [Required(ErrorMessage = "Država je obvezna!")]
        [Display(Name = "Država")]
        public string Drzava { get; set; } = "";
    }

    // Naloga 3+4: Korak 3 - e-naslov in geslo
    public class Korak3Model
    {
        // Naloga 4: Validacija e-naslova s preverbo @, domene in imena
        [Required(ErrorMessage = "E-naslov je obvezen!")]
        [RegularExpression(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "E-naslov ni v pravilni obliki (npr. ime@domena.com)!")]
        [Display(Name = "E-naslov")]
        public string ENaslov { get; set; } = "";

        // Naloga 4: Geslo mora vsebovati vsaj 1 številko in 1 poseben znak
        [Required(ErrorMessage = "Geslo je obvezno!")]
        [RegularExpression(
            @"^(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$",
            ErrorMessage = "Geslo mora vsebovati vsaj 6 znakov, eno številko in en poseben znak!")]
        [DataType(DataType.Password)]
        [Display(Name = "Geslo")]
        public string Geslo { get; set; } = "";

        // Naloga 4: Gesli se morata ujemati
        [Required(ErrorMessage = "Potrditev gesla je obvezna!")]
        [Compare(nameof(Geslo), ErrorMessage = "Gesli se ne ujemata!")]
        [DataType(DataType.Password)]
        [Display(Name = "Potrdi geslo")]
        public string GesloPotrdi { get; set; } = "";
    }

    // Naloga 3+4: Celoten model (vse korake skupaj) - za korak 4 in session
    public class RegistracijaSessionModel
    {
        public string Ime { get; set; } = "";
        public string Priimek { get; set; } = "";
        public DateTime DatumRojstva { get; set; }
        public string KrajRojstva { get; set; } = "";
        public string Emso { get; set; } = "";
        public int Starost { get; set; }
        public string Naslov { get; set; } = "";
        public string PostnaStevilka { get; set; } = "";
        public string Posta { get; set; } = "";
        public string Drzava { get; set; } = "";
        public string ENaslov { get; set; } = "";
        public string Geslo { get; set; } = "";
    }
}
