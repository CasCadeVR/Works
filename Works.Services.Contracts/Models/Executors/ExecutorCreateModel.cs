namespace Works.Services.Contracts.Models.Executors;

/// <summary>
/// Модель создания исполнителя
/// </summary>
public record ExecutorCreateModel()
{
    /// <summary>
    /// ФИО
    /// </summary>
    public string FIO { get; set; } = string.Empty;

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
    public string OGRN { get; set; } = string.Empty;
}