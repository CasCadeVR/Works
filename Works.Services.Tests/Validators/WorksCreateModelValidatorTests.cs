using FluentValidation.TestHelper;
using CasCadeVR.Works.Services.Contracts.Models.Works;
using CasCadeVR.Works.Services.Validators;
using Xunit;

namespace CasCadeVR.Works.Services.Tests.Validators;

/// <summary>
/// Тесты для <see cref="WorksCreateModelValidator"/>
/// </summary>
public class WorksCreateModelValidatorTests
{
    private readonly WorksCreateModelValidator validator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksCreateModelValidatorTests"/>
    /// </summary>
    public WorksCreateModelValidatorTests()
    {
        validator = new WorksCreateModelValidator();
    }

    /// <summary>
    /// Тест на пустую ошибку наименования
    /// </summary>
    [Fact]
    public async Task EmptyShouldHaveErrorMessages()
    {
        // Arrange
        var model = new WorksCreateModel();

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    /// <summary>
    /// Тест на минимальную ошибку наименования
    /// </summary>
    [Fact]
    public async Task ShortShouldHaveErrorMessages()
    {
        // Arrange
        var model = new WorksCreateModel
        {
            Name = "12"
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    /// <summary>
    /// Тест на максимальную ошибку наименования
    /// </summary>
    [Fact]
    public async Task LongShouldHaveErrorMessages()
    {
        // Arrange
        var model = new WorksCreateModel()
        {
            Name = new string('1', 300)
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    /// <summary>
    /// Тест на отрицательную цену
    /// </summary>
    [Fact]
    public async Task NegativePriceShouldHaveErrorMessages()
    {
        // Arrange
        var model = new WorksCreateModel()
        {
            Price = -100
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    /// <summary>
    /// Тест на отсутствие ошибок
    /// </summary>
    [Fact]
    public async Task ShouldNotHaveErrorMessages()
    {
        // Arrange
        var model = new WorksCreateModel()
        {
            Name = "1234",
            Price = 1000
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
        result.ShouldNotHaveValidationErrorFor(x => x.Price);
        result.ShouldNotHaveValidationErrorFor(x => x.Description);
    }
}