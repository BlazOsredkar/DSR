# Dokumentacija projekta 1_test

Ta dokument opisuje, kje je kaj po navodilih v kodi in kako posamezni deli delujejo.

## Hiter zagon

```bash
cd 1_test
dotnet restore
dotnet run
```

Podatkovna baza se ustvari z migracijami in se napolni s semenskimi podatki.
To je v [1_test/Data/DbSeeder.cs](1_test/Data/DbSeeder.cs).

Ce zelis ročno ustvariti migracije:

```bash
cd 1_test
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Izbrane entitete (3 glavne entitete)

- Uporabnik: [1_test/Models/User.cs](1_test/Models/User.cs)
- Knjiga (izdelek/storitev): [1_test/Models/Book.cs](1_test/Models/Book.cs)
- Narocilo (obdelava podatkov): [1_test/Models/Order.cs](1_test/Models/Order.cs)
- Nakup (Web API + kosarica): [1_test/Models/Purchase.cs](1_test/Models/Purchase.cs)

Vse entitete vsebujejo osnovne tipe (DateTime, String, Int, Double) in validacije.

## Identity uporabnik (prijava in vloge)

- Identity uporabnik: [1_test/Models/ApplicationUser.cs](1_test/Models/ApplicationUser.cs)
- Vloge (admin/user): [1_test/Infrastructure/AppRoles.cs](1_test/Infrastructure/AppRoles.cs)
- Seed admin uporabnika in vlog: [1_test/Data/IdentitySeeder.cs](1_test/Data/IdentitySeeder.cs)

## Code First baza in kontekst

- DbContext z DbSet polji: [1_test/Data/AppDbContext.cs](1_test/Data/AppDbContext.cs)
- Sejanje podatkov in `Migrate()`: [1_test/Data/DbSeeder.cs](1_test/Data/DbSeeder.cs)
- Povezava na SQLite v konfiguraciji: [1_test/appsettings.json](1_test/appsettings.json)
- Registracija DB in sej: [1_test/Program.cs](1_test/Program.cs)
- Identity in Google prijava: [1_test/Program.cs](1_test/Program.cs)

## Uporabnik brez gesel v bazi

- Osnovni uporabnik brez gesel: [1_test/Models/User.cs](1_test/Models/User.cs)
- Razsirjen uporabnik z gesli (NotMapped): [1_test/Models/UserWithPassword.cs](1_test/Models/UserWithPassword.cs)

To izpolni zahtevo, da razred za bazo ne vsebuje podatkov o geslu.

## Layout, glava, noga, navigacija

- Layout (glava, meni, noga, datum/cas): [1_test/Views/Shared/\_Layout.cshtml](1_test/Views/Shared/_Layout.cshtml)
- Glavni vsebinski del se spreminja prek `@RenderBody()` v layoutu.
- Navigacija vsebuje povezave do vseh podstrani.

## Trenutni datum in cas

- Izpis ob vsakem nalaganju strani: [1_test/Views/Shared/\_Layout.cshtml](1_test/Views/Shared/_Layout.cshtml)

## Skupni CSS in poravnava besedila

- Skupni stil: [1_test/wwwroot/css/site.css](1_test/wwwroot/css/site.css)
- Obojestranska poravnava vsebine je v `.content`.
- Barve, tipografija in poravnave so definirane na enem mestu.

## Responsive design in UI framework

- Bootstrap je vkljucen v layout: [1_test/Views/Shared/\_Layout.cshtml](1_test/Views/Shared/_Layout.cshtml)
- Uporabljeni so Bootstrap grid in komponente v viewih (npr. domača stran).

## Strani za glavne entitete (dynamicne)

- Uporabniki (seznam/CRUD): [1_test/Views/Users/Index.cshtml](1_test/Views/Users/Index.cshtml)
- Knjige (seznam/CRUD): [1_test/Views/Books/Index.cshtml](1_test/Views/Books/Index.cshtml)
- Narocila (accordion prikaz): [1_test/Views/Orders/Index.cshtml](1_test/Views/Orders/Index.cshtml)

## Strani za glavne entitete (staticne)

- Uporabniki (staticno): [1_test/Views/Presentation/Users.cshtml](1_test/Views/Presentation/Users.cshtml)
- Knjige (staticno): [1_test/Views/Presentation/Books.cshtml](1_test/Views/Presentation/Books.cshtml)
- Narocila (staticno): [1_test/Views/Presentation/Orders.cshtml](1_test/Views/Presentation/Orders.cshtml)

## jQuery UI komponente

- Accordion za narocila: [1_test/Views/Orders/Index.cshtml](1_test/Views/Orders/Index.cshtml) + [1_test/wwwroot/js/site.js](1_test/wwwroot/js/site.js)
- Tabs za registracijski obrazec: [1_test/Views/Registration/Tabs.cshtml](1_test/Views/Registration/Tabs.cshtml) + [1_test/wwwroot/js/site.js](1_test/wwwroot/js/site.js)
- Datepicker za DateTime: [1_test/Views/Shared/EditorTemplates/DateTime.cshtml](1_test/Views/Shared/EditorTemplates/DateTime.cshtml) + [1_test/wwwroot/js/site.js](1_test/wwwroot/js/site.js)
- Slider za Int32/Double: [1_test/Views/Shared/EditorTemplates/Int32.cshtml](1_test/Views/Shared/EditorTemplates/Int32.cshtml), [1_test/Views/Shared/EditorTemplates/Double.cshtml](1_test/Views/Shared/EditorTemplates/Double.cshtml) + [1_test/wwwroot/js/site.js](1_test/wwwroot/js/site.js)

## 4-koracni registracijski obrazec (PRG)

- Kontroler s PRG: [1_test/Controllers/RegistrationController.cs](1_test/Controllers/RegistrationController.cs)
- View modeli za korake: [1_test/Models/ViewModels/RegistrationStep1ViewModel.cs](1_test/Models/ViewModels/RegistrationStep1ViewModel.cs), [1_test/Models/ViewModels/RegistrationStep2ViewModel.cs](1_test/Models/ViewModels/RegistrationStep2ViewModel.cs), [1_test/Models/ViewModels/RegistrationStep3ViewModel.cs](1_test/Models/ViewModels/RegistrationStep3ViewModel.cs)
- Sejni model za zbiranje podatkov: [1_test/Models/ViewModels/RegistrationSessionModel.cs](1_test/Models/ViewModels/RegistrationSessionModel.cs)
- Pogledi korakov in povzetek: [1_test/Views/Registration/Step1.cshtml](1_test/Views/Registration/Step1.cshtml), [1_test/Views/Registration/Step2.cshtml](1_test/Views/Registration/Step2.cshtml), [1_test/Views/Registration/Step3.cshtml](1_test/Views/Registration/Step3.cshtml), [1_test/Views/Registration/Summary.cshtml](1_test/Views/Registration/Summary.cshtml)
- Shranjevanje v sejo: [1_test/Infrastructure/SessionExtensions.cs](1_test/Infrastructure/SessionExtensions.cs)

Kako deluje:

- Vsak korak je `GET` prikaz in `POST` obdelava.
- Po `POST` se podatki shranijo v sejo in sledi `RedirectToAction`, kar je PRG.
- Povzetek prebere podatke iz seje in jih prikaze v tabeli.

## Strogo tipiziran obrazec z zavihki

- Model z validacijo: [1_test/Models/UserWithPassword.cs](1_test/Models/UserWithPassword.cs)
- Pogled z zavihki: [1_test/Views/Registration/Tabs.cshtml](1_test/Views/Registration/Tabs.cshtml)
- Povzetek po PRG: [1_test/Views/Registration/TabsSummary.cshtml](1_test/Views/Registration/TabsSummary.cshtml)

Validacija je implementirana z DataAnnotations (brez JavaScript validacije).

## EditorForModel in DisplayForModel

- Uporabnik (vnos + predogled): [1_test/Views/Forms/UserForm.cshtml](1_test/Views/Forms/UserForm.cshtml), [1_test/Views/Forms/UserPreview.cshtml](1_test/Views/Forms/UserPreview.cshtml)
- Knjiga (vnos + predogled): [1_test/Views/Forms/BookForm.cshtml](1_test/Views/Forms/BookForm.cshtml), [1_test/Views/Forms/BookPreview.cshtml](1_test/Views/Forms/BookPreview.cshtml)
- Kontroler PRG: [1_test/Controllers/FormsController.cs](1_test/Controllers/FormsController.cs)

Kako deluje:

- `EditorForModel()` uporabi predloge za osnovne tipe (Int32/Double/DateTime).
- Po `POST` se podatki shranijo v sejo in sledi `RedirectToAction`.
- `DisplayForModel()` prikaze podatke v povzetku.

## Editor/Display predloge za osnovne tipe

- Int32 editor/display: [1_test/Views/Shared/EditorTemplates/Int32.cshtml](1_test/Views/Shared/EditorTemplates/Int32.cshtml), [1_test/Views/Shared/DisplayTemplates/Int32.cshtml](1_test/Views/Shared/DisplayTemplates/Int32.cshtml)
- Double editor/display: [1_test/Views/Shared/EditorTemplates/Double.cshtml](1_test/Views/Shared/EditorTemplates/Double.cshtml), [1_test/Views/Shared/DisplayTemplates/Double.cshtml](1_test/Views/Shared/DisplayTemplates/Double.cshtml)
- DateTime editor/display: [1_test/Views/Shared/EditorTemplates/DateTime.cshtml](1_test/Views/Shared/EditorTemplates/DateTime.cshtml), [1_test/Views/Shared/DisplayTemplates/DateTime.cshtml](1_test/Views/Shared/DisplayTemplates/DateTime.cshtml)

## Validacija (brez JavaScript validacije)

- Ime, priimek, email, gesla: [1_test/Models/User.cs](1_test/Models/User.cs), [1_test/Models/UserWithPassword.cs](1_test/Models/UserWithPassword.cs)
- EMSO custom atribut: [1_test/Validation/EmsoAttribute.cs](1_test/Validation/EmsoAttribute.cs)
- Validacija v korakih: [1_test/Models/ViewModels/RegistrationStep1ViewModel.cs](1_test/Models/ViewModels/RegistrationStep1ViewModel.cs), [1_test/Models/ViewModels/RegistrationStep3ViewModel.cs](1_test/Models/ViewModels/RegistrationStep3ViewModel.cs)

### Strogi format datuma (dd.MM.yyyy)

- Custom model binder za DateTime: [1_test/Infrastructure/StrictDateModelBinder.cs](1_test/Infrastructure/StrictDateModelBinder.cs)
- Registracija binderja: [1_test/Program.cs](1_test/Program.cs)

## Dodatno: Double z vejico/piko

- Custom model binder za Double: [1_test/Infrastructure/FlexibleDoubleModelBinder.cs](1_test/Infrastructure/FlexibleDoubleModelBinder.cs)
- Registracija binderja: [1_test/Program.cs](1_test/Program.cs)

## Kje so CRUD kontrolerji

- Uporabniki CRUD: [1_test/Controllers/UsersController.cs](1_test/Controllers/UsersController.cs)
- Knjige CRUD: [1_test/Controllers/BooksController.cs](1_test/Controllers/BooksController.cs)
- Narocila (seznam): [1_test/Controllers/OrdersController.cs](1_test/Controllers/OrdersController.cs)

## Avtentikacija in avtorizacija (Vaja 6)

- Prijava/odjava + zunanji ponudnik: [1_test/Controllers/AccountController.cs](1_test/Controllers/AccountController.cs)
- Registracija navadnega uporabnika (obstojeci koraki): [1_test/Controllers/RegistrationController.cs](1_test/Controllers/RegistrationController.cs)
- Administratorsko upravljanje uporabnikov (Identity): [1_test/Controllers/AdminUsersController.cs](1_test/Controllers/AdminUsersController.cs)
- Prikaz povezav glede na vlogo: [1_test/Views/Shared/\_Layout.cshtml](1_test/Views/Shared/_Layout.cshtml)
- Omejitve dostopa: `[Authorize]` in `[Authorize(Roles = "admin")]` v kontrolerjih (Books, Orders, AdminUsers)

Privzeta uporabnika (seed):

- admin: `admin@dsr.local` / `Admin123!`
- user: `user@dsr.local` / `User123!`

Za Google prijavo vnesi `ClientId` in `ClientSecret` v konfiguracijo (glej Vaja 6 dokumentacijo).

## Web API nakupi (Vaja 7)

- Model nakupa: [1_test/Models/Purchase.cs](1_test/Models/Purchase.cs)
- Postavke nakupa: [1_test/Models/PurchaseItem.cs](1_test/Models/PurchaseItem.cs)
- DTOji za Web API: [1_test/Models/Dto/PurchaseDtos.cs](1_test/Models/Dto/PurchaseDtos.cs)
- Web API kontroler: [1_test/Controllers/PurchasesApiController.cs](1_test/Controllers/PurchasesApiController.cs)
- MVC kosarica + checkout: [1_test/Controllers/PurchasesController.cs](1_test/Controllers/PurchasesController.cs)
- Pogledi kosarice in zgodovine: [1_test/Views/Purchases](1_test/Views/Purchases)

## Testni projekti (Vaja 8 + Vaja 9)

- Unit testi (xUnit): [1_test/1_test.Tests](1_test/1_test.Tests)
- Selenium UI testi: [1_test/1_test.SeleniumTests](1_test/1_test.SeleniumTests)
- Navodila za unit teste: [1_test/DOKUMENTACIJE/Vaja8_UnitTesti.md](1_test/DOKUMENTACIJE/Vaja8_UnitTesti.md)
- Navodila za Selenium teste: [1_test/DOKUMENTACIJE/Vaja9_Selenium.md](1_test/DOKUMENTACIJE/Vaja9_Selenium.md)

## Kako deluje (na kratko za test)

1. Ob zagonu se aplikacija zazene, uporabi migracije in doda semenske podatke.
2. Layout vedno nalozi glavo, navigacijo, nogo in trenutni datum/cas.
3. Strani entitet imajo posebej staticne predstavitve in posebej dinamicne CRUD sezname.
4. 4-koracna registracija shranjuje podatke v sejo in uporablja PRG.
5. Zavihki (Tabs) so en obrazec z validacijo, po oddaji sledi PRG in povzetek.
6. `EditorForModel()` uporabi predloge tipov (slider, datepicker), `DisplayForModel()` prikaze vrednosti.
