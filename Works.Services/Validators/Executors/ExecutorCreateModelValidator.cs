using FluentValidation;
using Works.Services.Contracts.Models.Executors;

namespace Works.Services.Validators.Executors;

/// <summary>
/// Валидация <see cref="ExecutorCreateModel"/>
/// </summary>
public class ExecutorCreateModelValidator : AbstractValidator<ExecutorCreateModel>
{
    private const int MinLength = 3;
    private const int MaxLength = 255;
    private const int OGRNLength = 13;

    /// <summary>
    /// ctor
    /// </summary>
    public ExecutorCreateModelValidator()
    {
        RuleFor(executor => executor.FIO)
            .NotEmpty().WithMessage("ФИО не может быть пустым")
            .Length(MinLength, MaxLength).WithMessage($"Длина ФИО должно быть от {MinLength} до {MaxLength}");

        RuleFor(executor => executor.Occupation)
            .NotNull().WithMessage("Должность не может быть пустым")
            .Length(MinLength, MaxLength).WithMessage($"Длина должности должна быть от {MinLength} до {MaxLength}");

        RuleFor(executor => executor.Firm)
            .NotNull().WithMessage("Фирма не может быть пустым")
            .Length(MinLength, MaxLength).WithMessage($"Длина фирмы должна быть от {MinLength} до {MaxLength}");

        RuleFor(executor => executor.OGRN)
            .NotNull().WithMessage("ОГРН не может быть пустым")
            .Length(OGRNLength).WithMessage($"ОГРН должен быть длиной {OGRNLength} цифр");
    }
}

