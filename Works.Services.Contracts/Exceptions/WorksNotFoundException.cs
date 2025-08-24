namespace CasCadeVR.Works.Services.Contracts.Exceptions;

/// <summary>
/// Ошибка о ненахождении
/// </summary>
public class WorksNotFoundException : WorksException
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksNotFoundException"/>
    /// </summary>
    public WorksNotFoundException(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public override string Message { get; } = string.Empty;
}