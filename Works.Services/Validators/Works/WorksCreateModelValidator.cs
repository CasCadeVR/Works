using FluentValidation;
using Works.Services.Contracts.Models.Works;

namespace Works.Services.Validators.Works;

/// <summary>
/// Валидация <see cref="WorksCreateModel"/>
/// </summary>
public class WorksCreateModelValidator : AbstractValidator<WorksCreateModel>
{
    private const int MinLength = 3;
    private const int MaxLength = 255;

    /// <summary>
    /// ctor
    /// </summary>
    public WorksCreateModelValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty().WithMessage("Наименование работы не может быть пустым")
            .Length(MinLength, MaxLength).WithMessage($"Длина наименования товара должно быть от {MinLength} до {MaxLength}");

        RuleFor(customer => customer.Description)
            .NotNull().WithMessage("Описание работы не может быть пустым");
    }
}
