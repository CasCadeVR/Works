namespace Works.Web.Contracts.Models.Executors;

/// <summary>
/// Модель исполнителя
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FIO">ФИО</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="OGRN">ОГРН</param>
public record ExecutorApiModel(
    Guid Id, 
    string FIO, 
    string Occupation, 
    string Firm, 
    string OGRN
);
