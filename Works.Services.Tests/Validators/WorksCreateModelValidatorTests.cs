using FluentValidation.TestHelper;
using Works.Services.Contracts.Models;
using Works.Services.Validators;
using Xunit;

namespace Works.Services.Tests.Validators;

/// <summary>
/// Тесты для <see cref="WorksModelValidator"/>
/// </summary>
public class WorksCreateModelValidatorTests
{
    private readonly WorksModelValidator validator;

    /// <summary>
    /// ctor
    /// </summary>
    public WorksCreateModelValidatorTests()
    {
        validator = new WorksCreateModelValidator();
    }

    /// <summary>
    /// Тест на пустую ошибку наименования
    /// </summary>
    [Fact]
    public async Task ShouldEmptyNameHaveErrorMessages()
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
    public async Task ShouldNameShortHaveErrorMessages()
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
    public async Task ShouldNameLongHaveErrorMessages()
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
    /// Тест на отсутствие ошибок
    /// </summary>
    [Fact]
    public async Task ShouldNotHaveErrorMessages()
    {
        // Arrange
        var model = new WorksCreateModel()
        {
            Name = "1234"
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
        result.ShouldNotHaveValidationErrorFor(x => x.Description);
    }
}