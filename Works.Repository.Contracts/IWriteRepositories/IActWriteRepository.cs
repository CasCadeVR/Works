using Works.Context.Contracts;

namespace Works.Repository.Contracts.IWriteRepositories;

/// <summary>
/// Репозиторий записи <see cref="Entities.Act"/>
/// </summary>
public interface IActWriteRepository : IDBWriter<Entities.Act> { }