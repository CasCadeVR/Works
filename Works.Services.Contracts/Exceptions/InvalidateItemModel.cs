namespace CasCadeVR.Works.Services.Contracts.Exceptions;

/// <summary>
/// Модель о возникщей ошибке
/// </summary>
public class InvalidateItemModel
{
    /// <summary>
    /// Статический конструктор
    /// </summary>
    public static InvalidateItemModel New(string field, string message)
        => new InvalidateItemModel(field, message);

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="InvalidateItemModel"/>
    /// </summary>
    public InvalidateItemModel(string field, string message)
    {
        Field = field;
        Message = message;
    }

    /// <summary>
    /// Поле
    /// </summary>
    public string Field { get; } =string.Empty;

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string Message { get; } = string.Empty;
}
