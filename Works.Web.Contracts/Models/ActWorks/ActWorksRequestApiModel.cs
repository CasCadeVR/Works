namespace Works.Web.Contracts.Models.ActWorks;

/// <summary>
/// Модель создания работы для акта
/// </summary>
/// <param name="WorkId">Идентификатор работы</param>
/// <param name="Quantity">Количество</param>
/// <param name="ActualPrice">Актуальная цена</param>
public record ActWorksRequestApiModel(
    Guid WorkId, 
    int Quantity,
    decimal ActualPrice
);
