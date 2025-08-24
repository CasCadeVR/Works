using FluentValidation;
using CasCadeVR.Works.Services.Contracts.Models.Acts;

namespace CasCadeVR.Works.Services.Validators;

/// <summary>
/// Валидация <see cref="ActCreateModel"/>
/// </summary>
public class ActCreateModelValidator : AbstractValidator<ActCreateModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ActCreateModelValidator"/>
    /// </summary>
    public ActCreateModelValidator()
    {
        RuleFor(act => act.ActNumber)
            .NotEmpty()
            .WithMessage("Номер акта не должен быть пустым");

        RuleFor(act => act.Date)
            .Must(x => x >= DateOnly.FromDateTime(DateTime.UtcNow))
            .NotEmpty()
            .WithMessage("Дата заполнения не может быть в будущем");

        RuleFor(act => act.NDS)
            .GreaterThan(0)
            .WithMessage("НДС не может быть меньше нуля");
    }
}
