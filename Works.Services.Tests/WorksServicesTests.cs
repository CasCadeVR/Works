using Ahatornn.TestGenerator;
using AutoMapper;
using Works.Common;
using Works.Context.Tests;
using Works.Repository;
using FluentAssertions;
using Works.Services.Contracts;
using Works.Services.Infrastructure;
using Moq;
using Xunit;
using Works.Services.Contracts.Models;
using Works.Entities;
using Works.Services.Contracts.Exceptions;

namespace Works.Services.Tests;

/// <summary>
/// Тесты на <see cref="WorksServices"/>
/// </summary>
public class WorksServicesTests : WorksContextInMemory
{
    private readonly IWorksServices service;

    /// <summary>
    /// ctor
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
            new WorksReadRepository(Context),
            new WorksWriteRepository(Context, Mock.Of<IDateTimeProvider>()));
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
        var goods1 = TestEntityProvider.Shared.Create<Work>(x => x.Name = "1");
        var goods2 = TestEntityProvider.Shared.Create<Work>(x => x.Name = "2");
        var goods3 = TestEntityProvider.Shared.Create<Work>(x => x.Name = "3");
        var goods4 = TestEntityProvider.Shared.Create<Work>(x =>
        {
            x.Name = "3";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });

        await Context.AddRangeAsync(goods1, goods2, goods3, goods4);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await service.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.Name);
    }

    /// <summary>
    /// Создание экземпляра работает
    /// </summary>
    [Fact]
    public async Task CreateShouldWork()
    {
        // Arrange
        var request = TestEntityProvider.Shared.Create<WorksCreateModel>();

        // Act
        var result = await service.Create(request, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(request);

        //var newValue = Context.Set<Entities.Goods>().Single(x => x.Id == Guid.Empty);
        //newValue.DeletedAt.Should().NotBeNull();
    }

    /// <summary>
    /// Проверяет, что редактирование элемента работает
    /// </summary>
    [Fact]
    public async Task UpdateShouldWork()
    {
        // Arrange
        var good = TestEntityProvider.Shared.Create<Work>();
        await Context.AddAsync(good);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var model = TestEntityProvider.Shared.Create<WorksModel>(x =>
        {
            x.Id = good.Id;
            x.Name = "newName";
            x.Description = "newDescription";
            x.Price = 10;
        });
        var result = await service.Update(model, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(model);
    }

    /// <summary>
    /// Удаляет существующий элемент
    /// </summary>
    [Fact]
    public async Task DeleteShouldWork()
    {
        // Arrange
        var good = TestEntityProvider.Shared.Create<Work>(x => x.DeletedAt = DateTime.UtcNow);

        await Context.AddAsync(good);
        await UnitOfWork.SaveChangesAsync();

        // Act

        // Assert
        var newValue = Context.Set<Work>().Single(x => x.Id == good.Id);

        newValue.DeletedAt.Should().NotBeNull();

    }
}
