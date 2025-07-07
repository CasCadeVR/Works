using Works.Common;
using Works.Context.Contracts;
using Works.Repository.Contracts.IWriteRepositories;

namespace Works.Repository.WriteRepositories;

/// <inheritdoc cref="IWorksWriteRepository"/>
public class WorksWriteRepository : BaseWriteRepository<Entities.Work>, IWorksWriteRepository
{
    /// <summary>
    /// ctor
    /// </summary>
    public WorksWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
