namespace CasCadeVR.Works.Entities.ValidationRules;

/// <summary>
/// Правила для валидации <see cref="Customer"/>
/// </summary>
public static class CustomerValidationRules
{
    /// <summary>
    /// Минимальная длина <see cref="Customer.FullName"/>
    /// </summary>
    public const int FullNameMinLength = 3;

    /// <summary>
    /// Максимальная длина <see cref="Customer.FullName"/>
    /// </summary>
    public const int FullNameMaxLength = 255;

    /// <summary>
    /// Минимальная длина <see cref="Customer.Firm"/>
    /// </summary>
    public const int FirmMinLength = 3;

    /// <summary>
    /// Максимальная длина <see cref="Customer.Firm"/>
    /// </summary>
    public const int FirmMaxLength = 255;

    /// <summary>
    /// Минимальная длина <see cref="Customer.Occupation"/>
    /// </summary>
    public const int OccupationMinLength = 3;

    /// <summary>
    /// Максимальная длина <see cref="Customer.Occupation"/>
    /// </summary>
    public const int OccupationMaxLength = 255;

    /// <summary>
    /// Длина <see cref="Customer.TaxPayerId"/> для физических лиц 
    /// </summary>
    public const int TaxPayerIdLengthForIndividuals = 12;

    /// <summary>
    /// Длина <see cref="Customer.TaxPayerId"/> для юридических лиц 
    /// </summary>
    public const int TaxPayerIdLengthForLegalEntities = 10;

    /// <summary>
    /// Максимальная длина <see cref="Customer.TaxPayerId"/>
    /// </summary>
    public const int TaxPayerIdMaxLength = 255;
}
