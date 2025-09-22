namespace CasCadeVR.Works.Repository.Contracts.Models;

/// <summary>
/// Модель работы для акта для запроса из базы данных
/// </summary>
public class ActWorkDbModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор акта
    /// </summary>
    public Guid ActId { get; set; }

    /// <summary>
    /// Наименование работы
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Цена работы на момент создания акта
    /// </summary>
    public decimal CapturedPrice { get; set; } = 0;

    /// <summary>
    /// Навигационное свойство <see cref="WorkDbModel"/>
    /// </summary>
    public WorkDbModel Work { get; set; } = null!;
}
