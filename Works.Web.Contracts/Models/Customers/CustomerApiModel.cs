namespace Works.Web.Contracts.Models.Customers;

/// <summary>
/// Модель заказчика
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FIO">ФИО</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="INN">ИНН</param>
public record CustomerApiModel(
    Guid Id, 
    string FIO, 
    string Occupation, 
    string Firm, 
    string INN
);
