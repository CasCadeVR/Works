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
/// Тесты сценариев <see cref="WorksController"/>
/// </summary>
[Collection(nameof(WorksCollections))]
public class WorksControllerTests
{
    private readonly IWorksApiClient webClient;
    private readonly WorksContext context;
    private readonly WorksApiFixture fixture;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksControllerTests"/>
    /// </summary>
    public WorksControllerTests(WorksApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
        this.fixture = fixture;
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="WorksController.GetById(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValues()
    {
        // Arrange
        var work = await fixture.SeedExampleWork();

        // Act
        var response = await webClient.WorksGETAsync(work.Id);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(work, options => options
               .Excluding(x => x.CreatedAt)
               .Excluding(x => x.UpdatedAt)
               .Excluding(x => x.DeletedAt)
               .Excluding(x => x.UnitOfMeasureId)
               .Excluding(x => x.UnitOfMeasure.CreatedAt)
               .Excluding(x => x.UnitOfMeasure.UpdatedAt)
               .Excluding(x => x.UnitOfMeasure.DeletedAt)
               );
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="WorksController.GetAll(CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        for (int i = 0; i < 3; i++)
        {
            await fixture.SeedExampleWork();
        }

        await fixture.SeedExampleWork(withSoftDelete: true);

        // Act
        var response = await webClient.WorksAllAsync();

        // Assert
        response.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.Name);
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="WorksController.Create(Models.Works.WorkCreateRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task CreateShouldReturnValues()
    {
        // Arrange
        var unitOfMeasure = await fixture.SeedExampleUnitOfMeasure();
        var request = TestEntityProvider.Shared.Create<WorkCreateRequestApiModel>(x => 
        {
            x.Price = 1000;
            x.UnitOfMeasureId = unitOfMeasure.Id;
        });

        // Act
        var response = await webClient.WorksPOSTAsync(request);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request, opt => opt.Excluding(x => x.UnitOfMeasureId));
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="WorksController.Update(Guid, Models.Works.WorkCreateRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldReturnValues()
    {
        // Arrange
        var work = await fixture.SeedExampleWork();

        var request = TestEntityProvider.Shared.Create<WorkCreateRequestApiModel>(x =>
        {
            x.Price = 1000;
            x.UnitOfMeasureId = work.UnitOfMeasureId;
        });

        // Act
        var response = await webClient.WorksPUTAsync(work.Id, request);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request, opt => opt.Excluding(x => x.UnitOfMeasureId));
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="WorksController.Delete(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task DeleteShouldReturnValues()
    {
        // Arrange
        var work = await fixture.SeedExampleWork();

        // Act
        await webClient.WorksDELETEAsync(work.Id);

        // Assert
        context.Entry(work).Reload();
        var newValue = context.Set<Work>().Single(x => x.Id == work.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
