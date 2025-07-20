namespace Works.Web.Contracts.Models.Executors;

/// <summary>
/// Модель создания исполнителя
/// </summary>
/// <param name="FIO">ФИО</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="OGRN">ОГРН</param>
public record ExecutorRequestApiModel(
    string FIO, 
    string Occupation, 
    string Firm, 
    string OGRN
);