using Ahatornn.TestGenerator;
using AutoMapper;
using CasCadeVR.Works.Common.Contracts;
using CasCadeVR.Works.Context.Tests;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.ReadRepositories;
using CasCadeVR.Works.Repository.WriteRepositories;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.Works;
using CasCadeVR.Works.Services.Infrastructure;
using CasCadeVR.Works.Services.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace CasCadeVR.Works.Services.Tests.Services;

/// <summary>
/// Тесты на <see cref="WorksServices"/>
/// </summary>
public class WorksServicesTests : WorksContextInMemory
{
    private readonly IWorksServices service;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksServicesTests"/>
    /// </summary>
    public WorksServicesTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ServiceProfile>();
        });

        var mapper = config.CreateMapper();

        service = new WorksServices(mapper,
            UnitOfWork,
            new UnitOfMeasureReadRepository(Context),
            new WorksReadRepository(Context),
            new WorksWriteRepository(Context, Mock.Of<IDateTimeProvider>()));
    }

    /// <summary>
    /// Проверяет, что GetById падёт с ошибкой WorksNotFoundExceptions
    /// </summary>
    [Fact]
    public async Task GetByIdShouldThrow()
    {
        // Arrange
        await SeedExampleWork();
        var id = Guid.NewGuid();

        // Act
        var act = () => service.GetById(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверяет, что GetById вернёт <see cref="Work"/> при его добавлении 
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var work = await SeedExampleWork();

        // Act
        var result = await service.GetById(work.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(work, options => options
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt)
                .Excluding(x => x.UnitOfMeasureId)
                .Excluding(x => x.UnitOfMeasure.CreatedAt)
                .Excluding(x => x.UnitOfMeasure.UpdatedAt)
                .Excluding(x => x.UnitOfMeasure.DeletedAt)
                );
    }

    /// <summary>
    /// Проверяет, что GetById падёт с ошибкой WorksNotFoundExceptions при "мягком" удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldThrowNotFound()
    {
        // Arrange
        var work = await SeedExampleWork(withSoftDelete: true);

        // Act
        var act = () => service.GetById(work.Id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{work.Id}*");
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
            await SeedExampleWork();
        }

        await SeedExampleWork(withSoftDelete: true);

        // Act
        var result = await service.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.Name);
    }

    /// <summary>
    /// Проверяет, что cоздание экземпляра падает с ошибкой о дупликате
    /// </summary>
    [Fact]
    public async Task CreateShouldThrowByName()
    {
        // Arrange
        var work = await SeedExampleWork();
        var request = TestEntityProvider.Shared.Create<WorksCreateModel>(x => x.Name = work.Name);

        // Act
        var act = () => service.Create(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksDuplicateException>().WithMessage($"*{request.Name}*");
    }

    /// <summary>
    /// Создание экземпляра работает
    /// </summary>
    [Fact]
    public async Task CreateShouldWork()
    {
        // Arrange
        var unitOfMeasure = await SeedExampleUnitOfMeasure();
        var request = TestEntityProvider.Shared.Create<WorksCreateModel>(x => x.UnitOfMeasureId = unitOfMeasure.Id);

        // Act
        var result = await service.Create(request, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(request, opt => opt.Excluding(x => x.UnitOfMeasureId));
    }

    /// <summary>
    /// Проверяет, что редактирование элемента падает с ошибкой 
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrow()
    {
        // Arrange
        var unitOfMeasure = await SeedExampleUnitOfMeasure();
        var request = TestEntityProvider.Shared.Create<WorksCreateModel>(x => x.UnitOfMeasureId = unitOfMeasure.Id);
        var id = Guid.NewGuid();

        // Act
        var act = () => service.Update(id, request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверяет, что cоздание экземпляра падает с ошибкой о дупликате
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByName()
    {
        // Arrange
        var work = await SeedExampleWork();
        var request = TestEntityProvider.Shared.Create<WorksCreateModel>(x => x.Name = work.Name);

        // Act
        var act = () => service.Update(work.Id, request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksDuplicateException>().WithMessage($"*{request.Name}*");
    }

    /// <summary>
    /// Проверяет, что редактирование элемента работает
    /// </summary>
    [Fact]
    public async Task UpdateShouldWork()
    {
        // Arrange
        var work = await SeedExampleWork();

        var model = TestEntityProvider.Shared.Create<WorksCreateModel>(x => x.UnitOfMeasureId = work.UnitOfMeasureId);

        // Act
        var result = await service.Update(work.Id, model, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(model, opt => opt.Excluding(x => x.UnitOfMeasureId));
    }

    /// <summary>
    /// Проверка на то, что удаление падает с ошибкой
    /// </summary>
    [Fact]
    public async Task DeleteShouldThrow()
    {
        // Arrange
        await SeedExampleWork();
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
        var work = await SeedExampleWork();

        // Act
        await service.Delete(work.Id, CancellationToken.None);

        // Assert
        var newValue = Context.Set<Work>().Single(x => x.Id == work.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
