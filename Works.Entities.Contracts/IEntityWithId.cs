namespace CasCadeVR.Works.Entities.Contracts;

/// <summary>
/// Сущность с идентификатором
/// </summary>
public interface IEntityWithId
{
    /// <summary>
    /// Идентифактор
    /// </summary>
    Guid Id { get; set; }
}