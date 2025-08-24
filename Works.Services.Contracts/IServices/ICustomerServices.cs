using CasCadeVR.Works.Services.Contracts.Models.Customers;

namespace CasCadeVR.Works.Services.Contracts.IServices;

/// <summary>
/// Сервис по работе с заказчиками
/// </summary>
public interface ICustomerServices
{
    /// <summary>
    /// Возвращает <see cref="CustomerModel"/> по идентификатору
    /// </summary>
    Task<CustomerModel> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает список <see cref="CustomerModel"/>
    /// </summary>
    Task<IReadOnlyCollection<CustomerModel>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новый <see cref="CustomerModel"/>
    /// </summary>
    Task<CustomerModel> Create(CustomerCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Редактирует существующий <see cref="CustomerModel"/>
    /// </summary>
    Task<CustomerModel> Update(CustomerModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет существующий <see cref="CustomerModel"/>
    /// </summary>
    Task Delete(Guid id, CancellationToken cancellationToken);
}