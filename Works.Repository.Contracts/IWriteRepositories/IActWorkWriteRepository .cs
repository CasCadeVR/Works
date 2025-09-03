using CasCadeVR.Works.Context.Contracts;

namespace CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

/// <summary>
/// Репозиторий записи <see cref="Entities.ActWork"/>
/// </summary>
public interface IActWorkWriteRepository : IDBWriter<Entities.ActWork> { }