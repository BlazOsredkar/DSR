using System.ComponentModel.DataAnnotations;

namespace _1_test.Validation;

public class EmsoAttribute : ValidationAttribute
{
    // Naloga: validacija EMSO s svojim atributom (navodilo: "Validacijo EMSA dopolnite z anotacijsko validacijo po meri").
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string emso || string.IsNullOrWhiteSpace(emso))
        {
            return new ValidationResult("EMSO je obvezen.");
        }

        if (emso.Length != 13 || !emso.All(char.IsDigit))
        {
            return new ValidationResult("EMSO mora imeti 13 stevilk.");
        }

        var weights = new[] { 7, 6, 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
        var sum = 0;

        for (var i = 0; i < weights.Length; i++)
        {
            sum += (emso[i] - '0') * weights[i];
        }

        var remainder = sum % 11;
        var control = 11 - remainder;
        if (control == 10)
        {
            return new ValidationResult("EMSO nima veljavne kontrolne stevilke.");
        }

        if (control == 11)
        {
            control = 0;
        }

        var lastDigit = emso[^1] - '0';
        if (lastDigit != control)
        {
            return new ValidationResult("EMSO nima veljavne kontrolne stevilke.");
        }

        return ValidationResult.Success;
    }
}
