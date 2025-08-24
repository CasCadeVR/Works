namespace CasCadeVR.Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Entities.Customer"/>
/// </summary>
public interface ICustomerReadRepository
{
    /// <summary>
    /// Получаю <see cref="Entities.Customer"/> по идентификатору
    /// </summary>
    Task<Entities.Customer?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Entities.Customer"/>
    /// </summary>
    Task<IReadOnlyCollection<Entities.Customer>> GetAll(CancellationToken cancellationToken);
}

