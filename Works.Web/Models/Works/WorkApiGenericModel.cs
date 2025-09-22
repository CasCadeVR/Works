namespace CasCadeVR.Works.Web.Models.Works;

/// <summary>
/// Общая модель работы
/// </summary>
public abstract class WorkApiGenericModel
{
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; } = 0;
}