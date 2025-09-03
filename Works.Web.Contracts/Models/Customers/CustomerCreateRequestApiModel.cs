namespace CasCadeVR.Works.Web.Contracts.Models.Customers;

/// <summary>
/// Модель создания заказчика
/// </summary>
/// <param name="FullName">Фамилия, имя, отчество</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="TaxpayerId">ИНН</param>
public record CustomerCreateRequestApiModel(
    string FullName, 
    string Occupation, 
    string Firm, 
    string TaxpayerId
);