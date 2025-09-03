using System.Linq.Expressions;

namespace CasCadeVR.Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Customer"/>
/// </summary>
public interface ICustomerReadRepository
{
    /// <summary>
    /// Возвращает true, если совпадает условие
    /// </summary>
    Task<bool> Any(Expression<Func<Entities.Customer, bool>> action, CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="Entities.Customer"/> по идентификатору
    /// </summary>
    Task<Entities.Customer?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Entities.Customer"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Customer>> GetAll(CancellationToken cancellationToken);
}

