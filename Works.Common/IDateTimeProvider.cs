namespace CasCadeVR.Works.Common;

/// <summary>
/// Поставщик времени 
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Текущее время по UTC
    /// </summary>
    DateTimeOffset UtcNow();

    /// <summary>
    /// Текущее время
    /// </summary>
    DateTimeOffset Now();
}
