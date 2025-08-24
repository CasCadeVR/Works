namespace CasCadeVR.Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Act"/>
/// </summary>
public interface IActReadRepository
{
    /// <summary>
    /// Получаю <see cref="Entities.Act"/> по идентификатору
    /// </summary>
    Task<Entities.Act?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Entities.Act"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Act>> GetAll(CancellationToken cancellationToken);
}

