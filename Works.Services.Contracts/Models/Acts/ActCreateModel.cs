using CasCadeVR.Works.Services.Contracts.Models.ActWorks;

namespace CasCadeVR.Works.Services.Contracts.Models.Acts;

/// <summary>
/// Модель акта
/// </summary>
public class ActCreateModel
{
    /// <summary>
    /// Номер акта
    /// </summary>
    public string ActNumber { get; set; } = string.Empty;

    /// <summary>
    /// Дата подписания
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Идентификатор исполнителя
    /// </summary>
    public Guid ExecutorId { get; set; }

    /// <summary>
    /// Идентификатор заказчика
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Список работ <see cref="ActWorksCreateModel"/>
    /// </summary>
    public ICollection<ActWorksCreateModel> ActWorks { get; set; } = new List<ActWorksCreateModel>();
}