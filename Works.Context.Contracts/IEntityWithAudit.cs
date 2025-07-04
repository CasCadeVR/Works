namespace Works.Context.Contracts;

/// <summary>
/// Сущность с аудитом
/// </summary>
public interface IEntityWithAudit
{
    /// <summary>
    /// Дата создания записи
    /// </summary>
    DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Дата изменения записи
    /// </summary>
    DateTimeOffset UpdatedAt { get; set; }
}
