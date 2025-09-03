using CasCadeVR.Works.Common.Contracts;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

namespace CasCadeVR.Works.Repository.WriteRepositories;

/// <inheritdoc cref="IExecutorWriteRepository"/>
public class ExecutorWriteRepository : BaseWriteRepository<Entities.Executor>, IExecutorWriteRepository
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ExecutorWriteRepository"/>
    /// </summary>
    public ExecutorWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
