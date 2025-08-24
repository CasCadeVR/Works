namespace CasCadeVR.Works.Entities.Contracts;

/// <summary>
/// Сущность с "мягким" удалением
/// </summary>
public interface IEntitySoftDeleted
{
    /// <summary>
    /// Дата удаления
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
}
