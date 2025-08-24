using FluentAssertions;
using CasCadeVR.Works.Context.Tests;
using Xunit;
using Ahatornn.TestGenerator;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.ReadRepositories;

namespace CasCadeVR.Works.Repository.Tests;

/// <summary>
/// Тесты на <see cref="WorksReadRepository"/>
/// </summary>
public class WorkReadRepositoryTests : WorksContextInMemory
{
    private readonly IWorksReadRepository worksReadRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorkReadRepositoryTests"/>
    /// </summary>
    public WorkReadRepositoryTests()
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
    /// Проверяет, что GetById вернёт <see cref="Work"/> при его добавлении 
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var work = TestEntityProvider.Shared.Create<Work>();
        await Context.AddAsync(work);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await worksReadRepository.GetById(work.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(work);
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNullByDelete()
    {
        // Arrange
        var work = TestEntityProvider.Shared.Create<Work>(x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddAsync(work);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await worksReadRepository.GetById(work.Id, CancellationToken.None);

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
        var result = await worksReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Проверяет, что GetAll вернёт список при добавлении нескольких <see cref="Work"/>
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnValues()
    {
        // Arrange
        var work1 = TestEntityProvider.Shared.Create<Work>(x => x.Name = "1");
        var work2 = TestEntityProvider.Shared.Create<Work>(x => x.Name = "2");
        var work3 = TestEntityProvider.Shared.Create<Work>(x => x.Name = "3");
        var work4 = TestEntityProvider.Shared.Create<Work>(x =>
        {
            x.Name = "3";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });

        await Context.AddRangeAsync(work1, work2, work3, work4);
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