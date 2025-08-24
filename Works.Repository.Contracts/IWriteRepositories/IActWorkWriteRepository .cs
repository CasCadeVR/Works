using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;

namespace CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

/// <summary>
/// Репозиторий записи <see cref="ActWork"/>
/// </summary>
public interface IActWorkWriteRepository : IDBWriter<ActWork> { }