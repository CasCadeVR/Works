using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Works.Web.Tests;
 static internal class WebHostBuilderHelper
{
    public static void ConfigureTestAppConfiguration(this IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.integration.json");
            config.AddJsonFile(configPath).AddEnvironmentVariables();
        });
    }
}
