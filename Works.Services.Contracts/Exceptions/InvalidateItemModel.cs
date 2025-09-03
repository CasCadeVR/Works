namespace CasCadeVR.Works.Services.Contracts.Exceptions;

/// <summary>
/// Модель о возникшей ошибке
/// </summary>
public class InvalidateItemModel
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="InvalidateItemModel"/>
    /// </summary>
    public InvalidateItemModel(string field, string message)
    {
        Field = field;
        Message = message;
    }

    /// <summary>
    /// Название поля
    /// </summary>
    public string Field { get; }

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string Message { get; }
}
