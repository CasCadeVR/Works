using FluentValidation;
using Works.Services.Contracts.Models.Executors;

namespace Works.Services.Validators.Executors;

/// <summary>
/// Валидация <see cref="ExecutorModel"/>
/// </summary>
public class ExecutorModelValidator : AbstractValidator<ExecutorModel>
{
    private const int MinLength = 3;
    private const int MaxLength = 255;
    private const int OGRNLength = 13;

    /// <summary>
    /// ctor
    /// </summary>
    public ExecutorModelValidator()
    {
        RuleFor(customer => customer.FIO)
            .NotEmpty().WithMessage("ФИО не может быть пустым")
            .Length(MinLength, MaxLength).WithMessage($"Длина ФИО должно быть от {MinLength} до {MaxLength}");

        RuleFor(customer => customer.Occupation)
            .NotNull().WithMessage("Должность не может быть пустым")
            .Length(MinLength, MaxLength).WithMessage($"Длина должности должна быть от {MinLength} до {MaxLength}");

        RuleFor(customer => customer.Firm)
            .NotNull().WithMessage("Фирма не может быть пустым")
            .Length(MinLength, MaxLength).WithMessage($"Длина фирмы должна быть от {MinLength} до {MaxLength}");

        RuleFor(customer => customer.OGRN)
            .NotNull().WithMessage("ОГРН не может быть пустым")
            .Length(OGRNLength).WithMessage($"ОГРН должен быть длиной {OGRNLength} цифр");
    }
}
