using System.Linq.Expressions;
using CasCadeVR.Works.Entities;

namespace CasCadeVR.Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Customer"/>
/// </summary>
public interface ICustomerReadRepository
{
    /// <summary>
    /// Возвращает true, если совпадает условие
    /// </summary>
    Task<bool> Any(Expression<Func<Customer, bool>> action, CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="Customer"/> по идентификатору
    /// </summary>
    Task<Customer?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Customer"/>
    /// </summary>
    Task<IReadOnlyCollection<Customer>> GetAll(CancellationToken cancellationToken);
}

