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
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Services.Services;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.ActWorks;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Contracts.Models.Works;

namespace CasCadeVR.Works.Services.Tests.Services;

/// <summary>
/// Тесты на <see cref="WorksServices"/>
/// </summary>
public class ActServicesTests : WorksContextInMemory
{
    private readonly IActServices service;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ActServicesTests"/>
    /// </summary>
    public ActServicesTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ServiceProfile>();
        });

        var mapper = config.CreateMapper();

        service = new ActServices(mapper,
            UnitOfWork,
            new ActReadRepository(Context),
            new WorksReadRepository(Context),
            new CustomerReadRepository(Context),
            new ExecutorReadRepository(Context),
            new ActWorkWriteRepository(Context),
            new ActWriteRepository(Context, Mock.Of<IDateTimeProvider>()));
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
    /// Проверяет, что GetById вернёт <see cref="Act"/> при его добавлении 
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var act = TestEntityProvider.Shared.Create<Act>();
        await Context.AddAsync(act);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await service.GetById(act.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(act, options => options
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt)
                .Excluding(x => x.CustomerId)
                .Excluding(x => x.Customer.CreatedAt)
                .Excluding(x => x.Customer.UpdatedAt)
                .Excluding(x => x.Customer.DeletedAt)
                .Excluding(x => x.ExecutorId)
                .Excluding(x => x.Executor.CreatedAt)
                .Excluding(x => x.Executor.UpdatedAt)
                .Excluding(x => x.Executor.DeletedAt)
                );
    }

    /// <summary>
    /// Проверяет, что GetById падёт с ошибкой WorksNotFoundExceptions при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNullByDelete()
    {
        // Arrange
        var act = TestEntityProvider.Shared.Create<Act>(x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddAsync(act);
        await UnitOfWork.SaveChangesAsync();

        // Act
        Func<Task> action = () => service.GetById(act.Id, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{act.Id}*");
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
        var act1 = TestEntityProvider.Shared.Create<Act>(x => x.ActNumber = "1");
        var act2 = TestEntityProvider.Shared.Create<Act>(x => x.ActNumber = "2");
        var act3 = TestEntityProvider.Shared.Create<Act>(x => x.ActNumber = "3");
        var act4 = TestEntityProvider.Shared.Create<Act>(x =>
        {
            x.ActNumber = "4";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });

        await Context.AddRangeAsync(act1, act2, act3, act4);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await service.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.ActNumber);
    }

    /// <summary>
    /// Создание экземпляра работает
    /// </summary>
    [Fact]
    public async Task CreateShouldWork()
    {
        // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>();
        var executor = TestEntityProvider.Shared.Create<Executor>();
        var work = TestEntityProvider.Shared.Create<Work>();

        await Context.AddRangeAsync(customer, executor, work);
        await UnitOfWork.SaveChangesAsync();

        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x =>
        {
            x.CustomerId = customer.Id;
            x.ExecutorId = executor.Id;
            x.Works = new List<ActWorksCreateModel>()
            { TestEntityProvider.Shared.Create<ActWorksCreateModel>(x => x.WorkId = work.Id) };
        });

        // Act
        var result = await service.Create(request, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(request, opt => opt
                .Excluding(x => x.CustomerId)
                .Excluding(x => x.ExecutorId)
                .Excluding(x => x.Works)
                .Excluding(x => x.Works)
                .Using<ActWork>(ctx => ctx.Subject.Should().BeEquivalentTo(ctx.Expectation, opt =>
                    opt.Excluding(w => w.WorkId)))
                .WhenTypeIs<ActWork>()
            );
    }

    /// <summary>
    /// Проверяет, что редактирование элемента падает с ошибкой о не нахождении <see cref="ActModel"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByActId()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<ActModel>(x => x.Id = Guid.NewGuid());

        // Act
        Func<Task<ActModel>> action = () => service.Update(request, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{request.Id}*");
    }

    /// <summary>
    /// Проверяет, что редактирование элемента падает с ошибкой о не нахождении <see cref="CustomerModel"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByCustomerId()
    {
        // Arrange
        var act = TestEntityProvider.Shared.Create<Act>();
        await Context.AddAsync(act);
        await UnitOfWork.SaveChangesAsync();
       
        var customer = TestEntityProvider.Shared.Create<CustomerModel>(x => x.Id = Guid.NewGuid());
        var request = TestEntityProvider.Shared.Create<ActModel>(x =>
        {
            x.Id = act.Id;
            x.Customer = customer;
        });

        // Act
        Func<Task<ActModel>> action = () => service.Update(request, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{customer.Id}*");
    }

    /// <summary>
    /// Проверяет, что редактирование элемента падает с ошибкой о не нахождении <see cref="ExecutorModel"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByExecutorId()
    {
        // Arrange
        var act = TestEntityProvider.Shared.Create<Act>();
        var customer = TestEntityProvider.Shared.Create<Customer>(x => x.Id = Guid.NewGuid());
        await Context.AddRangeAsync(act, customer);
        await UnitOfWork.SaveChangesAsync();

        var executor = TestEntityProvider.Shared.Create<ExecutorModel>(x => x.Id = Guid.NewGuid());
        var request = TestEntityProvider.Shared.Create<ActModel>(x =>
        {
            x.Id = act.Id;
            x.Executor = executor;
            x.Customer = TestEntityProvider.Shared.Create<CustomerModel>(x => x.Id = customer.Id);
        });

        // Act
        Func<Task<ActModel>> action = () => service.Update(request, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{executor.Id}*");
    }

    /// <summary>
    /// Проверяет, что редактирование элемента работает
    /// </summary>
    [Fact]
    public async Task UpdateShouldWork()
    {
        // Arrange
        var customer = TestEntityProvider.Shared.Create<Customer>();
        var executor = TestEntityProvider.Shared.Create<Executor>();
        var act = TestEntityProvider.Shared.Create<Act>();
        var work = TestEntityProvider.Shared.Create<Work>();

        await Context.AddRangeAsync(customer, executor, act, work);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var model = TestEntityProvider.Shared.Create<ActModel>(x =>
        {
            x.Id = act.Id;
            x.ActNumber = "1";
            x.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            x.Customer = TestEntityProvider.Shared.Create<CustomerModel>(x => {
                x.Id = customer.Id;
                x.FIO = customer.FIO;
                x.Firm = customer.Firm;
                x.Occupation = customer.Occupation;
                x.INN = customer.INN;
            });
            x.Executor = TestEntityProvider.Shared.Create<ExecutorModel>(x => {
                x.Id = executor.Id;
                x.FIO = executor.FIO;
                x.Firm = executor.Firm;
                x.Occupation = executor.Occupation;
                x.OGRN = executor.OGRN;
            });
            x.Works = new List<ActWorksModel>()
            {TestEntityProvider.Shared.Create<ActWorksModel>(x =>
                {
                    x.Work = TestEntityProvider.Shared.Create<WorksModel>(x =>
                    {
                        x.Id = work.Id;
                        x.Name = work.Name;
                        x.Description = work.Description;
                        x.Price = work.Price;
                        x.UnitOfMeasure = work.UnitOfMeasure;
                    });
                    x.Quantity = 50;
                    x.ActualPrice = 50000;
                }
            )};
            x.NDS = 14.4m;
        });

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
        Func<Task> action = () => service.Delete(id, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверка на то, что удаление работает
    /// </summary>
    [Fact]
    public async Task DeleteShouldWork()
    {
        // Arrange
        var act = TestEntityProvider.Shared.Create<Act>();
        await Context.AddAsync(act);
        await UnitOfWork.SaveChangesAsync();

        // Act
        await service.Delete(act.Id, CancellationToken.None);

        // Assert
        var newValue = Context.Set<Act>().Single(x => x.Id == act.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
