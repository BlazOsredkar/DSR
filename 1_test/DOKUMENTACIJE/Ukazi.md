## 🔧 Osnovne .NET CLI komande (z Entity Framework)

## `dotnet restore`

- Prenese vse odvisnosti (NuGet pakete), ki jih projekt potrebuje.
- Bere iz `.csproj` datoteke in namesti manjkajoče knjižnice.
- Običajno se izvede avtomatsko pri `build`, ampak jo lahko zaženeš posebej.
**Primer:**
`dotnet restore`

---
## `dotnet build`

- Prevede (compile-a) projekt v izvršljivo obliko (DLL ali EXE).
- Preveri tudi napake v kodi.
- Če odvisnosti še niso nameščene, jih najprej obnovi (`restore`).
**Primer:**
`dotnet build`

---
## 🗄️ Entity Framework (EF Core) komande

## `dotnet ef migrations add InitialCreate`

- Ustvari prvo migracijo (začetno strukturo baze).
- Pregleda tvoje modele (C# razrede) in generira SQL spremembe.
- `InitialCreate` je ime migracije (lahko je karkoli smiselnega).

**Primer:**
`dotnet ef migrations add InitialCreate`

---
## `dotnet ef migrations add AddBlogCreatedTimestamp`

- Ustvari novo migracijo, ko spremeniš modele (npr. dodaš novo polje).
- V tem primeru bi dodal npr. `CreatedAt` stolpec v tabelo Blog.
- 
**Primer:**
`dotnet ef migrations add AddBlogCreatedTimestamp`

---
## `dotnet ef database update`

- Uporabi vse migracije na bazi (dejansko spremeni strukturo baze).
- Ustvari tabelo, doda stolpce itd., glede na migracije.

**Primer:**
`dotnet ef database update`

---
## 🧠 Kako gre workflow skupaj

1. Spremeniš modele (npr. dodaš property v razred).
2. Ustvariš migracijo:
    `dotnet ef migrations add ImeMigracije`
3. Posodobiš bazo:
    `dotnet ef database update`