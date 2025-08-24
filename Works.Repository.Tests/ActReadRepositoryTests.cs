using FluentAssertions;
using CasCadeVR.Works.Context.Tests;
using Xunit;
using Ahatornn.TestGenerator;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.ReadRepositories;

namespace CasCadeVR.Works.Repository.Tests;

/// <summary>
/// Тесты на <see cref="ActReadRepository"/>
/// </summary>
public class ActReadRepositoryTests : WorksContextInMemory
{
    private readonly IActReadRepository actReadRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ActReadRepositoryTests"/>
    /// </summary>
    public ActReadRepositoryTests()
    {
        actReadRepository = new ActReadRepository(Context);
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
        var result = await actReadRepository.GetById(id, CancellationToken.None);

        // Assert
        result.Should().BeNull();
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
        var result = await actReadRepository.GetById(act.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(act);
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNullByDelete()
    {
        // Arrange
        var act = TestEntityProvider.Shared.Create<Act>(x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddAsync(act);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await actReadRepository.GetById(act.Id, CancellationToken.None);

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
        var result = await actReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Проверяет, что GetAll вернёт список при добавлении нескольких <see cref="Act"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        var work1 = TestEntityProvider.Shared.Create<Act>(x => x.ActNumber = "1");
        var work2 = TestEntityProvider.Shared.Create<Act>(x => x.ActNumber = "2");
        var work3 = TestEntityProvider.Shared.Create<Act>(x => x.ActNumber = "3");
        var work4 = TestEntityProvider.Shared.Create<Act>(x =>
        {
            x.ActNumber = "3";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });

        await Context.AddRangeAsync(work1, work2, work3, work4);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await actReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.ActNumber);
    }
}