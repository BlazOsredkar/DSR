# Vaja 6 — Avtentikacija, vloge in Identity

Cilj je dodati prijavo, registracijo in vloge (user/admin) z uporabo .NET Core Identity.

## 1) Kaj smo dodali

- **Identity uporabnik**: [1_test/Models/ApplicationUser.cs](1_test/Models/ApplicationUser.cs)
- **Vloge**: [1_test/Infrastructure/AppRoles.cs](1_test/Infrastructure/AppRoles.cs)
- **Seed uporabniki**: [1_test/Data/IdentitySeeder.cs](1_test/Data/IdentitySeeder.cs)
- **Account kontroler (login/logout + Google)**: [1_test/Controllers/AccountController.cs](1_test/Controllers/AccountController.cs)
- **Registracija (obstojeci koraki)**: [1_test/Controllers/RegistrationController.cs](1_test/Controllers/RegistrationController.cs)
- **Admin upravljanje uporabnikov**: [1_test/Controllers/AdminUsersController.cs](1_test/Controllers/AdminUsersController.cs)
- **Navigacija glede na vlogo**: [1_test/Views/Shared/\_Layout.cshtml](1_test/Views/Shared/_Layout.cshtml)

## 2) Vloge in dostopi

- Vloga **user**:
  - Domov, seznam knjig, kosarica, pregled nakupov.
- Vloga **admin**:
  - Vse od user + urejanje knjig, upravljanje uporabnikov, pregled nakupov.

V kontrolerjih uporabljamo `Authorize`:

```csharp
[Authorize]
public class BooksController : Controller
{
    [Authorize(Roles = "admin")]
    public IActionResult Create() { ... }
}
```

## 3) Seed uporabniki

V [1_test/Data/IdentitySeeder.cs](1_test/Data/IdentitySeeder.cs) sta dodana dva uporabnika:

- **admin**: `admin@dsr.local` / `Admin123!`
- **user**: `user@dsr.local` / `User123!`

> To je zgolj za testiranje. Gesla so preprosta, ker je to šolski projekt.

## 4) Registracija uporabnika

Uporabljamo obstojeci 3-koracni obrazec. Po povzetku kliknemo **"Zakljuci registracijo"**.

Koda je v [1_test/Controllers/RegistrationController.cs](1_test/Controllers/RegistrationController.cs):

```csharp
[HttpPost]
public async Task<IActionResult> Complete()
{
    // ...
    var result = await _userManager.CreateAsync(user, data.Password);
    await _userManager.AddToRoleAsync(user, AppRoles.User);
}
```

## 5) Prijava in odjava

- Prijava: `/Account/Login`
- Odjava: gumb **Odjava** v navigaciji

Prijava uporablja **email + geslo**:

```csharp
await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
```

## 6) Zunanji ponudnik (Google)

V `appsettings.json` sta dodani prazni vrednosti:

```json
"Authentication": {
  "Google": {
    "ClientId": "",
    "ClientSecret": ""
  }
}
```

### Kako dodas prave podatke (priporoceno prek User Secrets)

```bash
cd 1_test

dotnet user-secrets init

dotnet user-secrets set "Authentication:Google:ClientId" "YOUR_ID"
dotnet user-secrets set "Authentication:Google:ClientSecret" "YOUR_SECRET"
```

Ko sta vrednosti nastavljeni, se v prijavi prikaze gumb Google.

## 7) Admin upravljanje uporabnikov

Admin lahko dodaja/ureja/brise uporabnike na:

`/AdminUsers`

To je implementirano v [1_test/Controllers/AdminUsersController.cs](1_test/Controllers/AdminUsersController.cs).

## 8) Povzetek

- Identiteta in vloge so urejene z ASP.NET Core Identity.
- Registracija ustvari navadnega uporabnika.
- Admin upravlja uporabnike in ima dodatne menije.
- Google prijava je pripravljena, le nastavi se ClientId/Secret.
