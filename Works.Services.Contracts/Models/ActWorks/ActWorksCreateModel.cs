namespace Works.Services.Contracts.Models.ActWorks;

/// <summary>
/// Модель создания работы для акта
/// </summary>
public class ActWorksCreateModel()
{
    /// <summary>
    /// Идентификатор работы
    /// </summary>
    public Guid WorkId { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Актуальная цена
    /// </summary>
    public decimal ActualPrice { get; set; }
}