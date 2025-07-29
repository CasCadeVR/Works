using Works.Web.Contracts.Models.ActWorks;

namespace Works.Web.Contracts.Models.Acts;

/// <summary>
/// Модель создания акта
/// </summary>
/// <param name="Id">Идентификатор акта</param>
/// <param name="Date">Дата подписания акта</param>
/// <param name="ActNumber">Номер акта</param>
/// <param name="ExecutorId">Идентификатор исполнителя</param>
/// <param name="ExecutorFIO">ФИО исполнителя</param>
/// <param name="ExecutorOccupation">Должность исполнителя</param>
/// <param name="ExecutorFirm">Фирма исполнителя</param>
/// <param name="ExecutorOGRN">ОГРН исполнителя</param>
/// <param name="CustomerId">Идентификатор заказчика</param>
/// <param name="CustomerFIO">ФИО заказчика</param>
/// <param name="CustomerOccupation">Должность заказчика</param>
/// <param name="CustomerFirm">Фирма заказчика</param>
/// <param name="CustomerINN">ИНН заказчика</param>
/// <param name="Works">Список работ</param>
/// <param name="TotalPrice">Полная сумма без НДС</param>
/// <param name="NDS">НДС</param>
/// <param name="PriceNDS">сумма НДС</param>
/// <param name="TotalPriceNDS">Полная сумма с НДС</param>
public record ActApiModel(
    Guid Id,
    string ActNumber,
    DateTime Date,
    Guid ExecutorId,
    string ExecutorFIO,
    string ExecutorOccupation,
    string ExecutorFirm,
    string ExecutorOGRN,
    Guid CustomerId,
    string CustomerFIO,
    string CustomerOccupation,
    string CustomerFirm,
    string CustomerINN,
    ICollection<ActWorksApiModel> Works,
    decimal TotalPrice,
    decimal NDS,
    decimal PriceNDS,
    decimal TotalPriceNDS
);