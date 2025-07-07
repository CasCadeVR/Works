namespace Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Executor"/>
/// </summary>
public interface IExecutorReadRepository
{
    /// <summary>
    /// Получаю <see cref="Entities.Executor"/> по идентификатору
    /// </summary>
    Task<Entities.Executor?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Entities.Executor"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Executor>> GetAll(CancellationToken cancellationToken);
}

