using FluentAssertions;
using Works.Context.Tests;
using Xunit;
using Ahatornn.TestGenerator;
using Works.Entities;
using Works.Repository.Contracts.IReadRepositories;
using Works.Repository.ReadRepositories;

namespace Works.Repository.Tests;

/// <summary>
/// Тесты на <see cref="WorksReadRepository"/>
/// </summary>
public class WorksReadRepositoryTests : WorksContextInMemory
{
    private readonly IWorksReadRepository worksReadRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public WorksReadRepositoryTests()
    {
        worksReadRepository = new WorksReadRepository(Context);
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
        var result = await worksReadRepository.GetById(id, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null при мягком удалении <see cref="Work"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNullByDelete()
    {
        // Arrange
        var goods = TestEntityProvider.Shared.Create<Work>(x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddAsync(goods);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await worksReadRepository.GetById(goods.Id, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null при добавлении <see cref="Work"/>
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var goods = TestEntityProvider.Shared.Create<Work>();
        await Context.AddAsync(goods);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await worksReadRepository.GetById(goods.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(goods);
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null при добавлении <see cref="Work"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldBeEmpty()
    {
        // Act
        var result = await worksReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null при добавлении <see cref="Work"/>
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
        var result = await worksReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.Name);
    }
}
