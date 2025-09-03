using CasCadeVR.Works.Entities.Contracts;

namespace CasCadeVR.Works.Entities;

/// <summary>
/// Сущность акта
/// </summary>
public class Act : DataBaseEntity
{
    /// <summary>
    /// Номер акта
    /// </summary>
    public string ActNumber { get; set; } = string.Empty;

    /// <summary>
    /// Дата подписания
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Идентификатор <see cref="Executor"/>
    /// </summary>
    public Guid ExecutorId { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="Executor"/>
    /// </summary>
    public Executor Executor { get; set; } = null!;

    /// <summary>
    /// Идентификатор <see cref="Customer"/>
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="Customer"/>
    /// </summary>
    public Customer Customer { get; set; } = null!;

    /// <summary>
    /// Навигационное свойство списка <see cref="ActWork"/>
    /// </summary>
    public ICollection<ActWork> ActWorks { get; set; } = null!;
}