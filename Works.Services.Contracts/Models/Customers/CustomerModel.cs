namespace Works.Services.Contracts.Models.Customers;

/// <summary>
/// Модель заказчика
/// </summary>
public class CustomerModel()
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

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