using Works.Services.Contracts.Models.ActWorks;
using Works.Services.Contracts.Models.Customers;
using Works.Services.Contracts.Models.Executors;

namespace Works.Services.Contracts.Models.Acts;

/// <summary>
/// Модель акта
/// </summary>
public class ActModel()
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
    public DateTime Date { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="ExecutorModel"/>
    /// </summary>
    public ExecutorModel Executor { get; set; } = new ExecutorModel();

    /// <summary>
    /// Навигационное свойство <see cref="CustomerModel"/>
    /// </summary>
    public CustomerModel Customer { get; set; } = new CustomerModel();

    /// <summary>
    /// Навигационное свойство списка <see cref="ActWorksModel"/>
    /// </summary>
    public ICollection<ActWorksModel> Works { get; set; } = new List<ActWorksModel>();

    /// <summary>
    /// Полная сумма (без ндс)
    /// </summary>
    public decimal TotalPrice => Works?.Sum(x => x.TotalPrice) ?? 0;

    /// <summary>
    /// НДС (в процентах)
    /// </summary>
    public decimal NDS { get; set; }

    /// <summary>
    /// Полная сумма c ндс
    /// </summary>
    public decimal TotalPriceNDS => TotalPrice * NDS;
}