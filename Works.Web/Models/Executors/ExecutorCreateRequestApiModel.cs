namespace CasCadeVR.Works.Web.Models.Executors;

/// <summary>
/// Модель создания исполнителя
/// </summary>
/// <param name="FullName">ФИО</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="RegistrationNumber">ОГРН</param>
public record ExecutorCreateRequestApiModel(
    string FullName, 
    string Occupation, 
    string Firm, 
    string RegistrationNumber
);