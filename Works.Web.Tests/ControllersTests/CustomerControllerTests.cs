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
/// Тесты сценариев <see cref="CustomerController"/>
/// </summary>
[Collection(nameof(WorksCollections))]
public class CustomerControllerTests
{
    private readonly IWorksApiClient webClient;
    private readonly WorksContext context;
    private readonly WorksApiFixture fixture;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CustomerControllerTests"/>
    /// </summary>
    public CustomerControllerTests(WorksApiFixture fixture)
    {
        webClient = fixture.WebClient;
        context = fixture.Context;
        this.fixture = fixture;
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="CustomerController.GetById(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValues()
    {
        // Arrange
        var customer = await fixture.SeedExampleCustomer();

        // Act
        var response = await webClient.CustomerGETAsync(customer.Id);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(customer, options => options
               .Excluding(x => x.CreatedAt)
               .Excluding(x => x.UpdatedAt)
               .Excluding(x => x.DeletedAt)
               .WithMapping<Customer, CustomerApiModel>(dest => dest.FullName, src => src.FullName)
               .WithMapping<Customer, CustomerApiModel>(dest => dest.TaxPayerId, src => src.TaxPayerId)
           );
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="CustomerController.GetAll(CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        for (int i = 0; i < 3; i++)
        {
            await fixture.SeedExampleCustomer();
        }

        await fixture.SeedExampleCustomer(withSoftDelete: true);

        // Act
        var response = await webClient.CustomerAllAsync();

        // Assert
        response.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.FullName);
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="CustomerController.Create(Models.Customers.CustomerCreateRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task CreateShouldReturnValues()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<CustomerCreateRequestApiModel>(x => x.TaxPayerId = "1234567890");

        // Act
        var response = await webClient.CustomerPOSTAsync(request);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="CustomerController.Update(Guid, Models.Customers.CustomerCreateRequestApiModel, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldReturnValues()
    {
        // Arrange
        var customer = await fixture.SeedExampleCustomer();

        var request = TestEntityProvider.Shared.Create<CustomerCreateRequestApiModel>(x => x.TaxPayerId = "1234567891");

        // Act
        var response = await webClient.CustomerPUTAsync(customer.Id, request);

        // Assert
        response.Should()
           .NotBeNull()
           .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Проверяет работоспособность <see cref="CustomerController.Delete(Guid, CancellationToken)"/>
    /// </summary>
    [Fact]
    public async Task DeleteShouldReturnValues()
    {
        // Arrange
        var customer = await fixture.SeedExampleCustomer();

        // Act
        await webClient.CustomerDELETEAsync(customer.Id);

        // Assert
        context.Entry(customer).Reload();
        var newValue = context.Set<Customer>().Single(x => x.Id == customer.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
