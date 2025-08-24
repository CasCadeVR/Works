namespace CasCadeVR.Works.Web.Contracts.Models.Customers;

/// <summary>
/// Модель создания заказчика
/// </summary>
/// <param name="FIO">ФИО</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="INN">ИНН</param>
public record CustomerRequestApiModel(
    string FIO, 
    string Occupation, 
    string Firm, 
    string INN
);