using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CasCadeVR.Works.Web.Tests;

static internal class WebHostBuilderHelper
{
    public static void ConfigureTestAppConfiguration(this IWebHostBuilder builder)
    {
        var webAssembly = Assembly.GetAssembly(typeof(Program))
            ?? throw new InvalidOperationException("Не удалось найти сборку основного приложения.");

        var projectDir = Path.GetDirectoryName(webAssembly.Location)!;

        var configPath = Path.Combine(projectDir, "appsettings.json");

        var tempConfig = new ConfigurationBuilder()
            .AddJsonFile(configPath)
            .Build();

        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddJsonFile(configPath).AddEnvironmentVariables();
        });

        var environment = tempConfig["Enviroments:IntegrationEnviroment"] ?? "Development";
        builder.UseEnvironment(environment);
    }
}
