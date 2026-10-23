using CookieCutterFramework.Pages.Base;
using Microsoft.Playwright;

namespace CookieCutterFramework.Pages.Dashboard
{
    public partial class DashboardPage : BasePage
    {
        public DashboardPage(IPage page) : base(page) { }

        public async Task VerifyDashboardPageExists()
        {
            await DashboardHeader.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            Assert.That(await DashboardHeader.IsVisibleAsync(), Is.True);
        }

        public async Task VerifyHighScoreHeaderExists()
        {
            await HighScoresHeader.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            Assert.That(await HighScoresHeader.IsVisibleAsync(), Is.True);
        }

        public async Task VerifyHighScore(string playerName, int expectedScore)
        {
            await Assertions.Expect(PlayerScore(playerName)).ToHaveTextAsync(expectedScore.ToString());
        }

        public async Task CreateUserAndGameSession(string playerName)
        {
            await NameInputForm.FillAsync(playerName);
            await StartButton.ClickAsync();
        }
    }
}
