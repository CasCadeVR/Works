using System.Linq.Expressions;
using CasCadeVR.Works.Entities;

namespace CasCadeVR.Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Act"/>
/// </summary>
public interface IActReadRepository
{
    /// <summary>
    /// Возвращает true, если совпадает условие
    /// </summary>
    Task<bool> Any(Expression<Func<Act, bool>> action, CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="Act"/> по идентификатору
    /// </summary>
    Task<Act?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Act"/>
    /// </summary>
    Task<IReadOnlyCollection<Act>> GetAll(CancellationToken cancellationToken);
}

