using CookieCutterFramework.Pages.Base;
using Microsoft.Playwright;
using System.Security.Cryptography.X509Certificates;

namespace CookieCutterFramework.Pages.Game
{
    public partial class GamePage : BasePage
    {
        public GamePage(IPage page) : base(page) { }

        public async Task NavigateToDashboardPage()
        {
            await GamePageHeader.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await GamePageHeader.ClickAsync();
        }

        public async Task VerifyGamePageExistsWithCorrectUser(string name)
        {
            await GamePageSecondaryHeader(name).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            Assert.That(await GamePageSecondaryHeader(name).IsVisibleAsync(), Is.True);
        }

        public async Task ClickCookieBtnAndCountCookieTotal(int clicks)
        {
            for (int i = 0; i < clicks; i++)
            {
                await ClickCookieBtn.ClickAsync();
            }

            await Assertions.Expect(CookiesTotal).ToHaveTextAsync(clicks.ToString());
            //var cookiesTotal = await CookiesTotal.InnerTextAsync();   //This commented out section had a race condition issue where the assertion would run before the cookies total was updated, causing the test to fail. 
            //Assert.That(cookiesTotal, Is.EqualTo(clicks.ToString())); //The above line uses Playwright's built-in assertion to wait for the text to match the expected value instead.
        }

        public async Task SellCookiesAndCountNewTotal(int cookiesToSell)
        {
            var cookiesBefore = int.Parse(await CookiesTotal.InnerTextAsync());
            var expectedTotal = cookiesBefore - cookiesToSell;

            var moneyBefore = await GetMoneyAsync();
            var cookieValue = 0.25m;
            var expectedMoney = moneyBefore + (cookiesToSell * cookieValue);

            await SellCookiesInput.FillAsync(cookiesToSell.ToString());
            await SellCookiesBtn.ClickAsync();

            await Assertions.Expect(CookiesTotal).ToHaveTextAsync(expectedTotal.ToString());
            await ExpectMoneyAsync(expectedMoney);
        }

        private async Task<decimal> GetMoneyAsync()
        {
            var text = await MoneyTotal.InnerTextAsync();
            return decimal.Parse(text.Trim(), System.Globalization.CultureInfo.InvariantCulture); //This is to ensure that the decimal point is correctly interpreted regardless of the system's culture settings.
        }

        private Task ExpectMoneyAsync(decimal expected)
        {
            var text = expected.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture);
            var trailing = text.Contains('.') ? "0*" : @"(\.0*)?";
            return Assertions.Expect(MoneyTotal).ToHaveTextAsync(
                new System.Text.RegularExpressions.Regex($"^{System.Text.RegularExpressions.Regex.Escape(text)}{trailing}$")); //This regex allows for trailing zeros after the decimal point, which can occur due to floating-point arithmetic.
        }

        public async Task BuyFactoriesAndVerifyMoneyDeduction(int factoriesToBuy)
        {
            var moneyBefore = await GetMoneyAsync();
            var factoryCost = 3;
            var expectedMoney = moneyBefore - (factoriesToBuy * factoryCost);

            await BuyFactoriesInput.FillAsync(factoriesToBuy.ToString());
            await BuyFactoriesBtn.ClickAsync();
            await ExpectMoneyAsync(expectedMoney);
        }

        public async Task VerifyFactoryIncrement(int factories, int secondsToObserve = 3)
        {
            // Sample the live counter and record each distinct value, rather than asserting on a moving target.
            var observed = new List<int> { int.Parse(await CookiesTotal.InnerTextAsync()) };
            var deadline = DateTime.UtcNow.AddSeconds(secondsToObserve + 2);

            while (DateTime.UtcNow < deadline && observed.Count <= secondsToObserve)
            {
                var value = int.Parse(await CookiesTotal.InnerTextAsync());
                if (value != observed[^1])
                {
                    observed.Add(value);
                }
                await Task.Delay(100);
            }

            Assert.That(observed.Count, Is.GreaterThan(1), "Cookie total never changed.");

            for (int i = 1; i < observed.Count; i++)
            {
                var delta = observed[i] - observed[i - 1];
                Assert.That(delta, Is.EqualTo(factories),
                    $"Expected +{factories} per second but saw +{delta}. Samples: {string.Join(", ", observed)}");
            }
        }

        public async Task<int> CaptureCookieValue()
        {
            return int.Parse(await CookiesTotal.InnerTextAsync());
        }
    }
}
