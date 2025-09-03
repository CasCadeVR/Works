using CasCadeVR.Works.Entities.Contracts;

namespace CasCadeVR.Works.Entities;

/// <summary>
/// Сущность работы для акта
/// </summary>
public class ActWork : DataBaseEntity
{
    /// <summary>
    /// Идентификатор работы
    /// </summary>
    public Guid WorkId { get; set; }

    /// <summary>
    /// Идентификатор акта
    /// </summary>
    public Guid ActId { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="Work"/>
    /// </summary>
    public Work Work { get; set; } = null!;

    /// <summary>
    /// Навигационное свойство <see cref="Act"/>
    /// </summary>
    public Act Act { get; set; } = null!;
}