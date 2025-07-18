using Works.Services.Contracts.Models.ActWorks;

namespace Works.Services.Contracts.Models.Acts;

/// <summary>
/// Модель акта
/// </summary>
public class ActCreateModel()
{
    /// <summary>
    /// Номер акта
    /// </summary>
    public string ActNumber { get; set; } = string.Empty;

    /// <summary>
    /// Дата подписания
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Идентификатор исполнителя
    /// </summary>
    public Guid ExecutorId { get; set; }

    /// <summary>
    /// Идентификатора заказчика
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Список работ <see cref="ActWorksCreateModel"/>
    /// </summary>
    public ICollection<ActWorksCreateModel> Works { get; set; } = new List<ActWorksCreateModel>();

    /// <summary>
    /// НДС (в процентах)
    /// </summary>
    public decimal NDS { get; set; }
}