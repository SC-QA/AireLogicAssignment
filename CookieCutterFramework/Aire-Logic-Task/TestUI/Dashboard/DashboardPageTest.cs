namespace CookieCutterFramework.TestUI.Dashboard
{
    [TestFixture]
    public partial class DashboardPageTest : TestBaseUI
    {
        [Test]
        [Category("SmokeTest | Release")]
        public async Task VerifyDashboardPageExists()
        {
            await dashboardPage.VerifyDashboardPageExists();
        }

        [Test]
        [Category("SmokeTest | Release")]
        public async Task VerifyHighScoreHeaderExists()
        {
            await dashboardPage.VerifyHighScoreHeaderExists();
        }

        [Test]
        [Category("CI | Nightly")]
        public async Task CreateNewGameWithPlayerAndVerifyGameState()
        {
            await dashboardPage.CreateUserAndGameSession("Automated User2"); 
            await gamePage.VerifyGamePageExistsWithCorrectUser("Automated User2");
        }

        [Test]
        [Category("CI | Nightly")]
        public async Task CreateNewGameWithPlayerAndVerifyHighScore()
        {
            await dashboardPage.CreateUserAndGameSession("What Is My Highscore");
            await gamePage.VerifyGamePageExistsWithCorrectUser("What Is My Highscore");
            await gamePage.ClickCookieBtnAndCountCookieTotal(20);

            var cookiesCount = await gamePage.CaptureCookieValue();

            await gamePage.NavigateToDashboardPage();
            await dashboardPage.VerifyHighScore("What Is My Highscore", cookiesCount);
        }
    }
}
