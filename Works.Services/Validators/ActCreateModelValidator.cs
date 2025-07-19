using FluentValidation;
using Works.Services.Contracts.Models.Acts;

namespace Works.Services.Validators;

/// <summary>
/// Валидация <see cref="ActCreateModel"/>
/// </summary>
public class ActCreateModelValidator : AbstractValidator<ActCreateModel>
{
    /// <summary>
    /// ctor
    /// </summary>
    public ActCreateModelValidator()
    {
        RuleFor(act => act.ActNumber)
            .NotEmpty()
            .WithMessage("Номер акта не должен быть пустым");

        RuleFor(act => act.Date)
            .Must(NotBeInFuture)
            .NotEmpty()
            .WithMessage("Дата заполнения не может быть в будущем");

        RuleFor(act => act.NDS)
            .GreaterThan(0)
            .WithMessage("НДС не может быть меньше нуля");
    }

    private bool NotBeInFuture(DateTime time) => time >= DateTime.UtcNow;
}
