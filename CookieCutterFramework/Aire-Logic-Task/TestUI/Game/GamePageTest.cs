namespace CookieCutterFramework.TestUI.Game
{
    public partial class GamePageTest : TestBaseUI
    {
        public GamePageTest() { }

        [Test]
        [Category("CI | Nightly")]
        public async Task ClickCookiesAndCountTotal()
        {
            await dashboardPage.CreateUserAndGameSession("Automated User5");
            await gamePage.VerifyGamePageExistsWithCorrectUser("Automated User5");
            await gamePage.ClickCookieBtnAndCountCookieTotal(17);
        }

        [Test]
        [Category("CI | Nightly")]
        public async Task SellANumberOfCookiesAndValidateTotal()
        {
            await dashboardPage.CreateUserAndGameSession("Automated User6");
            await gamePage.VerifyGamePageExistsWithCorrectUser("Automated User6");
            await gamePage.ClickCookieBtnAndCountCookieTotal(27);
            await gamePage.SellCookiesAndCountNewTotal(10);
        }

        [Test]
        [Category("CI | Nightly")]
        public async Task BuyFactoriesAndValidateTotal()
        {
            await dashboardPage.CreateUserAndGameSession("Automated User7");
            await gamePage.VerifyGamePageExistsWithCorrectUser("Automated User7");
            await gamePage.ClickCookieBtnAndCountCookieTotal(30);
            await gamePage.BuyFactoriesAndVerifyMoneyDeduction(2);
        }

        [Test]
        [Category("CD | Regression")]
        public async Task VerifyFactoryIncrementIsCorrect()
        {
            await dashboardPage.CreateUserAndGameSession("Automated User8");
            await gamePage.VerifyGamePageExistsWithCorrectUser("Automated User8");
            await gamePage.ClickCookieBtnAndCountCookieTotal(30);
            await gamePage.BuyFactoriesAndVerifyMoneyDeduction(1);
            await gamePage.VerifyFactoryIncrement(1);
        }
    }
}
