using FluentAssertions;
using CasCadeVR.Works.Context.Tests;
using Xunit;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.ReadRepositories;
using CasCadeVR.Works.Repository.Contracts.Models;

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
        await SeedExampleAct();
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
        var act = await SeedExampleAct();
        var existingActWork = act.ActWorks.First();

        var expectedResult = new ActDbModel
        {
            Id = act.Id,
            ActNumber = act.ActNumber,
            Date = act.Date,
            Customer = act.Customer,
            Executor = act.Executor,
            ActWorks = act.ActWorks.Select(y => new ActWorkDbModel
            {
                Id = y.Id,
                Quantity = y.Quantity,
                Work = new WorkDbModel
                {
                    Id = y.Work.Id,
                    Name = y.Work.Name,
                    Description = y.Work.Description,
                    Price = y.Work.Price,
                    UnitOfMeasure = y.Work.UnitOfMeasure,
                }
            }).ToList(),
        };

        // Act
        var result = await actReadRepository.GetById(act.Id, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    /// <summary>
    /// Проверяет, что GetById вернёт null при мягком удалении
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnNullByDelete()
    {
        // Arrange
        var act = await SeedExampleAct(withSoftDelete: true);

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
        for (int i = 0; i < 3; i++)
        {
            await SeedExampleAct();
        }

        await SeedExampleAct(withSoftDelete: true);

        // Act
        var result = await actReadRepository.GetAll(CancellationToken.None);

        // Assert
        result.Should()
            .HaveCount(3)
            .And.BeInAscendingOrder(x => x.Date);
    }
}