using FluentValidation.TestHelper;
using Works.Services.Contracts.Models.Executors;
using Works.Services.Validators;
using Xunit;

namespace Works.Services.Tests.Validators;

/// <summary>
/// Тесты для <see cref="ExecutorCreateModelValidator"/>
/// </summary>
public class ExecutorCreateModelValidatorTests
{
    private readonly ExecutorCreateModelValidator validator;

    /// <summary>
    /// ctor
    /// </summary>
    public ExecutorCreateModelValidatorTests()
    {
        validator = new ExecutorCreateModelValidator();
    }

    /// <summary>
    /// Тест на пустые поля
    /// </summary>
    [Fact]
    public async Task ShouldEmptyFIOHaveErrorMessages()
    {
        // Arrange
        var model = new ExecutorCreateModel();

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FIO);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.OGRN);
    }

    /// <summary>
    /// Тест на минимальную ошибку
    /// </summary>
    [Fact]
    public async Task ShouldShortFIOHaveErrorMessages()
    {
        // Arrange
        var model = new ExecutorCreateModel
        {
            FIO = "12",
            Firm = "12",
            Occupation = "12",
            OGRN = "12",
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FIO);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.OGRN);
    }

    /// <summary>
    /// Тест на максимальную ошибку
    /// </summary>
    [Fact]
    public async Task ShouldFIOLongHaveErrorMessages()
    {
        // Arrange
        var model = new ExecutorCreateModel()
        {
            FIO = new string('1', 300),
            Firm = new string('1', 300),
            Occupation = new string('1', 300),
            OGRN = new string('1', 14),
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FIO);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.OGRN);
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
            FIO = "Мизулин Константин Николаевич",
            Firm = "ООО ЛОМО",
            Occupation = "Уборщик",
            OGRN = "1234567890123"
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.FIO);
        result.ShouldNotHaveValidationErrorFor(x => x.Occupation);
        result.ShouldNotHaveValidationErrorFor(x => x.Firm);
        result.ShouldNotHaveValidationErrorFor(x => x.OGRN);
    }
}