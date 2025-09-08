using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Entities.Contracts;

namespace CasCadeVR.Works.Repository.Contracts.Models;

/// <summary>
/// Модель акта для запроса из базы данных
/// </summary>
public class ActDbModel: IEntityWithId
{
    /// <summary>
    /// <inheritdoc cref="IEntityWithId"/>
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Номер акта
    /// </summary>
    public string ActNumber { get; set; } = string.Empty;

    /// <summary>
    /// Дата подписания
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="Executor"/>
    /// </summary>
    public Executor Executor { get; set; } = null!;

    /// <summary>
    /// Навигационное свойство <see cref="Customer"/>
    /// </summary>
    public Customer Customer { get; set; } = null!;

    /// <summary>
    /// Навигационное свойство списка <see cref="ActWorkDbModel"/>
    /// </summary>
    public ICollection<ActWorkDbModel> ActWorks { get; set; } = null!;
}
