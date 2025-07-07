namespace Works.Services.Contracts.Exceptions;

/// <summary>
/// Ошибка работы о неправильной работе внутренней операции (операции сервера)
/// </summary>
public class WorksInvalidOperationException : WorksException
{
    /// <summary>
    /// ctor
    /// </summary>
    public WorksInvalidOperationException(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public override string Message { get; } = string.Empty;
}