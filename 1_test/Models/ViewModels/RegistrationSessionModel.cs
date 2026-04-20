namespace _1_test.Models.ViewModels;

public class RegistrationSessionModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; } = DateTime.Today;
    public string Emso { get; set; } = string.Empty;
    public int Age { get; set; }

    public string Address { get; set; } = string.Empty;
    public int PostalCode { get; set; }
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}
