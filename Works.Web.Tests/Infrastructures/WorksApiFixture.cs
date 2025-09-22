using Ahatornn.TestGenerator;
using CasCadeVR.Works.Context;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Web.Tests.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CasCadeVR.Works.Web.Tests.Infrastructures;

/// <summary>
/// Модификация фикстуры для интеграционных тестов
/// </summary>
public class WorksApiFixture: IAsyncLifetime
{
    private readonly TestWebApplicationFactory factory;
    private WorksContext? context;

    /// <inheritdoc cref="IUnitOfWork"/>
    protected IUnitOfWork UnitOfWork => Context;

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

    internal IWorksApiClient WebClient
    {
        get
        {
            var client = factory.CreateClient();
            return new WorksApiClient(string.Empty, client);
        }
    }

    /// <summary>
    /// Создать в базе пример экземпляр <see cref="UnitOfMeasure"/>
    /// </summary>
    public async Task<UnitOfMeasure> SeedExampleUnitOfMeasure(bool withSoftDelete = false)
    {
        var unitOfMeasure = TestEntityProvider.Shared.Create<UnitOfMeasure>(x => x.Name = $"{nameof(x.Name)}{Guid.NewGuid()}");

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
        var customer = TestEntityProvider.Shared.Create<Customer>(x =>
            x.TaxPayerId = Random.Shared.NextInt64(1000000001, 9999999999).ToString());

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
        var executor = TestEntityProvider.Shared.Create<Executor>(x =>
           x.RegistrationNumber = Random.Shared.NextInt64(1000000000001, 9999999999999).ToString());

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
        var unitOfMeasure = TestEntityProvider.Shared.Create<UnitOfMeasure>(x => x.Name = $"{nameof(x.Name)}{Guid.NewGuid()}");
        var work = TestEntityProvider.Shared.Create<Work>(x =>
        {
            x.Name = $"{nameof(x.Name)}{Guid.NewGuid()}";
            x.UnitOfMeasureId = unitOfMeasure.Id;
        });

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

        var customer = TestEntityProvider.Shared.Create<Customer>(x =>
            x.TaxPayerId = Random.Shared.NextInt64(1000000001, 10000000000).ToString());

        var executor = TestEntityProvider.Shared.Create<Executor>(x =>
          x.RegistrationNumber = Random.Shared.NextInt64(1000000000001, 10000000000000).ToString());

        var unitOfMeasure = TestEntityProvider.Shared.Create<UnitOfMeasure>(x => x.Name = $"{nameof(x.Name)}_{Guid.NewGuid()}");

        var work = TestEntityProvider.Shared.Create<Work>(x =>
        {
            x.Name = $"{nameof(x.Name)}_{Guid.NewGuid()}";
            x.UnitOfMeasureId = unitOfMeasure.Id;
        });

        var actWork = TestEntityProvider.Shared.Create<ActWork>(x =>
        {
            x.ActId = id;
            x.Quantity = 5;
            x.CapturedPrice = work.Price;
            x.WorkId = work.Id;
        });

        var act = TestEntityProvider.Shared.Create<Act>(x =>
        {
            x.Id = id;
            x.Date = DateOnly.FromDateTime(DateTime.UtcNow);
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