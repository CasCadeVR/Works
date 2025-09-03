using CasCadeVR.Works.Context.Tests;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace CasCadeVR.Works.Repository.Tests;

/// <summary>
/// Тесты на <see cref="UnitOfMeasureReadRepository"/>
/// </summary>
public class UnitOfMeasureReadRepositoryTests : WorksContextInMemory
{
    private readonly IUnitOfMeasureReadRepository unitOfMeasureReadRepository;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UnitOfMeasureReadRepositoryTests"/>
    /// </summary>
    public UnitOfMeasureReadRepositoryTests()
    {
        unitOfMeasureReadRepository = new UnitOfMeasureReadRepository(Context);
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNull()
    {
        // Arrange
        await SeedExampleUnitOfMeasure();
        var id = Guid.NewGuid();

        // Act
        var result = await unitOfMeasureReadRepository.GetById(id, CancellationToken.None);

        // Assert
        result.Should().BeNull();
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
        var result = await unitOfMeasureReadRepository.GetById(unitOfMeasure.Id, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And.BeEquivalentTo(unitOfMeasure);
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNullByDelete()
    {
        // Arrange
        var unitOfMeasure = await SeedExampleUnitOfMeasure(withSoftDelete: true);

        // Act
        var result = await unitOfMeasureReadRepository.GetById(unitOfMeasure.Id, CancellationToken.None);

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
        var result = await unitOfMeasureReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Проверяет, что GetAll вернёт список при добавлении нескольких <see cref="UnitOfMeasure"/>
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
        var result = await unitOfMeasureReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .NotBeEmpty()
            .And.HaveCount(3)
            .And.BeInAscendingOrder(x => x.Name);
    }
}