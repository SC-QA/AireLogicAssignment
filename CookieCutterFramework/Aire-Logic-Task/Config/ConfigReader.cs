using Microsoft.Extensions.Configuration;

namespace CookieCutterFramework.Config
{
    public static class ConfigReader
    {
        private static readonly Lazy<TestSettings> _settings = new(Load);

        public static TestSettings Settings => _settings.Value;

        private static TestSettings Load()
        {
            var basePath = AppContext.BaseDirectory;

            var environmentConfig = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("environment.json", optional: true)
                .Build();

            var environment = environmentConfig["Environment"];

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false);

            if (!string.IsNullOrWhiteSpace(environment))
            {
                builder.AddJsonFile($"appsettings.{environment}.json", optional: false);
            }

            var configuration = builder
                .AddEnvironmentVariables()
                .Build();

            return configuration.GetSection("Settings").Get<TestSettings>() ?? new TestSettings();
        }
    }
}
