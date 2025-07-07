using Works.Context.Contracts;

namespace Works.Repository.Contracts.IWriteRepositories;

/// <summary>
/// Репозиторий записи <see cref="Entities.Customer"/>
/// </summary>
public interface ICustomerWriteRepository : IDBWriter<Entities.Customer> { }