using FluentAssertions;
using Works.Context.Tests;
using Xunit;
using Ahatornn.TestGenerator;
using Works.Entities;
using Works.Repository.Contracts.IReadRepositories;
using Works.Repository.ReadRepositories;

namespace Works.Repository.Tests;

/// <summary>
/// Тесты на <see cref="CustomerReadRepository"/>
/// </summary>
public class CustomerReadRepositoryTests : WorksContextInMemory
{
    private readonly ICustomerReadRepository customerReadRepository;

    /// <summary>
    /// ctor
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
        var customer = TestEntityProvider.Shared.Create<Customer>();
        await Context.AddAsync(customer);
        await UnitOfWork.SaveChangesAsync();

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
        var customer = TestEntityProvider.Shared.Create<Customer>(x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddAsync(customer);
        await UnitOfWork.SaveChangesAsync();

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
        var customer1 = TestEntityProvider.Shared.Create<Customer>(x => x.FIO = "Попов Александр Сергеевич");
        var customer2 = TestEntityProvider.Shared.Create<Customer>(x => x.FIO = "Иванов Иван Иванович");
        var customer3 = TestEntityProvider.Shared.Create<Customer>(x => x.FIO = "Каневская Мария Андреевна");
        var customer4 = TestEntityProvider.Shared.Create<Customer>(x =>
        {
            x.FIO = "Мизулин Константин Николаевич";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });

        await Context.AddRangeAsync(customer1, customer2, customer3, customer4);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await customerReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.FIO);
    }
}