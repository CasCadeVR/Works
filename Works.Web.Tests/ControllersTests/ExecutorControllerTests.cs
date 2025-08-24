using Ahatornn.TestGenerator;
using FluentAssertions;
using CasCadeVR.Works.Context;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Web.Controllers;
using CasCadeVR.Works.Web.Tests.Client;
using CasCadeVR.Works.Web.Tests.Infrastructures;
using Xunit;

namespace CasCadeVR.Works.Web.Tests.ControllersTests;

/// <summary>
/// Тесты сценариев <see cref="ExecutorController"/>
/// </summary>
[Collection(nameof(WorksCollections))]
public class ExecutorControllerTests
{
    private readonly IWorksApiClient webClient;
    private readonly WorksContext context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ExecutorControllerTests"/>
    /// </summary>
    public ExecutorControllerTests(WorksApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
    }

    /// <summary>
    /// Провереят работоспособность <see cref="ExecutorController.GetById(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var executor = TestEntityProvider.Shared.Create<Executor>();
        await context.AddAsync(executor);
        await context.SaveChangesAsync();

        // Act
        var respone = await webClient.ExecutorGETAsync(executor.Id);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(executor, options => options
               .Excluding(x => x.CreatedAt)
               .Excluding(x => x.UpdatedAt)
               .Excluding(x => x.DeletedAt)
               .WithMapping<Executor, ExecutorApiModel>(
            dest => dest.FIO,
            src => src.Fio
            )
               .WithMapping<Executor, ExecutorApiModel>(
            dest => dest.OGRN,
            src => src.Ogrn
            )
           );
    }

    /// <summary>
    /// Провереят работоспособность <see cref="ExecutorController.GetAll(CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        var executor1 = TestEntityProvider.Shared.Create<Executor>(x => x.FIO = "Попов Александр Сергеевич");
        var executor2 = TestEntityProvider.Shared.Create<Executor>(x => x.FIO = "Иванов Иван Иванович");
        var executor3 = TestEntityProvider.Shared.Create<Executor>(x => x.FIO = "Каневская Мария Андреевна");
        var executor4 = TestEntityProvider.Shared.Create<Executor>(x =>
        {
            x.FIO = "Мизулин Константин Николаевич";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });

        await context.AddRangeAsync(executor1, executor2, executor3, executor4);
        await context.SaveChangesAsync();

        // Act
        var respone = await webClient.ExecutorAllAsync();

        // Assert
        respone.Should()
            .NotBeEmpty()
            .And.ContainSingle(x => x.Id == executor1.Id)
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.Fio);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="ExecutorController.Create(Contracts.Models.Executors.ExecutorRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task CreateShouldReturnValues()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<ExecutorRequestApiModel>(x =>
        {
            x.Fio = "Иван Иванов";
            x.Ogrn = "1234567890123";
        });

        // Act
        var respone = await webClient.ExecutorPOSTAsync(request);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="ExecutorController.Update(Guid, Contracts.Models.Executors.ExecutorRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldReturnValues()
    {
        // Arrange
        var executor = TestEntityProvider.Shared.Create<Executor>();
        await context.AddAsync(executor);
        await context.SaveChangesAsync();

        var request = TestEntityProvider.Shared.Create<ExecutorRequestApiModel>(x =>
        {
            x.Fio = "Сергей Иванов";
            x.Ogrn = "1234567890123";
        });

        // Act
        var respone = await webClient.ExecutorPUTAsync(executor.Id, request);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="ExecutorController.Delete(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task DeleteShouldReturnValues()
    {
        // Arrange
        var executor = TestEntityProvider.Shared.Create<Executor>();
        await context.AddAsync(executor);
        await context.SaveChangesAsync();

        // Act
        await webClient.ExecutorDELETEAsync(executor.Id);

        // Assert
        context.Entry(executor).Reload();
        var newValue = context.Set<Executor>().Single(x => x.Id == executor.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
