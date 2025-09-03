namespace CasCadeVR.Works.Web.Models.ActWorks;

/// <summary>
/// Модель создания работы для акта
/// </summary>
/// <param name="WorkId">Идентификатор работы</param>
/// <param name="Quantity">Количество</param>
public record ActWorksCreateRequestApiModel(
    Guid WorkId, 
    int Quantity
);
