using CasCadeVR.Works.Web.Models.ActWorks;
using CasCadeVR.Works.Web.Models.Customers;
using CasCadeVR.Works.Web.Models.Executors;

namespace CasCadeVR.Works.Web.Models.Acts;

/// <summary>
/// Модель создания акта
/// </summary>
public class ActApiModel : ActApiGenericModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Объект передачи данных <see cref="ExecutorApiModel"/>
    /// </summary>
    public ExecutorApiModel Executor { get; set; } = null!;

    /// <summary>
    /// Объект передачи данных <see cref="CustomerApiModel"/>
    /// </summary>
    public CustomerApiModel Customer { get; set; } = null!;

    /// <summary>
    /// Объект передачи данных списка <see cref="ActWorksApiModel"/>
    /// </summary>
    public ICollection<ActWorksApiModel> ActWorks { get; set; } = [];
}