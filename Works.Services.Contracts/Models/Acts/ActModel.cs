using CasCadeVR.Works.Services.Contracts.Models.ActWorks;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Contracts.Models.Executors;

namespace CasCadeVR.Works.Services.Contracts.Models.Acts;

/// <summary>
/// Модель акта
/// </summary>
public class ActModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

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