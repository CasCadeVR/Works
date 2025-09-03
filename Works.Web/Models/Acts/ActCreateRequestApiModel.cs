using CasCadeVR.Works.Web.Models.ActWorks;

namespace CasCadeVR.Works.Web.Models.Acts;

/// <summary>
/// Модель создания акта
/// </summary>
/// <param name="Date">Дата подписания акта</param>
/// <param name="ActNumber">Номер акта</param>
/// <param name="ExecutorId">Идентификатор исполнителя</param>
/// <param name="CustomerId">Идентификатор заказчика</param>
/// <param name="ActWorks">Список работ</param>
public record ActCreateRequestApiModel(
    string ActNumber,
    DateOnly Date, 
    Guid ExecutorId, 
    Guid CustomerId, 
    ICollection<ActWorksCreateRequestApiModel> ActWorks
);
