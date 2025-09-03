using System.Linq.Expressions;

namespace CasCadeVR.Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Executor"/>
/// </summary>
public interface IExecutorReadRepository
{
    /// <summary>
    /// Возвращает true, если совпадает условие
    /// </summary>
    Task<bool> Any(Expression<Func<Entities.Executor, bool>> action, CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="Entities.Executor"/> по идентификатору
    /// </summary>
    Task<Entities.Executor?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Entities.Executor"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Executor>> GetAll(CancellationToken cancellationToken);
}

