using CasCadeVR.Works.Context.Tests;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace CasCadeVR.Works.Repository.Tests;

/// <summary>
/// Тесты на <see cref="CustomerReadRepository"/>
/// </summary>
public class CustomerReadRepositoryTests : WorksContextInMemory
{
    private readonly ICustomerReadRepository customerReadRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CustomerReadRepositoryTests"/>
    /// </summary>
    public CustomerReadRepositoryTests()
    {
        customerReadRepository = new CustomerReadRepository(Context);
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNull()
    {
        // Arrange
        await SeedExampleCustomer();
        var id = Guid.NewGuid();

        // Act
        var result = await customerReadRepository.GetById(id, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    /// <summary>
    /// Проверяет, что GetById вернёт <see cref="Customer"/> при его добавлении 
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var customer = await SeedExampleCustomer();

        // Act
        var result = await customerReadRepository.GetById(customer.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(customer);
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNullByDelete()
    {
        // Arrange
        var customer = await SeedExampleCustomer(withSoftDelete: true);

        // Act
        var result = await customerReadRepository.GetById(customer.Id, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    /// <summary>
    /// Проверяет, что GetAll вернёт пустой список
    /// </summary>
    [Fact]
    public async Task GetAllShouldBeEmpty()
    {
        // Act
        var result = await customerReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Проверяет, что GetAll вернёт список при добавлении нескольких <see cref="Customer"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        for (int i = 0; i < 3; i++)
        {
            await SeedExampleCustomer();
        }

        await SeedExampleCustomer(withSoftDelete: true);

        // Act
        var result = await customerReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.FullName);
    }
}