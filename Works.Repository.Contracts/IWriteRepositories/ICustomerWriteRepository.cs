using CasCadeVR.Works.Context.Contracts;

namespace CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

/// <summary>
/// Репозиторий записи <see cref="Entities.Customer"/>
/// </summary>
public interface ICustomerWriteRepository : IDBWriter<Entities.Customer> { }