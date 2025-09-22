namespace CasCadeVR.Works.Web.Models.Customers;

/// <summary>
/// Модель заказчика
/// </summary>
public class CustomerApiModel : CustomerCreateRequestApiModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}
