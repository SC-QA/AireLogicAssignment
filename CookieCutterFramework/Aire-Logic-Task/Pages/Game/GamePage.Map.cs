using Microsoft.Playwright;

namespace CookieCutterFramework.Pages.Game
{
    public partial class GamePage
    {
        private ILocator GamePageHeader => _page.Locator("//a[normalize-space()='Cookie Clicker!']");
        private ILocator GamePageSecondaryHeader (string name) => _page.GetByText($"Hello {name}", new() { Exact = true });
        private ILocator CookiesTotal => _page.Locator("#cookies");
        private ILocator FactoriesTotal => _page.Locator("#factories");
        private ILocator MoneyTotal => _page.Locator("#money");
        private ILocator ClickCookieBtn => _page.GetByRole(AriaRole.Button, new() { Name = "Click Cookie!" });
        private ILocator SellCookiesInput => _page.Locator("#cookies-to-sell");
        private ILocator SellCookiesBtn => _page.GetByRole(AriaRole.Button, new() { Name = "Sell Cookies!" });
        private ILocator BuyFactoriesInput => _page.Locator("#factories-to-buy");
        private ILocator BuyFactoriesBtn => _page.GetByRole(AriaRole.Button, new() { Name = "Buy Factories!" });
    }
}
