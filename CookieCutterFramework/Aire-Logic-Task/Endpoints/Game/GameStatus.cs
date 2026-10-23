using System.Text.Json.Serialization;

namespace CookieCutterFramework.Endpoints.Game
{
    public class GameStatus
    {
        [JsonPropertyName("cookies")]
        public double Cookies { get; set; }

        [JsonPropertyName("factories")]
        public double Factories { get; set; }

        [JsonPropertyName("money")]
        public double Money { get; set; }
    }
}
