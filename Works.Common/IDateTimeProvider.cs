namespace Works.Common;

/// <summary>
/// Поставщик времени 
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// 
    /// </summary>
    DateTimeOffset UtcNow();

    /// <summary>
    /// 
    /// </summary>
    DateTimeOffset Now();
}
