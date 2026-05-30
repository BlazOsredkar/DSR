using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace _1_test.SeleniumTests.Selenium;

public class LoginAndPurchaseTests : IDisposable
{
    private readonly IWebDriver _driver;
    private readonly string _baseUrl;

    public LoginAndPurchaseTests()
    {
        _baseUrl = Environment.GetEnvironmentVariable("APP_URL") ?? "http://localhost:5104";
        var headlessValue = Environment.GetEnvironmentVariable("HEADLESS");
        var useHeadless = string.IsNullOrWhiteSpace(headlessValue) || !headlessValue.Equals("false", StringComparison.OrdinalIgnoreCase);
        var options = new ChromeOptions();
        if (useHeadless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-gpu");
        }
        options.AddArgument("--window-size=1024,768");
        _driver = new ChromeDriver(options);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
    }

    [Fact]
    public void PravilnaPrijava_Admin_PrikazeAdminMeni()
    {
        Login("admin@dsr.local", "Admin123!");

        var adminLink = _driver.FindElements(By.LinkText("Upravljanje uporabnikov"));
        Assert.NotEmpty(adminLink);
    }

    [Fact]
    public void NepravilnaPrijava_PrikazeNapako()
    {
        _driver.Navigate().GoToUrl($"{_baseUrl}/Account/Login");

        _driver.FindElement(By.Name("Email")).SendKeys("admin@dsr.local");
        _driver.FindElement(By.Name("Password")).SendKeys("wrong");
        _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        var error = _driver.FindElements(By.CssSelector(".text-danger"));
        Assert.NotEmpty(error);
    }

    [Fact]
    public void DodajVKosarico_InZakljuciNakup()
    {
        Login("user@dsr.local", "User123!");

        _driver.Navigate().GoToUrl($"{_baseUrl}/Books");
        SafeClick(By.CssSelector("button.btn-outline-success"));

        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        wait.Until(driver => driver.Url!.Contains("/Purchases/Cart"));
        wait.Until(driver => driver.FindElements(By.CssSelector("table tbody tr")).Count > 0);

        SafeClick(By.LinkText("Na blagajno"));
        wait.Until(driver => driver.Url!.Contains("/Purchases/Checkout"));
        _driver.FindElement(By.Name("DeliveryAddress")).SendKeys("Testna ulica 1");
        _driver.FindElement(By.Name("ContactPhone")).SendKeys("041000000");
        SafeClick(By.CssSelector("form[action*='Checkout'] button[type='submit']"));

        var historyWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(12));
        var state = historyWait.Until(driver =>
        {
            var url = driver.Url ?? string.Empty;
            if (url.Contains("/Purchases/History"))
            {
                return "history";
            }

            if (url.Contains("/Account/Login"))
            {
                return "login";
            }

            var validation = driver.FindElements(By.CssSelector(".validation-summary-errors, .text-danger"));
            if (validation.Any(el => !string.IsNullOrWhiteSpace(el.Text)))
            {
                return "validation";
            }

            return null;
        });

        Assert.Equal("history", state);

        var title = _driver.FindElement(By.CssSelector("h1")).Text;
        Assert.Contains("Moji nakupi", title);
    }

    [Fact]
    public void Navigacija_PrikazeKosaricoZaPrijavljenega()
    {
        Login("user@dsr.local", "User123!");

        var cartLinks = _driver.FindElements(By.LinkText("Kosarica"));
        Assert.NotEmpty(cartLinks);
    }

    private void Login(string email, string password)
    {
        _driver.Navigate().GoToUrl($"{_baseUrl}/Account/Login");

        _driver.FindElement(By.Name("Email")).SendKeys(email);
        _driver.FindElement(By.Name("Password")).SendKeys(password);
        _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        wait.Until(driver => driver.Url!.Contains("/Home") || driver.Url!.EndsWith("/"));
    }

    private void SafeClick(By locator)
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        var element = wait.Until(driver =>
        {
            var candidate = driver.FindElement(locator);
            return candidate.Displayed && candidate.Enabled ? candidate : null;
        });

        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);

        try
        {
            element.Click();
        }
        catch (ElementClickInterceptedException)
        {
            // Fallback when a sticky element overlays the button.
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", element);
        }
    }

    public void Dispose()
    {
        _driver.Quit();
        _driver.Dispose();
    }
}
