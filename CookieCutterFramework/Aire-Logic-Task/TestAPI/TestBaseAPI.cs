using CookieCutterFramework.Endpoints.Game;
using CookieCutterFramework.Config;
using Microsoft.Playwright;

namespace CookieCutterFramework.TestAPI
{
    [Category("API")]
    public class TestBaseAPI : PlaywrightTest
    {
        protected static TestSettings Settings => ConfigReader.Settings;

        protected IAPIRequestContext Request = null!;
        protected GameEndpoint gameAPI = null!;

        [SetUp]
        public async Task SetupApi()
        {
            Request = await Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = Settings.BaseUrl,
                ExtraHTTPHeaders = new Dictionary<string, string> { ["Accept"] = "application/json" }
            });

            gameAPI = new GameEndpoint(Request);
        }

        [TearDown]
        public async Task TearDownApi()
        {
            await Request.DisposeAsync();
        }
    }
}
