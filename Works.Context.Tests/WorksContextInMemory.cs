using Ahatornn.TestGenerator;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CasCadeVR.Works.Context.Tests;

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

    /// <summary>
    /// Создать в базе пример экземпляр <see cref="UnitOfMeasure"/>
    /// </summary>
    public async Task<UnitOfMeasure> SeedExampleUnitOfMeasure(bool withSoftDelete = false)
    {
        var unitOfMeasure = TestEntityProvider.Shared.Create<UnitOfMeasure>();

        if (withSoftDelete)
        {
            unitOfMeasure.DeletedAt = DateTime.UtcNow;
        }

        await Context.AddAsync(unitOfMeasure);
        await UnitOfWork.SaveChangesAsync();

        return unitOfMeasure;
    }

    /// <summary>
    /// Создать в базе пример экземпляр <see cref="Customer"/>
    /// </summary>
    public async Task<Customer> SeedExampleCustomer(bool withSoftDelete = false)
    {
        var customer = TestEntityProvider.Shared.Create<Customer>();

        if (withSoftDelete)
        {
            customer.DeletedAt = DateTime.UtcNow;
        }

        await Context.AddAsync(customer);
        await UnitOfWork.SaveChangesAsync();

        return customer;
    }

    /// <summary>
    /// Создать в базе пример экземпляр <see cref="Executor"/>
    /// </summary>
    public async Task<Executor> SeedExampleExecutor(bool withSoftDelete = false)
    {
        var executor = TestEntityProvider.Shared.Create<Executor>();

        if (withSoftDelete)
        {
            executor.DeletedAt = DateTime.UtcNow;
        }

        await Context.AddAsync(executor);
        await UnitOfWork.SaveChangesAsync();

        return executor;
    }

    /// <summary>
    /// Создать в базе пример экземпляр <see cref="Work"/>
    /// </summary>
    public async Task<Work> SeedExampleWork(bool withSoftDelete = false)
    {
        // Arrange
        var unitOfMeasure = TestEntityProvider.Shared.Create<UnitOfMeasure>();
        var work = TestEntityProvider.Shared.Create<Work>(x => x.UnitOfMeasureId = unitOfMeasure.Id);

        if (withSoftDelete)
        {
            work.DeletedAt = DateTime.UtcNow;
        }

        await Context.AddRangeAsync(unitOfMeasure, work);
        await UnitOfWork.SaveChangesAsync();

        return work;
    }

    /// <summary>
    /// Создать в базе пример экземпляр <see cref="Act"/>
    /// </summary>
    public async Task<Act> SeedExampleAct(bool withSoftDelete = false)
    {
        var id = Guid.NewGuid();

        var customer = TestEntityProvider.Shared.Create<Customer>();
        var executor = TestEntityProvider.Shared.Create<Executor>();
        var unitOfMeasure = TestEntityProvider.Shared.Create<UnitOfMeasure>();
        var work = TestEntityProvider.Shared.Create<Work>(x => x.UnitOfMeasureId = unitOfMeasure.Id);
        var actWork = TestEntityProvider.Shared.Create<ActWork>(x =>
        {
            x.ActId = id;
            x.WorkId = work.Id;
        });

        var act = TestEntityProvider.Shared.Create<Act>(x =>
        {
            x.Id = id;
            x.CustomerId = customer.Id;
            x.ExecutorId = executor.Id;
        });

        if (withSoftDelete)
        {
            act.DeletedAt = DateTime.UtcNow;
        }

        await Context.AddRangeAsync(customer, executor, unitOfMeasure, work, actWork, act);
        await UnitOfWork.SaveChangesAsync();

        act.ActWorks.First().Act = null!;

        return act;
    }

    /// <inheritdoc cref="IAsyncDisposable"/>
    public async ValueTask DisposeAsync()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.DisposeAsync();
    }
}