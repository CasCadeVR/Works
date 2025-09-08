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
    private readonly WorksApiFixture fixture;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ExecutorControllerTests"/>
    /// </summary>
    public ExecutorControllerTests(WorksApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
        this.fixture = fixture;
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="ExecutorController.GetById(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var executor = await fixture.SeedExampleExecutor();

        // Act
        var response = await webClient.ExecutorGETAsync(executor.Id);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(executor, options => options
               .Excluding(x => x.CreatedAt)
               .Excluding(x => x.UpdatedAt)
               .Excluding(x => x.DeletedAt)
               .WithMapping<Executor, ExecutorApiModel>(
            dest => dest.FullName,
            src => src.FullName
            )
               .WithMapping<Executor, ExecutorApiModel>(
            dest => dest.RegistrationNumber,
            src => src.RegistrationNumber
            )
           );
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="ExecutorController.GetAll(CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        for (int i = 0; i < 3; i++)
        {
            await fixture.SeedExampleExecutor();
        }

        await fixture.SeedExampleExecutor(withSoftDelete: true);

        // Act
        var response = await webClient.ExecutorAllAsync();

        // Assert
        response.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.FullName);
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="ExecutorController.Create(Models.Executors.ExecutorCreateRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task CreateShouldReturnValues()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<ExecutorCreateRequestApiModel>(x => x.RegistrationNumber = "1234567890124");

        // Act
        var response = await webClient.ExecutorPOSTAsync(request);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="ExecutorController.Update(Guid, Models.Executors.ExecutorCreateRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldReturnValues()
    {
        // Arrange
        var executor = await fixture.SeedExampleExecutor();

        var request = TestEntityProvider.Shared.Create<ExecutorCreateRequestApiModel>(x => x.RegistrationNumber = "1234567890123");

        // Act
        var response = await webClient.ExecutorPUTAsync(executor.Id, request);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="ExecutorController.Delete(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task DeleteShouldReturnValues()
    {
        // Arrange
        var executor = await fixture.SeedExampleExecutor();

        // Act
        await webClient.ExecutorDELETEAsync(executor.Id);

        // Assert
        context.Entry(executor).Reload();
        var newValue = context.Set<Executor>().Single(x => x.Id == executor.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
