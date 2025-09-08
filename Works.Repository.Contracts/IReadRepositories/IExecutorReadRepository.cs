using System.Linq.Expressions;
using CasCadeVR.Works.Entities;

namespace CasCadeVR.Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Executor"/>
/// </summary>
public interface IExecutorReadRepository
{
    /// <summary>
    /// Возвращает true, если совпадает условие
    /// </summary>
    Task<bool> Any(Expression<Func<Executor, bool>> action, CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="Executor"/> по идентификатору
    /// </summary>
    Task<Executor?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Executor"/>
    /// </summary>
    Task<IReadOnlyCollection<Executor>> GetAll(CancellationToken cancellationToken);
}

