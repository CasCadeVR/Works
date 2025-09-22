namespace CasCadeVR.Works.Services.Contracts.Models.Acts;

/// <summary>
/// Общая модель акта
/// </summary>
abstract public class ActGenericModel
{
    /// <summary>
    /// Номер акта
    /// </summary>
    public string ActNumber { get; set; } = string.Empty;

    /// <summary>
    /// Дата подписания
    /// </summary>
    public DateOnly Date { get; set; }
}