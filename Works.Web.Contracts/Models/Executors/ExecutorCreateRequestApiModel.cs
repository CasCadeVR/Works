namespace CasCadeVR.Works.Web.Contracts.Models.Executors;

/// <summary>
/// Модель создания исполнителя
/// </summary>
/// <param name="FullName">Фамилия, имя, отчество</param>
/// <param name="Occupation">Должность</param>
/// <param name="Firm">Фирма</param>
/// <param name="RegistrationNumber">ОГРН</param>
public record ExecutorCreateRequestApiModel(
    string FullName, 
    string Occupation, 
    string Firm, 
    string RegistrationNumber
);