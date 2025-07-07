using Works.Context.Contracts;

namespace Works.Repository.Contracts.IWriteRepositories;

/// <summary>
/// Репозиторий записи <see cref="Entities.Work"/>
/// </summary>
public interface IWorksWriteRepository : IDBWriter<Entities.Work> { }