namespace CasCadeVR.Works.Entities.ValidationRules;

/// <summary>
/// Правила для валидации <see cref="Work"/>
/// </summary>
public static class WorkValidationRules
{
    /// <summary>
    /// Минимальная длина <see cref="Work.Name"/>
    /// </summary>
    public const int NameMinLength = 3;

    /// <summary>
    /// Максимальная длина <see cref="Work.Name"/>
    /// </summary>
    public const int NameMaxLength = 255;

    /// <summary>
    /// Максимальная длина <see cref="Work.UnitOfMeasure"/>
    /// </summary>
    public const int UnitOfMeasureMaxLength = 255;
}
