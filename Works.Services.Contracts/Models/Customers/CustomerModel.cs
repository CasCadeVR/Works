namespace CasCadeVR.Works.Services.Contracts.Models.Customers;

/// <summary>
/// Модель заказчика
/// </summary>
public class CustomerModel : CustomerCreateModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}