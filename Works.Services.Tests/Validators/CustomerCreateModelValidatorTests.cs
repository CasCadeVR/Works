using FluentValidation.TestHelper;
using Works.Services.Contracts.Models.Customers;
using Works.Services.Validators;
using Xunit;

namespace Works.Services.Tests.Validators;

/// <summary>
/// Тесты для <see cref="CustomerCreateModelValidator"/>
/// </summary>
public class CustomerCreateModelValidatorTests
{
    private readonly CustomerCreateModelValidator validator;

    /// <summary>
    /// ctor
    /// </summary>
    public CustomerCreateModelValidatorTests()
    {
        validator = new CustomerCreateModelValidator();
    }

    /// <summary>
    /// Тест на пустые поля
    /// </summary>
    [Fact]
    public async Task ShouldEmptyFIOHaveErrorMessages()
    {
        // Arrange
        var model = new CustomerCreateModel();

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FIO);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.INN);
    }

    /// <summary>
    /// Тест на минимальную ошибку
    /// </summary>
    [Fact]
    public async Task ShouldShortFIOHaveErrorMessages()
    {
        // Arrange
        var model = new CustomerCreateModel
        {
            FIO = "12",
            Firm = "12",
            Occupation = "12",
            INN = "12",
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FIO);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.INN);
    }

    /// <summary>
    /// Тест на максимальную ошибку
    /// </summary>
    [Fact]
    public async Task ShouldFIOLongHaveErrorMessages()
    {
        // Arrange
        var model = new CustomerCreateModel()
        {
            FIO = new string('1', 300),
            Firm = new string('1', 300),
            Occupation = new string('1', 300),
            INN = new string('1', 11),
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FIO);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.INN);
    }

    /// <summary>
    /// Тест на отсутствие ошибок
    /// </summary>
    [Fact]
    public async Task ShouldNotHaveErrorMessages()
    {
        // Arrange
        var model = new CustomerCreateModel()
        {
            FIO = "Мизулин Константин Николаевич",
            Firm = "ООО ЛОМО",
            Occupation = "Уборщик",
            INN = "123456789012"

        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.FIO);
        result.ShouldNotHaveValidationErrorFor(x => x.Occupation);
        result.ShouldNotHaveValidationErrorFor(x => x.Firm);
        result.ShouldNotHaveValidationErrorFor(x => x.INN);
    }
}