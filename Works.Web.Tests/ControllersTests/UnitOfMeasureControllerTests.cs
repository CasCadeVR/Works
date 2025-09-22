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
/// Тесты сценариев <see cref="UnitOfMeasureControllerTests"/>
/// </summary>
[Collection(nameof(WorksCollections))]
public class UnitOfMeasureControllerTests
{
    private readonly IWorksApiClient webClient;
    private readonly WorksContext context;
    private readonly WorksApiFixture fixture;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UnitOfMeasureControllerTests"/>
    /// </summary>
    public UnitOfMeasureControllerTests(WorksApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
        this.fixture = fixture;
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="UnitOfMeasureController.GetById(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var unitOfMeasure = await fixture.SeedExampleUnitOfMeasure();

        // Act
        var response = await webClient.UnitOfMeasureGETAsync(unitOfMeasure.Id);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(unitOfMeasure, options => options
               .Excluding(x => x.CreatedAt)
               .Excluding(x => x.UpdatedAt)
               .Excluding(x => x.DeletedAt)
               .WithMapping<UnitOfMeasure, UnitOfMeasureApiModel>(
            dest => dest.Name,
            src => src.Name
            )
           );
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="UnitOfMeasureController.GetAll(CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        var examples = new List<UnitOfMeasure>();
        for (int i = 0; i < 3; i++)
        {
            examples.Add(await fixture.SeedExampleUnitOfMeasure());
        }

        await fixture.SeedExampleUnitOfMeasure(withSoftDelete: true);

        // Act
        var response = await webClient.UnitOfMeasureAllAsync();

        // Assert
        response.Should().NotBeEmpty();
        foreach (var example in examples)
        {
            response.Should().ContainEquivalentOf(example, opt => opt.ExcludingMissingMembers());
        }
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="UnitOfMeasureController.Create(Models.UnitOfMeasure.UnitOfMeasureCreateRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task CreateShouldReturnValues()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<UnitOfMeasureCreateRequestApiModel>(x => x.Name = "см.");

        // Act
        var response = await webClient.UnitOfMeasurePOSTAsync(request);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="UnitOfMeasureController.Update(Guid, Models.UnitOfMeasure.UnitOfMeasureCreateRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldReturnValues()
    {
        // Arrange
        var unitOfMeasure = await fixture.SeedExampleUnitOfMeasure();

        var request = TestEntityProvider.Shared.Create<UnitOfMeasureCreateRequestApiModel>();

        // Act
        var response = await webClient.UnitOfMeasurePUTAsync(unitOfMeasure.Id, request);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="UnitOfMeasureController.Delete(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task DeleteShouldReturnValues()
    {
        // Arrange
        var unitOfMeasure = await fixture.SeedExampleUnitOfMeasure();

        // Act
        await webClient.UnitOfMeasureDELETEAsync(unitOfMeasure.Id);

        // Assert
        var newValue = context.Set<UnitOfMeasure>().Single(x => x.Id == unitOfMeasure.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
