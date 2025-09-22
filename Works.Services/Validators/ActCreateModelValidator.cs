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
            .NotEmpty()
            .Must(x => x <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Дата заполнения не может быть в будущем");

        RuleFor(act => act.ActWorks)
            .Must(x => x.All(y => y.Quantity >= 1))
            .WithMessage("Нельзя добавить работу с количеством, меньше единицы");

        RuleFor(act => act.ActWorks)
           .Must(x => x.Select(y => y.WorkId).Distinct().Count() == x.Count)
           .WithMessage("Нельзя добавить 2 и более одинаковых работ");
    }
}
