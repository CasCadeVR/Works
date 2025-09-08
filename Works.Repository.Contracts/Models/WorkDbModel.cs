using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Entities.Contracts;

namespace CasCadeVR.Works.Repository.Contracts.Models;

/// <summary>
/// Модель работы для запроса из базы данных
/// </summary>
public class WorkDbModel : IEntityWithId
{
    /// <inheritdoc cref="IEntityWithId"/>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование работы
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание работы
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Цена работы за 1 единицу измерения
    /// </summary>
    public decimal Price { get; set; } = 0;

    /// <summary>
    /// Навигационное свойство <see cref="UnitOfMeasure"/>
    /// </summary>
    public UnitOfMeasure UnitOfMeasure { get; set; } = null!;
}
