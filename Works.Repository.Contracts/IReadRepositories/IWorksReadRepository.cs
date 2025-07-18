namespace Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Work"/>
/// </summary>
public interface IWorksReadRepository
{
    /// <summary>
    /// Получает <see cref="Entities.Work"/> по идентификатору
    /// </summary>
    Task<Entities.Work?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Entities.Work"/> по идентификаторам
    /// </summary>
    Task<IReadOnlyCollection<Entities.Work>> GetByIds(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Entities.Work"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Work>> GetAll(CancellationToken cancellationToken);
}

