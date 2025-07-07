using FluentValidation;
using Works.Services.Contracts.Models.Customers;

namespace Works.Services.Validators.Customers;

/// <summary>
/// Валидация <see cref="CustomerModel"/>
/// </summary>
public class CustomerModelValidator : AbstractValidator<CustomerModel>
{
    private const int MinLength = 3;
    private const int MaxLength = 255;
    private const int INNLength = 16;

    /// <summary>
    /// ctor
    /// </summary>
    public CustomerModelValidator()
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
            .Length(INNLength).WithMessage($"ИНН должен быть длиной {INNLength}");
    }
}
