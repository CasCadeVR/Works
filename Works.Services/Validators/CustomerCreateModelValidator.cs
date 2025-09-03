using FluentValidation;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Entities.ValidationRules;

namespace CasCadeVR.Works.Services.Validators;

/// <summary>
/// Валидация <see cref="CustomerCreateModel"/>
/// </summary>
public class CustomerCreateModelValidator : AbstractValidator<CustomerCreateModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CustomerCreateModelValidator"/>
    /// </summary>
    public CustomerCreateModelValidator()
    {
        RuleFor(customer => customer.FullName)
            .NotEmpty().WithMessage("ФИО не может быть пустым")
            .Length(CustomerValidationRules.FullNameMinLength, CustomerValidationRules.FullNameMaxLength)
            .WithMessage($"Длина ФИО должна быть от {CustomerValidationRules.FullNameMinLength} до {CustomerValidationRules.FullNameMaxLength}");

        RuleFor(customer => customer.Occupation)
            .NotNull().WithMessage("Должность не может быть пустой")
            .Length(CustomerValidationRules.OccupationMinLength, CustomerValidationRules.OccupationMaxLength)
            .WithMessage($"Длина должности должна быть от {CustomerValidationRules.OccupationMinLength} до {CustomerValidationRules.OccupationMaxLength}");

        RuleFor(customer => customer.Firm)
            .NotNull().WithMessage("Фирма не может быть пустой")
            .Length(CustomerValidationRules.FirmMinLength, CustomerValidationRules.FirmMaxLength)
            .WithMessage($"Длина фирмы должна быть от {CustomerValidationRules.FirmMinLength} до {CustomerValidationRules.FirmMaxLength}");

        RuleFor(customer => customer.TaxPayerId)
            .NotNull().WithMessage("ИНН не может быть пустым")
            .Must(x => 
                x.Length == CustomerValidationRules.TaxPayerIdLengthForIndividuals
                || x.Length == CustomerValidationRules.TaxPayerIdLengthForLegalEntities)
            .WithMessage($"ИНН должен быть длиной для физических лиц {CustomerValidationRules.TaxPayerIdLengthForIndividuals} цифр, а для юридических — {CustomerValidationRules.TaxPayerIdLengthForLegalEntities} цифр");
    }
}

