using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Works.Context;
using Xunit;

namespace Works.Web.Tests.Infrastructures;

/// <summary>
/// Модификация фикстуры для интеграционных тестов
/// </summary>
public class WorksApiFixture: IAsyncLifetime
{
    private readonly TestWebApplicationFactory factory;
    private WorksContext? context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksApiFixture"/>
    /// </summary>
    public WorksApiFixture()
    {
        factory = new TestWebApplicationFactory();
    }

    internal WorksContext Context
    {
        get
        {
            if (context != null)
            {
                return context;
            }

            var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            context = scope.ServiceProvider.GetRequiredService<WorksContext>();
            return context;
        }
    }

    /// <inheritdoc cref="IAsyncLifetime.InitializeAsync"/>
    Task IAsyncLifetime.InitializeAsync() => Context.Database.MigrateAsync();

    /// <inheritdoc cref="IAsyncLifetime.DisposeAsync"/>
    async Task IAsyncLifetime.DisposeAsync()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.Database.CloseConnectionAsync();
        await Context.DisposeAsync();
        await factory.DisposeAsync();
    }
}