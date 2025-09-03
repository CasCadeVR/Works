using CasCadeVR.Works.Web.Contracts.Models.ActWorks;

namespace CasCadeVR.Works.Web.Contracts.Models.Acts;

/// <summary>
/// Модель создания акта
/// </summary>
/// <param name="Date">Дата подписания акта</param>
/// <param name="ActNumber">Номер акта</param>
/// <param name="ExecutorId">Идентификатор исполнителя</param>
/// <param name="CustomerId">Идентификатор заказчика</param>
/// <param name="Works">Список работ</param>
/// <param name="AddedTax">НДС</param>
public record ActCreateRequestApiModel(
    string ActNumber,
    DateOnly Date, 
    Guid ExecutorId, 
    Guid CustomerId, 
    ICollection<ActWorksCreateRequestApiModel> Works, 
    decimal AddedTax
);
