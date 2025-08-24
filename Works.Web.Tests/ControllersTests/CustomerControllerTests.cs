using Ahatornn.TestGenerator;
using DocumentFormat.OpenXml.ExtendedProperties;
using FluentAssertions;
using CasCadeVR.Works.Context;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Web.Controllers;
using CasCadeVR.Works.Web.Tests.Client;
using CasCadeVR.Works.Web.Tests.Infrastructures;
using Xunit;

namespace CasCadeVR.Works.Web.Tests.ControllersTests;

/// <summary>
/// Тесты сценариев <see cref="CustomerController"/>
/// </summary>
[Collection(nameof(WorksCollections))]
public class CustomerControllerTests
{
    private readonly IWorksApiClient webClient;
    private readonly WorksContext context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CustomerControllerTests"/>
    /// </summary>
    public CustomerControllerTests(WorksApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
    }

    /// <summary>
    /// Провереят работоспособность <see cref="CustomerController.GetById(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValues()
    {
        // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>();
        await context.AddAsync(customer);
        await context.SaveChangesAsync();

        // Act
        var respone = await webClient.CustomerGETAsync(customer.Id);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(customer, options => options
               .Excluding(x => x.CreatedAt)
               .Excluding(x => x.UpdatedAt)
               .Excluding(x => x.DeletedAt)
               .WithMapping<Customer, CustomerApiModel>(dest => dest.FIO, src => src.Fio)
               .WithMapping<Customer, CustomerApiModel>(dest => dest.INN, src => src.Inn)
           );
    }

    /// <summary>
    /// Провереят работоспособность <see cref="CustomerController.GetAll(CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        var customer1 = TestEntityProvider.Shared.Create<Customer>(x => x.FIO = "Попов Александр Сергеевич");
        var customer2 = TestEntityProvider.Shared.Create<Customer>(x => x.FIO = "Иванов Иван Иванович");
        var customer3 = TestEntityProvider.Shared.Create<Customer>(x => x.FIO = "Каневская Мария Андреевна");
        var customer4 = TestEntityProvider.Shared.Create<Customer>(x =>
        {
            x.FIO = "Мизулин Константин Николаевич";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });

        await context.AddRangeAsync(customer1, customer2, customer3, customer4);
        await context.SaveChangesAsync();

        // Act
        var respone = await webClient.CustomerAllAsync();

        // Assert
        respone.Should()
            .NotBeEmpty()
            .And.ContainSingle(x => x.Id == customer1.Id)
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.Fio);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="CustomerController.Create(Contracts.Models.Customers.CustomerRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task CreateShouldReturnValues()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<CustomerRequestApiModel>(x =>
        {
            x.Fio = "Иван Иванов";
            x.Inn = "1234567890";
        });

        // Act
        var respone = await webClient.CustomerPOSTAsync(request);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="CustomerController.Update(Guid, Contracts.Models.Customers.CustomerRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldReturnValues()
    {
        // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>();
        await context.AddAsync(customer);
        await context.SaveChangesAsync();

        var request = TestEntityProvider.Shared.Create<CustomerRequestApiModel>(x =>
        {
            x.Fio = "Иван Иванов";
            x.Inn = "1234567890";
        });

        // Act
        var respone = await webClient.CustomerPUTAsync(customer.Id, request);

        // Assert
        respone.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Провереят работоспособность <see cref="CustomerController.Delete(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task DeleteShouldReturnValues()
    {
        // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>();
        await context.AddAsync(customer);
        await context.SaveChangesAsync();

        // Act
        await webClient.CustomerDELETEAsync(customer.Id);

        // Assert
        context.Entry(customer).Reload();
        var newValue = context.Set<Customer>().Single(x => x.Id == customer.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
