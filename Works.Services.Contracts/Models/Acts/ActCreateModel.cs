using CasCadeVR.Works.Services.Contracts.Models.ActWorks;

namespace CasCadeVR.Works.Services.Contracts.Models.Acts;

/// <summary>
/// Модель акта
/// </summary>
public class ActCreateModel : ActGenericModel
{
    /// <summary>
    /// Идентификатор исполнителя
    /// </summary>
    public Guid ExecutorId { get; set; }

    /// <summary>
    /// Идентификатор заказчика
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Объект передачи данных списка <see cref="ActWorksCreateModel"/>
    /// </summary>
    public ICollection<ActWorksCreateModel> ActWorks { get; set; } = [];
}