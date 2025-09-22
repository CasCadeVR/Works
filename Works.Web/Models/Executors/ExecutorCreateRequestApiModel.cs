namespace CasCadeVR.Works.Web.Models.Executors;

/// <summary>
/// Модель создания исполнителя
/// </summary>
public class ExecutorCreateRequestApiModel
{
    /// <summary>
    /// ФИО
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Должность
    /// </summary>
    public string Occupation { get; set; } = string.Empty;

    /// <summary>
    /// Фирма
    /// </summary>
    public string Firm { get; set; } = string.Empty;

    /// <summary>
    /// ОГРН
    /// </summary>
    public string RegistrationNumber { get; set; } = string.Empty;
}