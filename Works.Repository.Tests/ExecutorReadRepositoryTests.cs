using FluentAssertions;
using CasCadeVR.Works.Context.Tests;
using Xunit;
using Ahatornn.TestGenerator;
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
        var executor = TestEntityProvider.Shared.Create<Executor>();
        await Context.AddAsync(executor);
        await UnitOfWork.SaveChangesAsync();

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
        var executor = TestEntityProvider.Shared.Create<Executor>(x => x.DeletedAt = DateTimeOffset.UtcNow);
        await Context.AddAsync(executor);
        await UnitOfWork.SaveChangesAsync();

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
        var executor1 = TestEntityProvider.Shared.Create<Executor>(x => x.FIO = "Попов Александр Сергеевич");
        var executor2 = TestEntityProvider.Shared.Create<Executor>(x => x.FIO = "Иванов Иван Иванович");
        var executor3 = TestEntityProvider.Shared.Create<Executor>(x => x.FIO = "Каневская Мария Андреевна");
        var executor4 = TestEntityProvider.Shared.Create<Executor>(x =>
        {
            x.FIO = "Мизулин Константин Николаевич";
            x.DeletedAt = DateTimeOffset.UtcNow;
        });

        await Context.AddRangeAsync(executor1, executor2, executor3, executor4);
        await UnitOfWork.SaveChangesAsync();

        // Act
        var result = await executorReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.FIO);
    }
}