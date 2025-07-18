using FluentValidation;
using Works.Services.Contracts.Models.ActWorks;

namespace Works.Services.Validators;

/// <summary>
/// Валидация <see cref="ActWorksCreateModel"/>
/// </summary>
public class ActWorksCreateModelValidator : AbstractValidator<ActWorksCreateModel>
{
    /// <summary>
    /// ctor
    /// </summary>
    public ActWorksCreateModelValidator()
    {
        RuleFor(ActWork => ActWork.WorkId)
            .NotEmpty()
            .WithMessage("Идентификатор работы не должен быть пустым");
    }
}
