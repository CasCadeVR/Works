namespace Works.Web.Models.ActWorks;

/// <summary>
/// Модель работы для акта
/// </summary>
/// <param name="Quantity">Количество</param>
/// <param name="ActualPrice">Актуальная цена</param>
/// <param name="TotalPrice">Полная сумма</param>
/// <param name="WorkId">Идентификатор работы</param>
/// <param name="WorkName">Именования</param>
/// <param name="WorkDescription">Описание</param>
/// <param name="WorkPrice">Цена</param>
/// <param name="WorkUnitOfMeasure">Единица измерения</param>
public record ActWorksApiModel(
    int Quantity, 
    decimal ActualPrice, 
    decimal TotalPrice, 
    Guid WorkId, 
    string WorkName, 
    string WorkDescription, 
    decimal WorkPrice, 
    string WorkUnitOfMeasure
);