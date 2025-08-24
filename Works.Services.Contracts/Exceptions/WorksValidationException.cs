namespace CasCadeVR.Works.Services.Contracts.Exceptions;

/// <summary>
/// Ошибка при непрохождении валидации
/// </summary>
public class WorksValidationException : WorksException
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksValidationException"/>
    /// </summary>
    public WorksValidationException(IEnumerable<InvalidateItemModel> errors)
    {
        Errors = errors;
    }

    /// <summary>
    /// Список неверных полей
    /// </summary>
    public IEnumerable<InvalidateItemModel> Errors { get; private set; }
}
