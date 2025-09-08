using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;

namespace CasCadeVR.Works.Services.Contracts.Models.Works;

/// <summary>
/// Модель работы
/// </summary>
public class WorksModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

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
    /// Объект передачи данных <see cref="UnitOfMeasure"/>
    /// </summary>
    public UnitOfMeasureModel UnitOfMeasure { get; set; } = null!;
}