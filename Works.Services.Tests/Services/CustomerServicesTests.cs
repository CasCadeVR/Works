using Ahatornn.TestGenerator;
using AutoMapper;
using CasCadeVR.Works.Common.Contracts;
using CasCadeVR.Works.Context.Tests;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.ReadRepositories;
using CasCadeVR.Works.Repository.WriteRepositories;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Infrastructure;
using CasCadeVR.Works.Services.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace CasCadeVR.Works.Services.Tests.Services;

/// <summary>
/// Тесты на <see cref="CustomerService"/>
/// </summary>
public class CustomerServicesTests : WorksContextInMemory
{
    private readonly ICustomerServices service;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CustomerServicesTests"/>
    /// </summary>
    public CustomerServicesTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ServiceProfile>();
        });

        var mapper = config.CreateMapper();

        service = new CustomerService(mapper,
            UnitOfWork,
            new CustomerReadRepository(Context),
            new CustomerWriteRepository(Context, Mock.Of<IDateTimeProvider>()));
    }

    /// <summary>
    /// Проверяет, что GetById падёт с ошибкой WorksNotFoundExceptions
    /// </summary>
    [Fact]
    public async Task GetByIdShouldThrow()
    {
        // Arrange
        await SeedExampleCustomer();
        var id = Guid.NewGuid();

        // Act
        var act = () => service.GetById(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
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
        var result = await service.GetById(customer.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(customer, options => options
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt)
                );
    }

    /// <summary>
    /// Проверяет, что GetById падёт с ошибкой WorksNotFoundExceptions при "мягком" удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldThrowNotFound()
    {
        // Arrange
        var customer = await SeedExampleCustomer(withSoftDelete: true);

        // Act
        var act = () => service.GetById(customer.Id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{customer.Id}*");
    }

    /// <summary>
    /// Проверяет, что при вызове вернёт пустое хранилище
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnEmpty()
    {
        // Act
        var result = await service.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Проверяет, что GetAll вернёт значения
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
        var result = await service.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.FullName);
    }

    /// <summary>
    /// Проверяет, что cоздание экземпляра падает с ошибкой о дупликате
    /// </summary>
    [Fact]
    public async Task CreateShouldThrowByTaxPayerId()
    {
        // Arrange
        var customer = await SeedExampleCustomer();
        var request = TestEntityProvider.Shared.Create<CustomerCreateModel>(x => x.TaxPayerId = customer.TaxPayerId);

        // Act
        var act = () => service.Create(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksDuplicateException>().WithMessage($"*{request.TaxPayerId}*");
    }

    /// <summary>
    /// Создание экземпляра работает
    /// </summary>
    [Fact]
    public async Task CreateShouldWork()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<CustomerCreateModel>();

        // Act
        var result = await service.Create(request, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(request);
    }

    /// <summary>
    /// Проверяет, что редактирование элемента падает с ошибкой 
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrow()
    {
        // Arrange
        await SeedExampleCustomer();
        var id = Guid.NewGuid();
        var request = TestEntityProvider.Shared.Create<CustomerCreateModel>();

        // Act
        var act = () => service.Update(id, request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверяет, что редактирование экземпляра падает с ошибкой о дупликате
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByTaxPayerId()
    {
        // Arrange
        var customer = await SeedExampleCustomer();
        var request = TestEntityProvider.Shared.Create<CustomerCreateModel>(x => x.TaxPayerId = customer.TaxPayerId);

        // Act
        var act = () => service.Update(customer.Id, request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksDuplicateException>().WithMessage($"*{request.TaxPayerId}*");
    }

    /// <summary>
    /// Проверяет, что редактирование элемента работает
    /// </summary>
    [Fact]
    public async Task UpdateShouldWork()
    {
        // Arrange
        var customer = await SeedExampleCustomer();

        var model = TestEntityProvider.Shared.Create<CustomerCreateModel>();

        // Act
        var result = await service.Update(customer.Id, model, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(model);
    }

    /// <summary>
    /// Проверка на то, что удаление падает с ошибкой
    /// </summary>
    [Fact]
    public async Task DeleteShouldThrow()
    {
        // Arrange
        await SeedExampleCustomer();
        var id = Guid.NewGuid();

        // Act
        var act = () => service.Delete(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверка на то, что удаление работает
    /// </summary>
    [Fact]
    public async Task DeleteShouldWork()
    {
        // Arrange
        var customer = await SeedExampleCustomer();

        // Act
        await service.Delete(customer.Id, CancellationToken.None);

        // Assert
        var newValue = Context.Set<Customer>().Single(x => x.Id == customer.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
