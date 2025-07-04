namespace Works.Repository.Contracts;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Work"/>
/// </summary>
public interface IWorksReadRepository
{
    /// <summary>
    /// Получаю <see cref="Entities.Work"/> по идентификатору
    /// </summary>
    Task<Entities.Work?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Entities.Work"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Work>> GetAll(CancellationToken cancellationToken);
}

