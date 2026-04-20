using Microsoft.AspNetCore.Mvc;

namespace _1_test.Controllers;

public class PresentationController : Controller
{
    // Naloga: staticne strani za glavne entitete (navodilo: "izpisete v staticni obliki podatke").
    public IActionResult Users()
    {
        return View();
    }

    public IActionResult Books()
    {
        return View();
    }

    public IActionResult Orders()
    {
        return View();
    }
}
