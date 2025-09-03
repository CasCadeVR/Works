namespace CasCadeVR.Works.Web.Contracts.Models.Customers;

/// <summary>
/// Модель заказчика
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FullName">Фамилия, имя, отчество</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="TaxpayerId">ИНН</param>
public record CustomerApiModel(
    Guid Id, 
    string FullName, 
    string Occupation, 
    string Firm, 
    string TaxpayerId
);
