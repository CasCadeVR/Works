using CasCadeVR.Works.Entities.Contracts;

namespace CasCadeVR.Works.Entities;

/// <summary>
/// Сущность акта
/// </summary>
public class Act : IEntityWithId, IEntityWithAudit, IEntitySoftDeleted
{
    /// <inheritdoc cref="IEntityWithId.Id"/>
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
    /// Идентификатор <see cref="Executor"/>
    /// </summary>
    public Guid ExecutorId { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="Executor"/>
    /// </summary>
    public Executor Executor { get; set; } = new Executor();

    /// <summary>
    /// Идентификатора <see cref="Customer"/>
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="Customer"/>
    /// </summary>
    public Customer Customer { get; set; } = new Customer();

    /// <summary>
    /// Навигационное свойство списка <see cref="ActWork"/>
    /// </summary>
    public ICollection<ActWork> Works { get; set; } = new List<ActWork>();

    /// <summary>
    /// НДС (в процентах)
    /// </summary>
    public decimal NDS { get; set; }

    /// <inheritdoc cref="IEntityWithAudit.CreatedAt"/>
    public DateTimeOffset CreatedAt { get; set; }

    /// <inheritdoc cref="IEntityWithAudit.UpdatedAt"/>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <inheritdoc cref="IEntitySoftDeleted.DeletedAt"/>
    public DateTimeOffset? DeletedAt { get; set; }
}