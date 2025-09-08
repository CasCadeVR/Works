using Ahatornn.TestGenerator;
using CasCadeVR.Works.Context;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Web.Controllers;
using CasCadeVR.Works.Web.Tests.Client;
using CasCadeVR.Works.Web.Tests.Infrastructures;
using FluentAssertions;
using Xunit;

namespace CasCadeVR.Works.Web.Tests.ControllersTests;

/// <summary>
/// Тесты сценариев <see cref="ActController"/>
/// </summary>
[Collection(nameof(WorksCollections))]
public class ActControllerTests
{
    private readonly IWorksApiClient webClient;
    private readonly WorksContext context;
    private readonly WorksApiFixture fixture;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ActControllerTests"/>
    /// </summary>
    public ActControllerTests(WorksApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
        this.fixture = fixture;
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="ActController.GetById(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var act = await fixture.SeedExampleAct();

        // Act
        var result = await webClient.ActGETAsync(act.Id);

        var expectedResult = new ActApiModel
        {
            Id = act.Id,
            ActNumber = act.ActNumber,
            Date = new DateTimeOffset(act.Date.ToDateTime(TimeOnly.MinValue), TimeSpan.FromHours(3)),
            Customer = new CustomerApiModel
            {
                Id = act.Customer.Id,
                FullName = act.Customer.FullName,
                Occupation = act.Customer.Occupation,
                Firm = act.Customer.Firm,
                TaxPayerId = act.Customer.TaxPayerId,
            },
            Executor = new ExecutorApiModel
            {
                Id = act.Executor.Id,
                FullName = act.Executor.FullName,
                Occupation = act.Executor.Occupation,
                Firm = act.Executor.Firm,
                RegistrationNumber = act.Executor.RegistrationNumber,
            },
            ActWorks = act.ActWorks.Select(y => new ActWorksApiModel
            {
                Quantity = y.Quantity,
                Work = new WorkApiModel
                {
                    Id = y.Work.Id,
                    Name = y.Work.Name,
                    Description = y.Work.Description,
                    Price = (double)y.Work.Price,
                    UnitOfMeasure = new UnitOfMeasureApiModel
                    {
                        Id = y.Work.UnitOfMeasure.Id,
                        Name = y.Work.UnitOfMeasure.Name,
                    },
                },
            }).ToList(),
        };

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(expectedResult, opt => opt
                .Excluding(x => x.Date));

        result.Date.ToLocalTime().Should().Be(expectedResult.Date.ToLocalTime());
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="ActController.GetAll(CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        for (int i = 0; i < 3; i++)
        {
            await fixture.SeedExampleAct();
        }

        await fixture.SeedExampleAct(withSoftDelete: true);

        // Act
        var response = await webClient.ActAllAsync();

        // Assert
        response.Should()
           .NotBeEmpty()
           .And.HaveCount(3)
           .And.BeInAscendingOrder(x => x.Date);
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="ActController.Create(Models.Acts.ActCreateRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task CreateShouldReturnValues()
    {
        // Arrange
        var customer = await fixture.SeedExampleCustomer();
        var executor = await fixture.SeedExampleExecutor();
        var work = await fixture.SeedExampleWork();

        var request = TestEntityProvider.Shared.Create<ActCreateRequestApiModel>(x =>
        {
            x.Date = DateTime.UtcNow;
            x.CustomerId = customer.Id;
            x.ExecutorId = executor.Id;
            x.ActWorks = [TestEntityProvider.Shared.Create<ActWorksCreateRequestApiModel>(y =>
            {
                y.Quantity = 5;
                y.WorkId = work.Id;
            })];
        });

        var requestActWorks = request.ActWorks!.First();

        // Act
        var response = await webClient.ActPOSTAsync(request);
        var responseActWorks = response.ActWorks!.First();

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request, opt => opt
                .Excluding(x => x.Date)
                .Excluding(x => x.ExecutorId)
                .Excluding(x => x.CustomerId)
                .Excluding(x => x.ActWorks)
                );

        response.Date.Date.Should().Be(request.Date.Date);

        responseActWorks
            .Should()
            .NotBeNull()
            .And.BeEquivalentTo(requestActWorks, opt => opt.Excluding(x => x.WorkId));
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="ActController.Update(Guid, Models.Acts.ActCreateRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldReturnValues()
    {
        // Arrange
        var work = await fixture.SeedExampleWork();
        var act = await fixture.SeedExampleAct();

        var request = TestEntityProvider.Shared.Create<ActCreateRequestApiModel>(x =>
        {
            x.ActNumber = "2";
            x.Date = DateTime.UtcNow;
            x.CustomerId = act.CustomerId;
            x.ExecutorId = act.ExecutorId;
            x.ActWorks = [TestEntityProvider.Shared.Create<ActWorksCreateRequestApiModel>(y =>
                {
                    y.Quantity = 6;
                    y.WorkId = work.Id;
                })];
        });
        var requestActWorks = request.ActWorks!.First();


        // Act
        var response = await webClient.ActPUTAsync(act.Id, request);
        var responseActWorks = response.ActWorks!.First();

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request, opt => opt
                .Excluding(x => x.Date)
                .Excluding(x => x.ExecutorId)
                .Excluding(x => x.CustomerId)
                .Excluding(x => x.ActWorks)
                );

        response.Date.Date.Should().Be(request.Date.Date);

        responseActWorks
            .Should()
            .NotBeNull()
            .And.BeEquivalentTo(requestActWorks, opt => opt.Excluding(x => x.WorkId));
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="ActController.Delete(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task DeleteShouldReturnValues()
    {
        // Arrange
        var act = await fixture.SeedExampleAct();

        // Act
        await webClient.ActDELETEAsync(act.Id);

        // Assert
        var newValue = context.Set<Act>().Single(x => x.Id == act.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
