namespace Works.Context.Contracts;

/// <summary>
/// Сущность с удалением
/// </summary>
public interface IEntitySoftDeleted
{
    /// <summary>
    /// Дата удалени
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
}
