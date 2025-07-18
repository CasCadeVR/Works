namespace Works.Services.Contracts.Models.Works;

/// <summary>
/// Модель создания работы
/// </summary>
public record WorksCreateModel()
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
    /// Единица измерения
    /// </summary>
    public string UnitOfMeasure { get; set; } = string.Empty;
}