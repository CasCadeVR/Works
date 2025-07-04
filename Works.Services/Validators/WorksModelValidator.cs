using FluentValidation;
using Works.Services.Contracts.Models;

namespace Works.Services.Validators;

/// <summary>
/// Валидация <see cref="WorksModel"/>
/// </summary>
public class WorksModelValidator : AbstractValidator<WorksModel>
{
    private const int MinLength = 3;
    private const int MaxLength = 255;

    /// <summary>
    /// ctor
    /// </summary>
    public WorksModelValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty().WithMessage("Наименование работы не может быть пустым")
            .Length(MinLength, MaxLength).WithMessage($"Длина наименования товара должно быть от {MinLength} до {MaxLength}");

        RuleFor(customer => customer.Description)
            .NotNull().WithMessage("Описание работы не может быть пустым");
    }
}
