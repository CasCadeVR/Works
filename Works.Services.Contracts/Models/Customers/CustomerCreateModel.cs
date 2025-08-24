namespace CasCadeVR.Works.Services.Contracts.Models.Customers;

/// <summary>
/// Модель создания заказчика
/// </summary>
public record CustomerCreateModel()
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
    /// ИНН
    /// </summary>
    public string INN { get; set; } = string.Empty;
}