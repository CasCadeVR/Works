using FluentValidation.TestHelper;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Validators;
using Xunit;

namespace CasCadeVR.Works.Services.Tests.Validators;

/// <summary>
/// Тесты для <see cref="ExecutorCreateModelValidator"/>
/// </summary>
public class ExecutorCreateModelValidatorTests
{
    private readonly ExecutorCreateModelValidator validator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ExecutorCreateModelValidatorTests"/>
    /// </summary>
    public ExecutorCreateModelValidatorTests()
    {
        validator = new ExecutorCreateModelValidator();
    }

    /// <summary>
    /// Тест на пустые поля
    /// </summary>
    [Fact]
    public async Task EmptyShouldHaveErrorMessages()
    {
        // Arrange
        var model = new ExecutorCreateModel();

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.RegistrationNumber);
    }

    /// <summary>
    /// Тест на минимальную ошибку
    /// </summary>
    [Fact]
    public async Task ShortShouldHaveErrorMessages()
    {
        // Arrange
        var model = new ExecutorCreateModel
        {
            FullName = "12",
            Firm = "12",
            Occupation = "12",
            RegistrationNumber = "12",
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.RegistrationNumber);
    }

    /// <summary>
    /// Тест на максимальную ошибку
    /// </summary>
    [Fact]
    public async Task LongShouldHaveErrorMessages()
    {
        // Arrange
        var model = new ExecutorCreateModel()
        {
            FullName = new string('1', 300),
            Firm = new string('1', 300),
            Occupation = new string('1', 300),
            RegistrationNumber = new string('1', 14),
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.RegistrationNumber);
    }

    /// <summary>
    /// Тест на отсутствие ошибок
    /// </summary>
    [Fact]
    public async Task ShouldNotHaveErrorMessages()
    {
        // Arrange
        var model = new ExecutorCreateModel()
        {
            FullName = "Мизулин Константин Николаевич",
            Firm = "ООО ЛОМО",
            Occupation = "Уборщик",
            RegistrationNumber = "1234567890123"
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.FullName);
        result.ShouldNotHaveValidationErrorFor(x => x.Occupation);
        result.ShouldNotHaveValidationErrorFor(x => x.Firm);
        result.ShouldNotHaveValidationErrorFor(x => x.RegistrationNumber);
    }
}