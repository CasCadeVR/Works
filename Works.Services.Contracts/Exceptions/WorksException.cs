namespace Works.Services.Contracts.Exceptions;

/// <summary>
/// Общая ошибка для работ
/// </summary>
public class WorksException : Exception
{
    /// <summary>
    /// ctor
    /// </summary>
    protected WorksException() { }

    /// <summary>
    /// ctor
    /// </summary>
    protected WorksException(string field)
        => new WorksException(field);
}