namespace CasCadeVR.Works.Web.Models.Executors;

/// <summary>
/// Модель исполнителя
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FullName">ФИО</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="RegistrationNumber">ОГРН</param>
public record ExecutorApiModel(
    Guid Id, 
    string FullName, 
    string Occupation, 
    string Firm, 
    string RegistrationNumber
);
