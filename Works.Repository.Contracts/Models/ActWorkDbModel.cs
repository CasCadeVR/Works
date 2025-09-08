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
    /// Наименование работы
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="WorkDbModel"/>
    /// </summary>
    public WorkDbModel Work { get; set; } = null!;
}
