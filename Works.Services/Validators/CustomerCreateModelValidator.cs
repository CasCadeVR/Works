using FluentValidation;
using CasCadeVR.Works.Services.Contracts.Models.Customers;

namespace CasCadeVR.Works.Services.Validators;

/// <summary>
/// Валидация <see cref="CustomerCreateModel"/>
/// </summary>
public class CustomerCreateModelValidator : AbstractValidator<CustomerCreateModel>
{
    private const int MinLength = 3;
    private const int MaxLength = 255;
    private const int INNMinLength = 10;
    private const int INNMaxLength = 12;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CustomerCreateModelValidator"/>
    /// </summary>
    public CustomerCreateModelValidator()
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

        RuleFor(customer => customer.INN)
            .NotNull().WithMessage("ИНН не может быть пустым")
            .Must(HaveValidLength).WithMessage($"ИНН должен быть длиной для физических лиц {INNMaxLength} цифр, а для юридических — из {INNMinLength}");
    }

    private bool HaveValidLength(string inn) => inn.Length == INNMinLength || inn.Length == INNMaxLength;
}

