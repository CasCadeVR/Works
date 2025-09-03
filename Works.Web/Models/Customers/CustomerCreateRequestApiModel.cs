namespace CasCadeVR.Works.Web.Models.Customers;

/// <summary>
/// Модель создания заказчика
/// </summary>
/// <param name="FullName">ФИО</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="TaxPayerId">ИНН</param>
public record CustomerCreateRequestApiModel(
    string FullName, 
    string Occupation, 
    string Firm, 
    string TaxPayerId
);