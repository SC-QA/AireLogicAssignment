using CookieCutterFramework.Config;
using CookieCutterFramework.Pages.Dashboard;
using CookieCutterFramework.Pages.Game;
using Microsoft.Playwright;

namespace CookieCutterFramework.TestUI
{
    public class TestBaseUI : PageTest
    {
        public TestBaseUI() { }

        protected static TestSettings Settings => ConfigReader.Settings;

        public DashboardPage dashboardPage;
        public GamePage gamePage;

        [OneTimeSetUp]
        public void ConfigureBrowserMode()
        {
            if (Settings.Headed)
            {
                Environment.SetEnvironmentVariable("HEADED", "1");
            }
        }

        [SetUp]
        public async Task Setup()
        {
            dashboardPage = new DashboardPage(Page);
            gamePage = new GamePage(Page);

            await Page.GotoAsync(Settings.BaseUrl);
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            var options = new BrowserNewContextOptions
            {
                ColorScheme = ColorScheme.Dark,
                ViewportSize = new()
                {
                    Width = 1920,
                    Height = 1080
                }
            };

            return options;
        }
    }
}




