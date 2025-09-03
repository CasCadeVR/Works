namespace CasCadeVR.Works.Entities.ValidationRules;

/// <summary>
/// Правила для валидации <see cref="Executor"/>
/// </summary>
public static class ExecutorValidationRules
{
    /// <summary>
    /// Минимальная длина <see cref="Executor.FullName"/>
    /// </summary>
    public const int FullNameMinLength = 3;

    /// <summary>
    /// Максимальная длина <see cref="Executor.FullName"/>
    /// </summary>
    public const int FullNameMaxLength = 255;

    /// <summary>
    /// Минимальная длина <see cref="Executor.Firm"/>
    /// </summary>
    public const int FirmMinLength = 3;

    /// <summary>
    /// Максимальная длина <see cref="Executor.Firm"/>
    /// </summary>
    public const int FirmMaxLength = 255;

    /// <summary>
    /// Минимальная длина <see cref="Executor.Occupation"/>
    /// </summary>
    public const int OccupationMinLength = 3;

    /// <summary>
    /// Максимальная длина <see cref="Executor.Occupation"/>
    /// </summary>
    public const int OccupationMaxLength = 255;

    /// <summary>
    /// Длина <see cref="Executor.RegistrationNumber"/>
    /// </summary>
    public const int RegistrationNumberLength = 13;

    /// <summary>
    /// Максимальная длина <see cref="Executor.RegistrationNumber"/>
    /// </summary>
    public const int RegistrationMaxLength = 255;
}
