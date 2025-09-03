using FluentValidation.TestHelper;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Validators;
using Xunit;

namespace CasCadeVR.Works.Services.Tests.Validators;

/// <summary>
/// Тесты для <see cref="CustomerCreateModelValidator"/>
/// </summary>
public class CustomerCreateModelValidatorTests
{
    private readonly CustomerCreateModelValidator validator;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CustomerCreateModelValidatorTests"/>
    /// </summary>
    public CustomerCreateModelValidatorTests()
    {
        validator = new CustomerCreateModelValidator();
    }

    /// <summary>
    /// Тест на пустые поля
    /// </summary>
    [Fact]
    public async Task EmptyShouldHaveErrorMessages()
    {
        // Arrange
        var model = new CustomerCreateModel();

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.TaxPayerId);
    }

    /// <summary>
    /// Тест на минимальную ошибку
    /// </summary>
    [Fact]
    public async Task ShortShouldHaveErrorMessages()
    {
        // Arrange
        var model = new CustomerCreateModel
        {
            FullName = "12",
            Firm = "12",
            Occupation = "12",
            TaxPayerId = "12",
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.TaxPayerId);
    }

    /// <summary>
    /// Тест на максимальную ошибку
    /// </summary>
    [Fact]
    public async Task LongShouldHaveErrorMessages()
    {
        // Arrange
        var model = new CustomerCreateModel()
        {
            FullName = new string('1', 300),
            Firm = new string('1', 300),
            Occupation = new string('1', 300),
            TaxPayerId = new string('1', 11),
        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
        result.ShouldHaveValidationErrorFor(x => x.Firm);
        result.ShouldHaveValidationErrorFor(x => x.Occupation);
        result.ShouldHaveValidationErrorFor(x => x.TaxPayerId);
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
            FullName = "Мизулин Константин Николаевич",
            Firm = "ООО ЛОМО",
            Occupation = "Уборщик",
            TaxPayerId = "123456789012"

        };

        // Act
        var result = await validator.TestValidateAsync(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.FullName);
        result.ShouldNotHaveValidationErrorFor(x => x.Occupation);
        result.ShouldNotHaveValidationErrorFor(x => x.Firm);
        result.ShouldNotHaveValidationErrorFor(x => x.TaxPayerId);
    }
}