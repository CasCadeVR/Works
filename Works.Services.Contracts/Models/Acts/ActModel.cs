using CasCadeVR.Works.Services.Contracts.Models.ActWorks;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Contracts.Models.Executors;

namespace CasCadeVR.Works.Services.Contracts.Models.Acts;

/// <summary>
/// Модель акта
/// </summary>
public class ActModel : ActGenericModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Объект передачи данных <see cref="ExecutorModel"/>
    /// </summary>
    public ExecutorModel Executor { get; set; } = null!;

    /// <summary>
    /// Объект передачи данных <see cref="CustomerModel"/>
    /// </summary>
    public CustomerModel Customer { get; set; } = null!;

    /// <summary>
    /// Объект передачи данных списка <see cref="ActWorksModel"/>
    /// </summary>
    public ICollection<ActWorksModel> ActWorks { get; set; } = null!;
}