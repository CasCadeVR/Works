using Microsoft.AspNetCore.Hosting;

namespace CasCadeVR.Works.Web.Tests;

static internal class WebHostBuilderHelper
{
    public static void ConfigureTestAppConfiguration(this IWebHostBuilder builder)
    {
        builder.UseEnvironment(EnviromentProvider.IntegrationEnviroment);
    }
}
