using FluentValidation.TestHelper;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Services.Validators;
using Xunit;
using CasCadeVR.Works.Services.Contracts.Models.ActWorks;

namespace CasCadeVR.Works.Services.Tests.Validators;

/// <summary>
/// Тесты для <see cref="ActCreateModelValidator"/>
/// </summary>
public class ActCreateModelValidatorTests
{
    private readonly ActCreateModelValidator validator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ActCreateModelValidatorTests"/>
    /// </summary>
    public ActCreateModelValidatorTests()
    {
        validator = new ActCreateModelValidator();
    }

    /// <summary>
    /// Тест на пустые поля
    /// </summary>
    [Fact]
    public async Task EmptyShouldHaveErrorMessages()
    {
        // Arrange
        var model = new ActCreateModel();

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ActNumber);
        result.ShouldHaveValidationErrorFor(x => x.Date);
    }

    /// <summary>
    /// Тест на прошлую дату
    /// </summary>
    [Fact]
    public async Task FutureDateShouldHaveErrorMessages()
    {
        // Arrange
        var model = new ActCreateModel
        {
            Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3)),
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Date);
    }

    /// <summary>
    /// Тест на количество работ меньше единицы
    /// </summary>
    [Fact]
    public async Task PastDateShouldHaveErrorMessages()
    {
        // Arrange
        var model = new ActCreateModel
        {
            ActWorks = [new ActWorksCreateModel() {
                Quantity = 0,
            }],
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ActWorks);
    }

    /// <summary>
    /// Тест на дубликаты работ
    /// </summary>
    [Fact]
    public async Task DuplicateShouldHaveErrorMessages()
    {
        // Arrange
        var workId = Guid.NewGuid();
        var model = new ActCreateModel
        {
            ActWorks = [
                new ActWorksCreateModel()
                {
                    Quantity = 1,
                    WorkId = workId,
                },
                new ActWorksCreateModel()
                {
                    Quantity = 2,
                    WorkId = workId,
                },
            ],
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ActWorks);
    }

    /// <summary>
    /// Тест на отсутствие ошибок
    /// </summary>
    [Fact]
    public async Task ShouldNotHaveErrorMessages()
    {
        // Arrange
        var model = new ActCreateModel()
        {
            ActNumber = "1",
            Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)),
            ActWorks = [new ActWorksCreateModel() {
                Quantity = 1,
            }],
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.ActNumber);
        result.ShouldNotHaveValidationErrorFor(x => x.Date);
        result.ShouldNotHaveValidationErrorFor(x => x.ActWorks);
    }
}