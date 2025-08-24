namespace CasCadeVR.Works.Services.Contracts.Exceptions;

/// <summary>
/// Общая ошибка для работ
/// </summary>
public class WorksException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksException"/>
    /// </summary>
    protected WorksException() { }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksException"/> c аргументом для поля, в котором возникла ошибка
    /// </summary>
    protected WorksException(string field)
        => new WorksException(field);
}