using CasCadeVR.Works.Context.Contracts;

namespace CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

/// <summary>
/// Репозиторий записи <see cref="Entities.Work"/>
/// </summary>
public interface IWorksWriteRepository : IDBWriter<Entities.Work> { }