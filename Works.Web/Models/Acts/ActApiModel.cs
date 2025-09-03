using CasCadeVR.Works.Web.Models.ActWorks;
using CasCadeVR.Works.Web.Models.Customers;
using CasCadeVR.Works.Web.Models.Executors;

namespace CasCadeVR.Works.Web.Models.Acts;

/// <summary>
/// Модель создания акта
/// </summary>
/// <param name="Id">Идентификатор акта</param>
/// <param name="Date">Дата подписания акта</param>
/// <param name="ActNumber">Номер акта</param>
/// <param name="Executor">Исполнитель</param>
/// <param name="Customer">Заказчик</param>
/// <param name="ActWorks">Список работ</param>
public record ActApiModel(
    Guid Id,
    string ActNumber,
    DateOnly Date,
    ExecutorApiModel Executor,
    CustomerApiModel Customer,
    ICollection<ActWorksApiModel> ActWorks
);