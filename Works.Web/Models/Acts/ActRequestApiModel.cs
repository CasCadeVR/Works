using Works.Web.Models.ActWorks;

namespace Works.Web.Models.Acts;

/// <summary>
/// Модель создания акта
/// </summary>
/// <param name="Date">Дата подписания акта</param>
/// <param name="ActNumber">Номер акта</param>
/// <param name="ExecutorId">Идентификатор исполнителя</param>
/// <param name="CustomerId">Идентификатор заказчика</param>
/// <param name="Works">Список работ</param>
/// <param name="NDS">НДС</param>
public record ActRequestApiModel(
    string ActNumber, 
    DateTime Date, 
    Guid ExecutorId, 
    Guid CustomerId, 
    ICollection<ActWorksRequestApiModel> Works, 
    decimal NDS
);
