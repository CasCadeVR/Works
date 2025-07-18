using Works.Context.Contracts;
using Works.Entities;

namespace Works.Repository.Contracts.IWriteRepositories;

/// <summary>
/// Репозиторий записи <see cref="ActWork"/>
/// </summary>
public interface IActWorkWriteRepository : IDBWriter<ActWork> { }