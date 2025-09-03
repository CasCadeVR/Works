using FluentValidation;
using CasCadeVR.Works.Services.Contracts.Models.Works;
using CasCadeVR.Works.Entities.ValidationRules;

namespace CasCadeVR.Works.Services.Validators;

/// <summary>
/// Валидация <see cref="WorksCreateModel"/>
/// </summary>
public class WorksCreateModelValidator : AbstractValidator<WorksCreateModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksCreateModelValidator"/>
    /// </summary>
    public WorksCreateModelValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty().WithMessage("Наименование работы не может быть пустым")
            .Length(WorkValidationRules.NameMinLength, WorkValidationRules.NameMaxLength)
            .WithMessage($"Длина наименования должно быть от {WorkValidationRules.NameMinLength} до {WorkValidationRules.NameMaxLength}");

        RuleFor(customer => customer.Description)
            .NotNull().WithMessage("Описание работы не может быть пустым");

        RuleFor(act => act.Price)
            .GreaterThan(0)
            .WithMessage("Нельзя добавить работу с ценой, меньше нуля");
    }
}
