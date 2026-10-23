namespace CookieCutterFramework.TestAPI.GamePage
{
    public class GamePageAPITests : TestBaseAPI
    {
        private string player = string.Empty;

        [SetUp]
        public async Task CreatePlayer()
        {
            player = $"ApiUser{Guid.NewGuid():N}"[..16];
            await gameAPI.CreateGameAsync(player);
        }

        [Test]
        [Retry(3)]
        public async Task ReturnsCorrectGameState()
        {
            var status = await gameAPI.GetStatusAsync(player);

            Assert.Multiple(() =>
            {
                Assert.That(status.Cookies, Is.EqualTo(0));
                Assert.That(status.Money, Is.EqualTo(0));
                Assert.That(status.Factories, Is.EqualTo(0));
            });
        }

        [Test]
        [Retry(3)]
        public async Task ClickAndIncrementsCookies()
        {
            await gameAPI.ClickAsync(player);
            var click = await gameAPI.ClickAsync(player);
            var status = await gameAPI.GetStatusAsync(player);

            Assert.Multiple(() =>
            {
                Assert.That(click, Is.EqualTo(2));
                Assert.That(status.Cookies, Is.EqualTo(2));
            });
        }

        [Test]
        [Retry(3)]
        public async Task SellCookiesAndAddsMoney()
        {
            for (int i = 0; i < 5; i++)
            {
                await gameAPI.ClickAsync(player);
            }

            await gameAPI.SellCookiesAsync(player, 4);
            var status = await gameAPI.GetStatusAsync(player);

            Assert.Multiple(() =>
            {
                Assert.That(status.Cookies, Is.EqualTo(1));
                Assert.That(status.Money, Is.EqualTo(1.0).Within(0.001));
            });
        }

        [Test]
        [Retry(3)]
        public async Task BuyFactoriesAndDeductMoney()
        {
            for (int i = 0; i < 13; i++)
            {
                await gameAPI.ClickAsync(player);
            }

            await gameAPI.SellCookiesAsync(player, 12);

            await gameAPI.BuyFactoriesAsync(player, 1);
            var status = await gameAPI.GetStatusAsync(player);

            Assert.Multiple(() =>
            {
                Assert.That(status.Factories, Is.EqualTo(1));
                Assert.That(status.Money, Is.EqualTo(0).Within(0.001));
            });
        }
    }
}
