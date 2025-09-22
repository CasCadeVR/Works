using CasCadeVR.Works.Web.Models.ActWorks;

namespace CasCadeVR.Works.Web.Models.Acts;

/// <summary>
/// Модель создания акта
/// </summary>
public class ActCreateRequestApiModel : ActApiGenericModel
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
    /// Объект передачи данных списка <see cref="ActWorksCreateRequestApiModel"/>
    /// </summary>
    public ICollection<ActWorksCreateRequestApiModel> ActWorks { get; set; } = [];
}
