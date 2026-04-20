using Microsoft.AspNetCore.Mvc;
using _1_test.Infrastructure;
using _1_test.Models;
using _1_test.Models.ViewModels;

namespace _1_test.Controllers;

public class RegistrationController : Controller
{
    private const string SessionKey = "RegistrationData";
    private const string TabsSessionKey = "RegistrationTabsData";

    // Naloga: 4-koracni obrazec (navodilo: "spletni obrazec iz 4 korakov").
    public IActionResult Step1()
    {
        var data = GetSessionData();
        var model = new RegistrationStep1ViewModel
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            BirthDate = data.BirthDate,
            Emso = data.Emso,
            Age = data.Age
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Step1(RegistrationStep1ViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var data = GetSessionData();
        data.FirstName = model.FirstName;
        data.LastName = model.LastName;
        data.BirthDate = model.BirthDate;
        data.Emso = model.Emso;
        data.Age = model.Age;
        SaveSessionData(data);

        // Naloga: PRG (navodilo: "Pri preusmerjanju upostevajte vzorec PRG").
        return RedirectToAction(nameof(Step2));
    }

    public IActionResult Step2()
    {
        var data = GetSessionData();
        var model = new RegistrationStep2ViewModel
        {
            Address = data.Address,
            PostalCode = data.PostalCode,
            City = data.City,
            Country = data.Country
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Step2(RegistrationStep2ViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var data = GetSessionData();
        data.Address = model.Address;
        data.PostalCode = model.PostalCode;
        data.City = model.City;
        data.Country = model.Country;
        SaveSessionData(data);

        return RedirectToAction(nameof(Step3));
    }

    public IActionResult Step3()
    {
        var data = GetSessionData();
        var model = new RegistrationStep3ViewModel
        {
            Email = data.Email,
            Password = data.Password,
            ConfirmPassword = data.Password
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Step3(RegistrationStep3ViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var data = GetSessionData();
        data.Email = model.Email;
        data.Password = model.Password;
        SaveSessionData(data);

        return RedirectToAction(nameof(Summary));
    }

    public IActionResult Summary()
    {
        var data = GetSessionData();
        if (string.IsNullOrWhiteSpace(data.FirstName))
        {
            return RedirectToAction(nameof(Step1));
        }

        return View(data);
    }

    // Naloga: obrazec z zavihki (navodilo: "Obrazec ... zavihki").
    public IActionResult Tabs()
    {
        var model = HttpContext.Session.GetObject<UserWithPassword>(TabsSessionKey) ?? new UserWithPassword
        {
            BirthDate = DateTime.Today
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Tabs(UserWithPassword model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        HttpContext.Session.SetObject(TabsSessionKey, model);
        return RedirectToAction(nameof(TabsSummary));
    }

    public IActionResult TabsSummary()
    {
        var model = HttpContext.Session.GetObject<UserWithPassword>(TabsSessionKey);
        if (model == null)
        {
            return RedirectToAction(nameof(Tabs));
        }

        return View(model);
    }

    private RegistrationSessionModel GetSessionData()
    {
        return HttpContext.Session.GetObject<RegistrationSessionModel>(SessionKey) ?? new RegistrationSessionModel();
    }

    private void SaveSessionData(RegistrationSessionModel data)
    {
        HttpContext.Session.SetObject(SessionKey, data);
    }
}
