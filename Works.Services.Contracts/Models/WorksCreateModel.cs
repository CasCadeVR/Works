namespace Works.Services.Contracts.Models;

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
}