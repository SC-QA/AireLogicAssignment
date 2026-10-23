using Microsoft.Playwright;
using System.Text.Json;

namespace CookieCutterFramework.Endpoints.Game
{
    public class GameEndpoint
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

        private readonly IAPIRequestContext _request;

        public GameEndpoint(IAPIRequestContext request) => _request = request;

        public Task<IAPIResponse> CreateGameAsync(string name) =>
            _request.PostAsync("new_game", new APIRequestContextOptions
            {
                Headers = new Dictionary<string, string> { ["Content-Type"] = "application/x-www-form-urlencoded" },
                DataString = $"name={Uri.EscapeDataString(name)}",
                MaxRedirects = 0
            });

        public Task<GameStatus> GetStatusAsync(string player) => CallAsync<GameStatus>(player, "status");

        public Task<double> ClickAsync(string player) => CallAsync<double>(player, "click");

        public Task SellCookiesAsync(string player, double count) => CallAsync<object>(player, "sell_cookies", count);

        public Task BuyFactoriesAsync(string player, double count) => CallAsync<object>(player, "buy_factories", count);

        private async Task<T> CallAsync<T>(string player, string method, params object[] args)
        {
            var response = await _request.PostAsync($"rpc/{Uri.EscapeDataString(player)}/", new APIRequestContextOptions
            {
                DataObject = new { jsonrpc = "2.0", method, @params = args, id = 1 }
            });

            using var doc = JsonDocument.Parse(await response.TextAsync());
            var root = doc.RootElement;

            if (root.GetProperty("error").ValueKind == JsonValueKind.String)
            {
                throw new InvalidOperationException($"RPC '{method}' failed: {root.GetProperty("error").GetString()}");
            }

            return root.GetProperty("result").Deserialize<T>(JsonOptions)!;
        }
    }
}
