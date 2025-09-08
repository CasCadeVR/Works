namespace CasCadeVR.Works.Web.Models.Customers;

/// <summary>
/// Модель заказчика
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FullName">ФИО</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="TaxPayerId">ИНН</param>
public record CustomerApiModel(
    Guid Id, 
    string FullName, 
    string Occupation, 
    string Firm, 
    string TaxPayerId
) : CustomerCreateRequestApiModel(FullName, Occupation, Firm, TaxPayerId);
