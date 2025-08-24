using Ahatornn.TestGenerator;
using FluentAssertions;
using CasCadeVR.Works.Context;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Services.Contracts.Exceptions;
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

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksControllerTests"/>
    /// </summary>
    public WorksControllerTests(WorksApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
    }

    /// <summary>
    /// Провереят работоспособность <see cref="WorksController.GetById(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValues()
    {
        // Arrange
        var work = TestEntityProvider.Shared.Create<Work>();
        await context.AddAsync(work);
        await context.SaveChangesAsync();

        // Act
        var respone = await webClient.WorksGETAsync(work.Id);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(work, options => options
               .Excluding(x => x.CreatedAt)
               .Excluding(x => x.UpdatedAt)
               .Excluding(x => x.DeletedAt)
               );
    }

    /// <summary>
    /// Провереят работоспособность <see cref="WorksController.GetAll(CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        var work1 = TestEntityProvider.Shared.Create<Work>(x => x.Name = "1");
        var work2 = TestEntityProvider.Shared.Create<Work>(x => x.Name = "2");
        var work3 = TestEntityProvider.Shared.Create<Work>(x => x.Name = "3");
        var work4 = TestEntityProvider.Shared.Create<Work>(x =>
        {
            x.Name = "3";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });

        await context.AddRangeAsync(work1, work2, work3, work4);
        await context.SaveChangesAsync();

        // Act
        var respone = await webClient.WorksAllAsync();

        // Assert
        respone.Should()
            .NotBeEmpty()
            .And.ContainSingle(x => x.Id == work1.Id)
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.Name);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="WorksController.Create(Contracts.Models.Works.WorksRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task CreateShouldReturnValues()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<WorksRequestApiModel>();

        // Act
        var respone = await webClient.WorksPOSTAsync(request);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="WorksController.Update(Guid, Contracts.Models.Works.WorksRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldReturnValues()
    {
        // Arrange
        var work = TestEntityProvider.Shared.Create<Work>();
        await context.AddAsync(work);
        await context.SaveChangesAsync();

        var request = TestEntityProvider.Shared.Create<WorksRequestApiModel>();

        // Act
        var respone = await webClient.WorksPUTAsync(work.Id, request);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="WorksController.Delete(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task DeleteShouldReturnValues()
    {
        // Arrange
        var work = TestEntityProvider.Shared.Create<Work>();
        await context.AddAsync(work);
        await context.SaveChangesAsync();

        // Act
        await webClient.WorksDELETEAsync(work.Id);

        // Assert
        context.Entry(work).Reload();
        var newValue = context.Set<Work>().Single(x => x.Id == work.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
