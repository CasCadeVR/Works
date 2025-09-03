namespace CasCadeVR.Works.Services.Contracts.Models.Works;

/// <summary>
/// Модель создания работы
/// </summary>
public class WorksCreateModel
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

    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public Guid UnitOfMeasureId { get; set; }
}