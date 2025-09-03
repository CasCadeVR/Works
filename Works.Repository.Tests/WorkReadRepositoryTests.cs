using FluentAssertions;
using CasCadeVR.Works.Context.Tests;
using Xunit;
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
        await SeedExampleWork();
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
        var work = await SeedExampleWork();

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
        var work = await SeedExampleWork(withSoftDelete: true);

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
        for (int i = 0; i < 3; i++)
        {
            await SeedExampleWork();
        }

        await SeedExampleWork(withSoftDelete: true);

        // Act
        var result = await worksReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.Name);
    }
}