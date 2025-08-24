using Ahatornn.TestGenerator;
using AutoMapper;
using CasCadeVR.Works.Common;
using CasCadeVR.Works.Context.Tests;
using CasCadeVR.Works.Repository.ReadRepositories;
using CasCadeVR.Works.Repository.WriteRepositories;
using FluentAssertions;
using CasCadeVR.Works.Services.Infrastructure;
using Moq;
using Xunit;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Services;
using CasCadeVR.Works.Services.Contracts.IServices;

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
        var id = Guid.NewGuid();

        // Act
        Func<Task> act = () => service.GetById(id, CancellationToken.None);

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
        var customer = TestEntityProvider.Shared.Create<Customer>();
        await Context.AddAsync(customer);
        await UnitOfWork.SaveChangesAsync();

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
    /// Проверяет, что GetById падёт с ошибкой WorksNotFoundExceptions при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNullByDelete()
    {
        // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>(x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddAsync(customer);
        await UnitOfWork.SaveChangesAsync();

        // Act
        Func<Task> act = () => service.GetById(customer.Id, CancellationToken.None);

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
        var result = await service.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.FIO);
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
        var request = TestEntityProvider.Shared.Create<CustomerModel>(x => x.Id = Guid.NewGuid());

        // Act
        Func<Task<CustomerModel>> act = () => service.Update(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{request.Id}*");
    }

    /// <summary>
    /// Проверяет, что редактирование элемента работает
    /// </summary>
    [Fact]
    public async Task UpdateShouldWork()
    {
        // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>();
        await Context.AddAsync(customer);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var model = TestEntityProvider.Shared.Create<CustomerModel>(x => x.Id = customer.Id);

        var result = await service.Update(model, CancellationToken.None);

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
        var id = Guid.NewGuid();

        // Act
        Func<Task> act = () => service.Delete(id, CancellationToken.None);

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
        var customer = TestEntityProvider.Shared.Create<Customer>();
        await Context.AddAsync(customer);
        await UnitOfWork.SaveChangesAsync();

        // Act
        await service.Delete(customer.Id, CancellationToken.None);

        // Assert
        var newValue = Context.Set<Customer>().Single(x => x.Id == customer.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
