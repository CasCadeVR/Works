namespace Works.Services.Contracts.Exceptions;

/// <summary>
/// Ошибка о дубликате
/// </summary>
public class WorksDuplicateException : WorksException
{
    /// <summary>
    /// ctor
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