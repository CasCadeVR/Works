using Ahatornn.TestGenerator;
using AutoMapper;
using CasCadeVR.Works.Common.Contracts;
using CasCadeVR.Works.Context.Tests;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.ReadRepositories;
using CasCadeVR.Works.Repository.WriteRepositories;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;
using CasCadeVR.Works.Services.Infrastructure;
using CasCadeVR.Works.Services.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace CasCadeVR.Works.Services.Tests.Services;

/// <summary>
/// Тесты на <see cref="UnitOfMeasureServices"/>
/// </summary>
public class UnitOfMeasureServicesTests : WorksContextInMemory
{
    private readonly IUnitOfMeasureServices service;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UnitOfMeasureServicesTests"/>
    /// </summary>
    public UnitOfMeasureServicesTests()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile<ServiceProfile>();
        });

        var mapper = config.CreateMapper();

        service = new UnitOfMeasureServices(mapper,
            UnitOfWork,
            new UnitOfMeasureReadRepository(Context),
            new UnitOfMeasureWriteRepository(Context, Mock.Of<IDateTimeProvider>()));
    }

    /// <summary>
    /// Проверяет, что GetById падёт с ошибкой WorksNotFoundExceptions
    /// </summary>
    [Fact]
    public async Task GetByIdShouldThrow()
    {
        // Arrange
        await SeedExampleUnitOfMeasure();
        var id = Guid.NewGuid();

        // Act
        var act = () => service.GetById(id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверяет, что GetById вернёт <see cref="UnitOfMeasure"/> при его добавлении 
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var unitOfMeasure = await SeedExampleUnitOfMeasure();

        // Act
        var result = await service.GetById(unitOfMeasure.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(unitOfMeasure, options => options
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
        var unitOfMeasure = await SeedExampleUnitOfMeasure(withSoftDelete: true);

        // Act
        var act = () => service.GetById(unitOfMeasure.Id, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{unitOfMeasure.Id}*");
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
            await SeedExampleUnitOfMeasure();
        }

        await SeedExampleUnitOfMeasure(withSoftDelete: true);

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
        var unitOfMeasure = await SeedExampleUnitOfMeasure();
        var request = TestEntityProvider.Shared.Create<UnitOfMeasureCreateModel>(x => x.Name = unitOfMeasure.Name);

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
        var request = TestEntityProvider.Shared.Create<UnitOfMeasureCreateModel>();

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
    public async Task UpdateShouldThrowById()
    {
        // Arrange
        await SeedExampleUnitOfMeasure();
        var id = Guid.NewGuid();
        var request = TestEntityProvider.Shared.Create<UnitOfMeasureCreateModel>();

        // Act
        var act = () => service.Update(id, request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<WorksNotFoundException>().WithMessage($"*{id}*");
    }

    /// <summary>
    /// Проверяет, что редактирование экземпляра падает с ошибкой о дупликате
    /// </summary>
    [Fact]
    public async Task UpdateShouldThrowByName()
    {
        // Arrange
        var unitOfMeasure = await SeedExampleUnitOfMeasure();
        var request = TestEntityProvider.Shared.Create<UnitOfMeasureCreateModel>(x => x.Name = unitOfMeasure.Name);

        // Act
        var act = () => service.Update(unitOfMeasure.Id, request, CancellationToken.None);

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
        var unitOfMeasure = await SeedExampleUnitOfMeasure();

        var model = TestEntityProvider.Shared.Create<UnitOfMeasureCreateModel>();

        // Act
        var result = await service.Update(unitOfMeasure.Id, model, CancellationToken.None);

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
        await SeedExampleUnitOfMeasure();
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
        var unitOfMeasure = await SeedExampleUnitOfMeasure();

        // Act
        await service.Delete(unitOfMeasure.Id, CancellationToken.None);

        // Assert
        var newValue = Context.Set<UnitOfMeasure>().Single(x => x.Id == unitOfMeasure.Id);
        newValue.DeletedAt.Should().NotBeNull();
    }
}
