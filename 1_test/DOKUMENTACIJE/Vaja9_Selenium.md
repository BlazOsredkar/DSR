# Vaja 9 — Selenium testi (UI testiranje)

Cilj vaje je avtomatsko testiranje spletne aplikacije z brskalnikom.

## 1) Testni projekt

Projekt: [1_test/1_test.SeleniumTests](1_test/1_test.SeleniumTests)

Dodani paketi:

- `Selenium.WebDriver`
- `Selenium.WebDriver.ChromeDriver`
- `Selenium.Support`

## 2) Setup (Chrome + aplikacija)

Predpogoji:

- Namecen .NET SDK
- Namecen Google Chrome
- Selenium WebDriver in ChromeDriver sta ze dodana kot NuGet paketa

Opomba za macOS (prvi zagon):

- Ce macOS blokira `chromedriver`, pojdi v System Settings -> Privacy & Security
   in klikni **Allow**.

Nato zazeni aplikacijo:

```bash
cd 1_test

dotnet run
```

App port preberi iz konzole (`Now listening on: ...`). Privzeto je ponavadi:

```
http://localhost:5104
```

Ce uporabljas drug port, nastavi okoljsko spremenljivko:

```bash
export APP_URL="http://localhost:5001"
```

Selenium je privzeto **headless**. Ce zelis viden brskalnik:

```bash
export HEADLESS=false
```

## 3) Selenium testi

Testi so v datoteki:

[1_test/1_test.SeleniumTests/Selenium/LoginAndPurchaseTests.cs](1_test/1_test.SeleniumTests/Selenium/LoginAndPurchaseTests.cs)

V testih uporabljamo:

```csharp
_driver = new ChromeDriver(options);
_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
```

## 3.1) Integracija Selenium (locen testni projekt)

Selenium ni del MVC aplikacije. Gre za **locen testni projekt**, ki od zunaj odpira brskalnik.

Koraki integracije:

1. Ustvari testni projekt in dodaj pakete (ze urejeno v tem projektu).
2. V testu inicializiraj `ChromeDriver`.
3. Zazeni app in nato teste.

Mini primer (konzolen test):

```csharp
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

public class MiniSeleniumTest
{
   [Fact]
   public void OdpreDomacoStran()
   {
      var options = new ChromeOptions();
      options.AddArgument("--headless=new");
      options.AddArgument("--window-size=1024,768");

      IWebDriver driver = new ChromeDriver(options);
      driver.Navigate().GoToUrl("http://localhost:5104");

      var title = driver.FindElement(By.TagName("h1")).Text;
      Assert.Contains("DSR", title);

      driver.Quit();
      driver.Dispose();
   }
}
```

To je osnova: **ustvaris driver, odpres URL, preveris elemente, zapres driver**.

## 4) Funkcionalnosti, ki jih testiramo (4 primeri)

1. **Pravilna prijava admina**
   - prijavi se z `admin@dsr.local`
   - preveri, da se pojavi meni "Upravljanje uporabnikov"

2. **Nepravilna prijava**
   - napacen password
   - preveri, da je izpisana napaka

3. **Nakup**
   - dodaj knjigo v kosarico
   - pojdi na blagajno
   - zakljuci nakup
   - preveri, da se prikaze "Moji nakupi"

4. **Navigacija po prijavi**
   - prijavi se kot `user@dsr.local`
   - preveri, da se prikaze povezava "Kosarica"

## 5) Zagon testov

```bash
cd 1_test

dotnet test 1_test.SeleniumTests
```

## 6) Opombe

- Chrome mora biti namecen.
- Ce testi padajo zaradi pocasne aplikacije, povecaj `ImplicitWait`.
- Selenium testi so pocasnejsi od unit testov, zato jih poganjamo manj pogosto.
