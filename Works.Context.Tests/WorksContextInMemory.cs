using Works.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Works.Context.Tests;

/// <summary>
/// Класс <see cref="WorksContext"/> для тестов с базой в памяти. Один контекст на тест
/// </summary>
public abstract class WorksContextInMemory: IAsyncDisposable
{
    /// <summary>
    /// Контекст <see cref="WorksContext"/>
    /// </summary>
    protected WorksContext Context { get; }

    /// <inheritdoc cref="IUnitOfWork"/>
    protected IUnitOfWork UnitOfWork => Context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksContextInMemory"/>
    /// </summary>
    protected WorksContextInMemory()
    {
        var optionsBuilder = new DbContextOptionsBuilder<WorksContext>()
            .UseInMemoryDatabase($"WorksTests{Guid.NewGuid()}")
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

        Context = new WorksContext(optionsBuilder.Options);
    }

    /// <inheritdoc cref="IAsyncDisposable"/>
    public async ValueTask DisposeAsync()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.DisposeAsync();
    }
}