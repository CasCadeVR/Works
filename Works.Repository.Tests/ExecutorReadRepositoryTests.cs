using FluentAssertions;
using CasCadeVR.Works.Context.Tests;
using Xunit;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.ReadRepositories;

namespace CasCadeVR.Works.Repository.Tests;

/// <summary>
/// Тесты на <see cref="ExecutorReadRepository"/>
/// </summary>
public class ExecutorReadRepositoryTests : WorksContextInMemory
{
    private readonly IExecutorReadRepository executorReadRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ExecutorReadRepositoryTests"/>
    /// </summary>
    public ExecutorReadRepositoryTests()
    {
        executorReadRepository = new ExecutorReadRepository(Context);
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNull()
    {
        // Arrange
        await SeedExampleExecutor();
        var id = Guid.NewGuid();

        // Act
        var result = await executorReadRepository.GetById(id, CancellationToken.None);

        // Assert
        result.Should().BeNull();
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
        var result = await executorReadRepository.GetById(executor.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(executor);
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNullByDelete()
    {
        // Arrange
        var executor = await SeedExampleExecutor(withSoftDelete: true);

        // Act
        var result = await executorReadRepository.GetById(executor.Id, CancellationToken.None);

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
        var result = await executorReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Проверяет, что GetAll вернёт список при добавлении нескольких <see cref="Executor"/>
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
        var result = await executorReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.FullName);
    }
}