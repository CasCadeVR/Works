using System.Linq.Expressions;
using CasCadeVR.Works.Entities;

namespace CasCadeVR.Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Work"/>
/// </summary>
public interface IWorksReadRepository
{
    /// <summary>
    /// Возвращает true, если совпадает условие
    /// </summary>
    Task<bool> Any(Expression<Func<Work, bool>> action, CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="Work"/> по идентификатору
    /// </summary>
    Task<Work?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Work"/> по идентификаторам
    /// </summary>
    Task<IReadOnlyCollection<Work>> GetByIds(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Work"/>
    /// </summary>
    Task<IReadOnlyCollection<Work>> GetAll(CancellationToken cancellationToken);
}

