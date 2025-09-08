using FluentValidation;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Entities.ValidationRules;

namespace CasCadeVR.Works.Services.Validators;

/// <summary>
/// Валидация <see cref="ExecutorCreateModel"/>
/// </summary>
public class ExecutorCreateModelValidator : AbstractValidator<ExecutorCreateModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ExecutorCreateModelValidator"/>
    /// </summary>
    public ExecutorCreateModelValidator()
    {
        RuleFor(executor => executor.FullName)
            .NotEmpty().WithMessage("ФИО не может быть пустыми")
            .Length(ExecutorValidationRules.FullNameMinLength, ExecutorValidationRules.FullNameMaxLength)
            .WithMessage($"Длина ФИО должна быть от {ExecutorValidationRules.FullNameMinLength} до {ExecutorValidationRules.FullNameMaxLength}");

        RuleFor(executor => executor.Occupation)
            .NotNull().WithMessage("Должность не может быть пустой")
            .Length(ExecutorValidationRules.OccupationMinLength, ExecutorValidationRules.OccupationMaxLength)
            .WithMessage($"Длина должности должна быть от {ExecutorValidationRules.OccupationMinLength} до {ExecutorValidationRules.OccupationMaxLength}");

        RuleFor(executor => executor.Firm)
            .NotNull().WithMessage("Фирма не может быть пустой")
            .Length(ExecutorValidationRules.FirmMinLength, ExecutorValidationRules.FirmMaxLength)
            .WithMessage($"Длина фирмы должна быть от {ExecutorValidationRules.FirmMinLength} до {ExecutorValidationRules.FirmMaxLength}");

        RuleFor(executor => executor.RegistrationNumber)
            .NotNull().WithMessage("ОГРН не может быть пустым")
            .Length(ExecutorValidationRules.RegistrationNumberLength)
            .WithMessage($"ОГРН должен быть длиной {ExecutorValidationRules.RegistrationNumberLength} цифр");
    }
}

