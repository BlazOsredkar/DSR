using Microsoft.AspNetCore.Mvc;
using _1_test.Infrastructure;
using _1_test.Models;

namespace _1_test.Controllers;

public class FormsController : Controller
{
    private const string UserFormKey = "UserFormPreview";
    private const string BookFormKey = "BookFormPreview";

    // Naloga: EditorForModel + DisplayForModel za uporabnika (navodilo: "Obrazec ... EditorForModel() ... DisplayForModel()").
    public IActionResult UserForm()
    {
        return View(new UserWithPassword { BirthDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UserForm(UserWithPassword model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        HttpContext.Session.SetObject(UserFormKey, model);
        return RedirectToAction(nameof(UserPreview));
    }

    public IActionResult UserPreview()
    {
        var model = HttpContext.Session.GetObject<UserWithPassword>(UserFormKey);
        if (model == null)
        {
            return RedirectToAction(nameof(UserForm));
        }

        return View(model);
    }

    // Naloga: EditorForModel + DisplayForModel za izdelek (navodilo: "obrazec za dodajanje in ogled izdelkov").
    public IActionResult BookForm()
    {
        return View(new Book { PublishDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BookForm(Book model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        HttpContext.Session.SetObject(BookFormKey, model);
        return RedirectToAction(nameof(BookPreview));
    }

    public IActionResult BookPreview()
    {
        var model = HttpContext.Session.GetObject<Book>(BookFormKey);
        if (model == null)
        {
            return RedirectToAction(nameof(BookForm));
        }

        return View(model);
    }
}
