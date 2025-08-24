namespace CasCadeVR.Works.Services.Contracts.Models.Works;

/// <summary>
/// Модель работы
/// </summary>
public class WorksModel()
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Именования
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public string UnitOfMeasure { get; set; } = string.Empty;
}