using Works.Context.Contracts;

namespace Works.Repository.Contracts.IWriteRepositories;

/// <summary>
/// Репозиторий записи <see cref="Entities.Executor"/>
/// </summary>
public interface IExecutorWriteRepository : IDBWriter<Entities.Executor> { }