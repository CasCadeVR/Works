using CasCadeVR.Works.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace CasCadeVR.Works.Web.Tests.Infrastructures;

/// <summary>
/// Модифицированная фабрика для интеграционных тестов
/// </summary>
public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.CreateHost"/>
    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }

    /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.ConfigureWebHost"/>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestAppConfiguration();
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                typeof(DbContextOptions<WorksContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetValue<string>("ConnectionStrings:IntergrationConnection");
                var dbContextOptions = new DbContextOptions<WorksContext>(new Dictionary<Type, IDbContextOptionsExtension>());
                var optionsBuilder = new DbContextOptionsBuilder<WorksContext>(dbContextOptions)
                .UseApplicationServiceProvider(provider)
                .UseNpgsql(string.Format(connectionString!, Guid.NewGuid().ToString("N")));

                return optionsBuilder.Options;
            });
        });
    }
}