using Works.Services.Contracts.Models.Works;

namespace Works.Services.Contracts.Models.ActWorks;

/// <summary>
/// Модель работы для акта
/// </summary>
public class ActWorksModel()
{
    /// <inheritdoc cref="WorksModel"/>
    public WorksModel Work { get; set; } = new WorksModel();

    /// <summary>
    /// Количество
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Актуальная цена
    /// </summary>
    public decimal ActualPrice { get; set; }

    /// <summary>
    /// Общая сумма (до НДС)
    /// </summary>
    public decimal TotalPrice => ActualPrice * Quantity;
}