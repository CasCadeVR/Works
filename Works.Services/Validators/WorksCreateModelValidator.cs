using FluentValidation;
using CasCadeVR.Works.Services.Contracts.Models.Works;

namespace CasCadeVR.Works.Services.Validators;

/// <summary>
/// Валидация <see cref="WorksCreateModel"/>
/// </summary>
public class WorksCreateModelValidator : AbstractValidator<WorksCreateModel>
{
    private const int MinLength = 3;
    private const int MaxLength = 255;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksCreateModelValidator"/>
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
