using FluentValidation;
using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;
using CasCadeVR.Works.Entities.ValidationRules;

namespace CasCadeVR.Works.Services.Validators;

/// <summary>
/// Валидация <see cref="UnitOfMeasureCreateModel"/>
/// </summary>
public class UnitOfMeasureCreateModelValidator : AbstractValidator<UnitOfMeasureCreateModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksCreateModelValidator"/>
    /// </summary>
    public UnitOfMeasureCreateModelValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty().WithMessage("Название единицы измерения не может быть пустым")
            .Length(0, UnitOfMeasureValidationRules.NameMaxLength)
            .WithMessage($"Название единицы измерения должно быть от 0 до {UnitOfMeasureValidationRules.NameMaxLength}");
    }
}
