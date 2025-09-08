using Ahatornn.TestGenerator;
using AutoMapper;
using CasCadeVR.Works.Common.Contracts;
using CasCadeVR.Works.Context.Tests;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.ReadRepositories;
using CasCadeVR.Works.Repository.WriteRepositories;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Infrastructure;
using CasCadeVR.Works.Services.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace CasCadeVR.Works.Services.Tests.Services;

/// <summary>
/// Тесты на <see cref="ExecutorServices"/>
/// </summary>
public class ExecutorServicesTests : WorksContextInMemory
{
    private readonly IExecutorServices service;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ExecutorServicesTests"/>
    /// </summary>
    public ExecutorServicesTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ServiceProfile>();
        });

        var mapper = config.CreateMapper();

        service = new ExecutorServices(mapper,
            UnitOfWork,
            new ExecutorReadRepository(Context),
            new ExecutorWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
            new ActReadRepository(Context),
            new ActWriteRepository(Context, Mock.Of<IDateTimeProvider>())
            );
    }

    /// <summary>
    /// Проверяет, что GetById падёт с ошибкой WorksNotFoundExceptions
    /// </summary>
    [Fact]
    public async Task GetByIdShouldThrow()
    {
        // Arrange
        await SeedExampleExecutor();
        var id = Guid.NewGuid();

        // Act
        var act = () => service.GetById(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверяет, что GetById вернёт <see cref="Executor"/> при его добавлении 
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var executor = await SeedExampleExecutor();

        // Act
        var result = await service.GetById(executor.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(executor, options => options
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
        var executor = await SeedExampleExecutor(withSoftDelete: true);

        // Act
        var act = () => service.GetById(executor.Id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{executor.Id}*");
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
            await SeedExampleExecutor();
        }

        await SeedExampleExecutor(withSoftDelete: true);

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
    public async Task CreateShouldThrowByRegistrationNumber()
    {
        // Arrange
        var executor = await SeedExampleExecutor();
        var request = TestEntityProvider.Shared.Create<ExecutorCreateModel>(x => x.RegistrationNumber = executor.RegistrationNumber);

        // Act
        var act = () => service.Create(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksDuplicateException>().WithMessage($"*{request.RegistrationNumber}*");
    }

    /// <summary>
    /// Создание экземпляра работает
    /// </summary>
    [Fact]
    public async Task CreateShouldWork()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<ExecutorCreateModel>();

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
        await SeedExampleExecutor();
        var id = Guid.NewGuid();
        var request = TestEntityProvider.Shared.Create<ExecutorCreateModel>();

        // Act
        var act = () => service.Update(id, request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверяет, что cоздание экземпляра падает с ошибкой о дупликате
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByRegistrationNumber()
    {
        // Arrange
        var executor = await SeedExampleExecutor();
        var request = TestEntityProvider.Shared.Create<ExecutorCreateModel>(x => x.RegistrationNumber = executor.RegistrationNumber);

        // Act
        var act = () => service.Update(executor.Id, request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksDuplicateException>().WithMessage($"*{request.RegistrationNumber}*");
    }

    /// <summary>
    /// Проверяет, что редактирование элемента работает
    /// </summary>
    [Fact]
    public async Task UpdateShouldWork()
    {
        // Arrange
        var executor = await SeedExampleExecutor();
        var model = TestEntityProvider.Shared.Create<ExecutorCreateModel>();

        // Act
        var result = await service.Update(executor.Id, model, CancellationToken.None);

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
        await SeedExampleExecutor();
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
        var executor = await SeedExampleExecutor();

        // Act
        await service.Delete(executor.Id, CancellationToken.None);

        // Assert
        var newValue = Context.Set<Executor>().Single(x => x.Id == executor.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
