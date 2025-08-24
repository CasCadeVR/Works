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
/// Тесты сценариев <see cref="ActController"/>
/// </summary>
[Collection(nameof(WorksCollections))]
public class ActControllerTests
{
    private readonly IWorksApiClient webClient;
    private readonly WorksContext context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ActControllerTests"/>
    /// </summary>
    public ActControllerTests(WorksApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
    }

    /// <summary>
    /// Провереят работоспособность <see cref="ActController.GetById(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>(x => x.FIO = "1");
        var executor = TestEntityProvider.Shared.Create<Executor>(x => x.FIO = "2");
        var work = TestEntityProvider.Shared.Create<Work>(x => x.Name = "3");

        var act = TestEntityProvider.Shared.Create<Act>(x => 
        {
            x.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            x.Customer = customer;
            x.Executor = executor;
            x.Works = new List<ActWork>()
            { TestEntityProvider.Shared.Create<ActWork>(x => x.WorkId = work.Id) };
        });
        await context.AddRangeAsync(customer, executor, work, act);
        await context.SaveChangesAsync();

        // Act
        var result = await webClient.ActGETAsync(act.Id);

        var expectedresult = new ActApiModel
        {
            Id = act.Id,
            ActNumber = act.ActNumber,
            Date = act.Date,
            CustomerId = act.Customer.Id,
            CustomerFIO = act.Customer.FIO,
            CustomerOccupation = act.Customer.Occupation,
            CustomerFirm = act.Customer.Firm,
            CustomerINN = act.Customer.INN,
            ExecutorId = act.Executor.Id,
            ExecutorFIO = act.Executor.FIO,
            ExecutorOccupation = act.Executor.Occupation,
            ExecutorFirm = act.Executor.Firm,
            ExecutorOGRN = act.Executor.OGRN,
            Works = act.Works.Select(w => new ActWorksApiModel
            {
                WorkId = w.WorkId,
                WorkName = w.Work.Name,
                WorkDescription = w.Work.Description,
                WorkPrice = (double)w.Work.Price,
                WorkUnitOfMeasure = w.Work.UnitOfMeasure,
                ActualPrice = (double)w.ActualPrice,
                Quantity = w.Quantity,
                
            }).ToList(),
            Nds = (double)act.NDS,
        };

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(expectedresult, opt => opt
                .Excluding(x => x.Date));

        result.Date.Should().Be(expectedresult.Date);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="ActController.GetAll(CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>(x => x.FIO = "1");
        var executor = TestEntityProvider.Shared.Create<Executor>(x => x.FIO = "2");
        var work = TestEntityProvider.Shared.Create<Work>(x => x.Name = "3");

        var act1 = TestEntityProvider.Shared.Create<Act>(x =>
        {
            x.ActNumber = "1";
            x.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            x.Customer = customer;
            x.Executor = executor;
            x.Works = new List<ActWork>()
            { TestEntityProvider.Shared.Create<ActWork>(x => x.Work = work) };
        });

        var act2 = TestEntityProvider.Shared.Create<Act>(x =>
        {
            x.ActNumber = "2";
            x.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            x.Customer = customer;
            x.Executor = executor;
            x.Works = new List<ActWork>()
           { TestEntityProvider.Shared.Create<ActWork>(x => x.Work = work) };
        });

        var act3 = TestEntityProvider.Shared.Create<Act>(x =>
        {
            x.ActNumber = "3";
            x.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            x.Customer = customer;
            x.Executor = executor;
            x.Works = new List<ActWork>()
            { TestEntityProvider.Shared.Create<ActWork>(x => x.Work = work) };
        });

        var act4 = TestEntityProvider.Shared.Create<Act>(x =>
        {
            x.ActNumber = "4";
            x.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            x.Customer = customer;
            x.Executor = executor;
            x.Works = new List<ActWork>()
            { TestEntityProvider.Shared.Create<ActWork>(x => x.Work = work) };
            x.DeletedAt = DateTimeOffset.UtcNow;
        });

        await context.AddRangeAsync(customer, executor, work, act1, act2, act3, act4);
        await context.SaveChangesAsync();

        // Act
        var respone = await webClient.ActAllAsync();

        // Assert
        respone.Should()
           .NotBeEmpty()
           .And.ContainSingle(x => x.Id == act1.Id)
           .And.HaveCount(3)
           .And.BeInAscendingOrder(x => x.ActNumber);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="ActController.Create(Contracts.Models.Acts.ActRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task CreateShouldReturnValues()
    {
        // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>(x => x.FIO = "1");
        var executor = TestEntityProvider.Shared.Create<Executor>(x => x.FIO = "2");
        var work = TestEntityProvider.Shared.Create<Work>(x => x.Name = "3");

        await context.AddRangeAsync(customer, executor, work);
        await context.SaveChangesAsync();

        var request = TestEntityProvider.Shared.Create<ActRequestApiModel>(x =>
        {
            x.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            x.CustomerId = customer.Id;
            x.ExecutorId = executor.Id;
            x.Works = new List<ActWorksRequestApiModel>()
            { TestEntityProvider.Shared.Create<ActWorksRequestApiModel>(x => x.WorkId = work.Id) };
            x.Nds = 14.4;
        });

        // Act
        var respone = await webClient.ActPOSTAsync(request);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="ActController.Update(Guid, Contracts.Models.Acts.ActRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldReturnValues()
    {
       // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>(x => x.FIO = "1");
        var executor = TestEntityProvider.Shared.Create<Executor>(x => x.FIO = "2");
        var work = TestEntityProvider.Shared.Create<Work>(x => x.Name = "3");
        var act = TestEntityProvider.Shared.Create<Act>(x =>
        {
            x.ActNumber = "1";
            x.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            x.Customer = customer;
            x.Executor = executor;
            x.Works = new List<ActWork>()
            { TestEntityProvider.Shared.Create<ActWork>(x => x.Work = work) };
        });

        await context.AddRangeAsync(customer, executor, work, act);
        await context.SaveChangesAsync();

        var request = TestEntityProvider.Shared.Create<ActRequestApiModel>(x =>
        {
            x.ActNumber = "2";
            x.Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3));
            x.CustomerId = customer.Id;
            x.ExecutorId = executor.Id;
            x.Works = new List<ActWorksRequestApiModel>()
            { TestEntityProvider.Shared.Create<ActWorksRequestApiModel>(x =>
                {
                    x.WorkId = work.Id;
                    x.Quantity = 1;
                    x.ActualPrice = 10;
                }) 
            };
            x.Nds = 14.4;
        });

        // Act
        var respone = await webClient.ActPUTAsync(act.Id, request);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="ActController.Delete(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task DeleteShouldReturnValues()
    {
        // Arrange
        var act = TestEntityProvider.Shared.Create<Act>();
        await context.AddAsync(act);
        await context.SaveChangesAsync();

        // Act
        await webClient.ActDELETEAsync(act.Id);

        // Assert
        context.Entry(act).Reload();
        var newValue = context.Set<Act>().Single(x => x.Id == act.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
