namespace CasCadeVR.Works.Entities.Contracts;

/// <summary>
/// Сущность для базы данных с идентификатором, аудитом и "мягким" удалением
/// </summary>
public class DataBaseEntity : IEntityWithId, IEntityWithAudit, IEntitySoftDeleted
{
    /// <inheritdoc cref="IEntityWithId.Id"/>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <inheritdoc cref="IEntityWithAudit.CreatedAt"/>
    public DateTimeOffset CreatedAt { get; set; }

    /// <inheritdoc cref="IEntityWithAudit.UpdatedAt"/>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <inheritdoc cref="IEntitySoftDeleted.DeletedAt"/>
    public DateTimeOffset? DeletedAt { get; set; }
}
