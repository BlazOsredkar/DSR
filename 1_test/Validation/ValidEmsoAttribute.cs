// Naloga 4: Lastna validacija za EMŠO (ValidationAttribute)
// Naloga 4: Vir: https://sl.wikipedia.org/wiki/Enotna_mati%C4%8Dna_%C5%A1tevilka_ob%C4%8Dana
// EMŠO = 13 številk, zadnja je kontrolna številka
// Format: DDMMLLXXXXXK (dan, mesec, leto, regija+zap.st., kontrolna)

using System.ComponentModel.DataAnnotations;

namespace RentACar.Validation
{
    public class ValidEmsoAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            string emso = value.ToString() ?? "";

            // Mora biti točno 13 številk
            if (emso.Length != 13 || !emso.All(char.IsDigit))
                return false;

            // Izračun kontrolne številke po algoritmu iz Wikipedije
            // Koeficienti za prvih 12 številk
            int[] koef = { 7, 6, 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int vsota = 0;

            for (int i = 0; i < 12; i++)
            {
                vsota += (emso[i] - '0') * koef[i];
            }

            int ostanek = vsota % 11;
            int kontrolna;

            if (ostanek == 0)
                kontrolna = 0;
            else if (ostanek == 1)
                return false; // EMŠO ni veljaven
            else
                kontrolna = 11 - ostanek;

            // Zadnja številka mora biti enaka kontrolni
            return (emso[12] - '0') == kontrolna;
        }
    }
}
