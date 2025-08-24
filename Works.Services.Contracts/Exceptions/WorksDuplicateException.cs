namespace CasCadeVR.Works.Services.Contracts.Exceptions;

/// <summary>
/// Ошибка о дубликате
/// </summary>
public class WorksDuplicateException : WorksException
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksDuplicateException"/>
    /// </summary>
    public WorksDuplicateException(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public override string Message { get; } = string.Empty;
}