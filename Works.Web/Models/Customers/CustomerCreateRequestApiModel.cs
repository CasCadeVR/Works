namespace CasCadeVR.Works.Web.Models.Customers;

/// <summary>
/// Модель создания заказчика
/// </summary>
public class CustomerCreateRequestApiModel
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
    /// ИНН
    /// </summary>
    public string TaxPayerId { get; set; } = string.Empty;
}