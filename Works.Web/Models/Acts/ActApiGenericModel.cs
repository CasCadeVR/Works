namespace CasCadeVR.Works.Web.Models.Acts;

/// <summary>
/// Общая модель создания акта
/// </summary>
public abstract class ActApiGenericModel
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