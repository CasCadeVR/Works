using Works.Common;
using Works.Context.Contracts;
using Works.Repository.Contracts.IWriteRepositories;

namespace Works.Repository.WriteRepositories;

/// <inheritdoc cref="IExecutorWriteRepository"/>
public class ExecutorWriteRepository : BaseWriteRepository<Entities.Executor>, IExecutorWriteRepository
{
    /// <summary>
    /// ctor
    /// </summary>
    public ExecutorWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
