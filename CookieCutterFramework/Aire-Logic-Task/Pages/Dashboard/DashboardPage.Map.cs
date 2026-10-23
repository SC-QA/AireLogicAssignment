using Microsoft.Playwright;

namespace CookieCutterFramework.Pages.Dashboard
{
    public partial class DashboardPage
    {
        private ILocator DashboardHeader => _page.Locator("//h1[normalize-space()='Cookie Clicker!']");
        private ILocator NameInputForm => _page.Locator("//input[@name='name']");
        private ILocator StartButton => _page.GetByRole(AriaRole.Button, new() { Name = "Start!" });
        private ILocator HighScoresHeader => _page.Locator("//h2[normalize-space()='High Scores']");
        private ILocator Player(string name) => _page.Locator($"//tbody//td[a='{name}']");
        private ILocator PlayerScore(string name) => _page.Locator($"//tbody//tr[td/a[normalize-space()='{name}']]/td[2]");
    }
}
