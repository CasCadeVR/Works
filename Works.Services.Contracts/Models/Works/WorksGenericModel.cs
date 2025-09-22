namespace CasCadeVR.Works.Services.Contracts.Models.Works;

/// <summary>
/// Общая модель работы
/// </summary>
abstract public class WorksGenericModel
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
