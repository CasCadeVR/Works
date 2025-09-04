using Ahatornn.TestGenerator;
using AutoMapper;
using CasCadeVR.Works.Common.Contracts;
using CasCadeVR.Works.Context.Tests;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Export.Excel;
using CasCadeVR.Works.Repository.ReadRepositories;
using CasCadeVR.Works.Repository.WriteRepositories;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Services.Contracts.Models.ActWorks;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Infrastructure;
using CasCadeVR.Works.Services.Services;
using FluentAssertions;
using Moq;
using Xunit;

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
            new ExcelExporter(Mock.Of<IAddedTaxService>()),
            UnitOfWork,
            new ActReadRepository(Context),
            new WorksReadRepository(Context),
            new CustomerReadRepository(Context),
            new ExecutorReadRepository(Context),
            new ActWorkWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
            new ActWriteRepository(Context, Mock.Of<IDateTimeProvider>()));
    }

    /// <summary>
    /// Проверяет, что GetById падёт с ошибкой WorksNotFoundExceptions
    /// </summary>
    [Fact]
    public async Task GetByIdShouldThrow()
    {
        // Arrange
        await SeedExampleAct();
        var id = Guid.NewGuid();

        // Act
        var act = () => service.GetById(id, CancellationToken.None);

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
        var act = await SeedExampleAct();
        var existingActWork = act.ActWorks.First();

        // Act
        var result = await service.GetById(act.Id, CancellationToken.None);
        var resultActWork = result.ActWorks.First();

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(new 
            {
                Id = act.Id,
                ActNumber = act.ActNumber,
                Date = act.Date,
                Customer = new
                {
                    FullName = act.Customer.FullName,
                    Firm = act.Customer.Firm,
                    Occupation = act.Customer.Occupation,
                    TaxPayerId = act.Customer.TaxPayerId,
                },
                Executor = new
                {
                    FullName = act.Executor.FullName,
                    Firm = act.Executor.Firm,
                    Occupation = act.Executor.Occupation,
                    RegistrationNumber = act.Executor.RegistrationNumber,
                },
            });

        resultActWork.Should()
            .NotBeNull()
            .And.BeEquivalentTo(new
            {
                Quantity = resultActWork.Quantity,
                Work = new
                {
                    Id = resultActWork.Work.Id,
                    Name = resultActWork.Work.Name,
                    Description = resultActWork.Work.Description,
                    Price = resultActWork.Work.Price,
                    UnitOfMeasureId = resultActWork.Work.UnitOfMeasureId,
                    UnitOfMeasure = new
                    {
                        Id = resultActWork.Work.UnitOfMeasure.Id,
                        Name = resultActWork.Work.UnitOfMeasure.Name,
                    },
                },
            });
    }

    /// <summary>
    /// Проверяет, что GetById падёт с ошибкой WorksNotFoundExceptions при "мягком" удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldThrowNotFound()
    {
        // Arrange
        var act = await SeedExampleAct(withSoftDelete: true);

        // Act
        var action = () => service.GetById(act.Id, CancellationToken.None);

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
        for (int i = 0; i < 3; i++)
        {
            await SeedExampleAct();
        }

        await SeedExampleAct(withSoftDelete: true);

        // Act
        var result = await service.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.Date);
    }

    /// <summary>
    /// Проверяет, что cоздание экземпляра падает с ошибкой о дупликате номеров акта
    /// </summary>
    [Fact]
    public async Task CreateShouldThrowByActNumber()
    {
        // Arrange
        var act = await SeedExampleAct();
        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x => x.ActNumber = act.ActNumber);

        // Act
        var acttion = () => service.Create(request, CancellationToken.None);

        // Assert
        await acttion.Should().ThrowAsync<WorksDuplicateException>().WithMessage($"*{request.ActNumber}*");
    }

    /// <summary>
    /// Проверяет, что cоздание экземпляра падает с ошибкой о дупликате работ
    /// </summary>
    [Fact]
    public async Task CreateShouldThrowByWorksDuplicate()
    {
        // Arrange
        var customer = await SeedExampleCustomer();
        var executor = await SeedExampleExecutor();
        var work = await SeedExampleWork();

        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x =>
        {
            x.CustomerId = customer.Id;
            x.ExecutorId = executor.Id;
            x.ActWorks = [TestEntityProvider.Shared.Create<ActWorksCreateModel>(y => y.WorkId = work.Id),
                TestEntityProvider.Shared.Create<ActWorksCreateModel>(y => y.WorkId = work.Id)];
        });

        // Act
        var acttion = () => service.Create(request, CancellationToken.None);

        // Assert
        await acttion.Should().ThrowAsync<WorksDuplicateException>().WithMessage($"*{work.Id}*");
    }

    /// <summary>
    /// Проверяет, что cоздание экземпляра падает с ошибкой о ненахождении работ
    /// </summary>
    [Fact]
    public async Task CreateShouldThrowByWorksIds()
    {
        // Arrange
        var id = Guid.NewGuid();
        var customer = await SeedExampleCustomer();
        var executor = await SeedExampleExecutor();

        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x =>
        {
            x.CustomerId = customer.Id;
            x.ExecutorId = executor.Id;
            x.ActWorks = [TestEntityProvider.Shared.Create<ActWorksCreateModel>(y => y.WorkId = id)];
        });

        // Act
        var acttion = () => service.Create(request, CancellationToken.None);

        // Assert
        await acttion.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Создание экземпляра работает
    /// </summary>
    [Fact]

    public async Task CreateShouldWork()
    {
        // Arrange
        var customer = await SeedExampleCustomer();
        var executor = await SeedExampleExecutor();
        var work = await SeedExampleWork();

        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x =>
        {
            x.CustomerId = customer.Id;
            x.ExecutorId = executor.Id;
            x.ActWorks = [TestEntityProvider.Shared.Create<ActWorksCreateModel>(y => y.WorkId = work.Id)];
        });

        var requestActWork = request.ActWorks.First();

        // Act
        var result = await service.Create(request, CancellationToken.None);
        var resultActWork = result.ActWorks.First();
        
        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(request, opt => opt.Excluding(x => x.ActWorks));

        resultActWork
            .Should()
            .NotBeNull()
            .And.BeEquivalentTo(requestActWork, opt => opt.Excluding(x => x.WorkId));
    }

    /// <summary>
    /// Проверяет, что cоздание экземпляра падает с ошибкой о дупликате
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByActNumber()
    {
        // Arrange
        var act = await SeedExampleAct();
        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x => x.ActNumber = act.ActNumber);

        // Act
        var action = () => service.Update(act.Id, request, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<WorksDuplicateException>().WithMessage($"*{request.ActNumber}*");
    }

    /// <summary>
    /// Проверяет, что cоздание экземпляра падает с ошибкой о ненахождении работ
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByWorksIds()
    {
        // Arrange
        var id = Guid.NewGuid();
        var act = await SeedExampleAct();
        var customer = await SeedExampleCustomer();
        var executor = await SeedExampleExecutor();

        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x =>
        {
            x.CustomerId = customer.Id;
            x.ExecutorId = executor.Id;
            x.ActWorks = [TestEntityProvider.Shared.Create<ActWorksCreateModel>(y => y.WorkId = id)];
        });

        // Act
        var acttion = () => service.Update(act.Id, request, CancellationToken.None);

        // Assert
        await acttion.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверяет, что редактирование экземпляра падает с ошибкой о дупликате работ
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByWorksDuplicate()
    {
        // Arrange
        var act = await SeedExampleAct();
        var customer = await SeedExampleCustomer();
        var executor = await SeedExampleExecutor();
        var work = await SeedExampleWork();

        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x =>
        {
            x.CustomerId = customer.Id;
            x.ExecutorId = executor.Id;
            x.ActWorks = [TestEntityProvider.Shared.Create<ActWorksCreateModel>(y => y.WorkId = work.Id),
                TestEntityProvider.Shared.Create<ActWorksCreateModel>(y => y.WorkId = work.Id)];
        });

        // Act
        var acttion = () => service.Update(act.Id, request, CancellationToken.None);

        // Assert
        await acttion.Should().ThrowAsync<WorksDuplicateException>().WithMessage($"*{work.Id}*");
    }

    /// <summary>
    /// Проверяет, что редактирование элемента падает с ошибкой о не нахождении <see cref="ActModel"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByActId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = TestEntityProvider.Shared.Create<ActCreateModel>();

        // Act
        var action = () => service.Update(id, request, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверяет, что редактирование элемента падает с ошибкой о не нахождении <see cref="CustomerModel"/>
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByCustomerId()
    {
        // Arrange
        var act = await SeedExampleAct();
       
        var customer = TestEntityProvider.Shared.Create<CustomerModel>(x => x.Id = Guid.NewGuid());
        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x => x.CustomerId = customer.Id);

        // Act
        var action = () => service.Update(act.Id, request, CancellationToken.None);

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
        var act = await SeedExampleAct();

        var executor = TestEntityProvider.Shared.Create<ExecutorModel>(x => x.Id = Guid.NewGuid());
        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x =>
        {
            x.ExecutorId = executor.Id;
            x.CustomerId = act.CustomerId;
        });

        // Act
        var action = () => service.Update(act.Id, request, CancellationToken.None);

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
        var act = await SeedExampleAct();

        var requestActWork = act.ActWorks.First();
        var request = TestEntityProvider.Shared.Create<ActCreateModel>(x =>
        {
            x.CustomerId = act.CustomerId;
            x.ExecutorId = act.ExecutorId;
            x.ActWorks = [TestEntityProvider.Shared.Create<ActWorksCreateModel>(y => y.WorkId = requestActWork.Work.Id)];
        });

        // Act
        var result = await service.Update(act.Id, request, CancellationToken.None);
        var resultActWork = result.ActWorks.First();

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(request, opt => opt.Excluding(x => x.ActWorks));

        requestActWork.Should()
            .NotBeNull()
            .And.BeEquivalentTo(requestActWork, opt => opt.Excluding(x => x.WorkId));
    }

    /// <summary>
    /// Проверка на то, что удаление падает с ошибкой
    /// </summary>
    [Fact]
    public async Task DeleteShouldThrow()
    {
        // Arrange
        await SeedExampleAct();
        var id = Guid.NewGuid();

        // Act
        var action = () => service.Delete(id, CancellationToken.None);

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
        var act = await SeedExampleAct();

        // Act
        await service.Delete(act.Id, CancellationToken.None);

        // Assert
        var newValue = Context.Set<Act>().Single(x => x.Id == act.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
