using FluentValidation.TestHelper;
using Works.Services.Contracts.Models.Acts;
using Works.Services.Validators;
using Xunit;

namespace Works.Services.Tests.Validators;

/// <summary>
/// Тесты для <see cref="ActCreateModelValidator"/>
/// </summary>
public class ActCreateModelValidatorTests
{
    private readonly ActCreateModelValidator validator;

    /// <summary>
    /// ctor
    /// </summary>
    public ActCreateModelValidatorTests()
    {
        validator = new ActCreateModelValidator();
    }

    /// <summary>
    /// Тест на пустые поля
    /// </summary>
    [Fact]
    public async Task EmptyFieldsShouldHaveErrorMessages()
    {
        // Arrange
        var model = new ActCreateModel();

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ActNumber);
        result.ShouldHaveValidationErrorFor(x => x.Date);
        result.ShouldHaveValidationErrorFor(x => x.NDS);
    }

    /// <summary>
    /// Тест на прошлую дату
    /// </summary>
    [Fact]
    public async Task PastDateShouldHaveErrorMessages()
    {
        // Arrange
        var model = new ActCreateModel
        {
            Date = DateTime.UtcNow.AddDays(-3),
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Date);
    }

    /// <summary>
    /// Тест на НДС
    /// </summary>
    [Fact]
    public async Task NdsLessThanZeroShouldHaveErrorMessages()
    {
        // Arrange
        var model = new ActCreateModel()
        {
            NDS = 0,
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NDS);
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
            Date = DateTime.UtcNow,
            NDS = 14.4M,
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.ActNumber);
        result.ShouldNotHaveValidationErrorFor(x => x.Date);
        result.ShouldNotHaveValidationErrorFor(x => x.NDS);
    }
}