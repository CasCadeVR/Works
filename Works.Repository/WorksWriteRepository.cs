using Works.Repository.Contracts;
using Works.Common;
using Works.Context.Contracts;

namespace Works.Repository;

/// <inheritdoc cref="IWorksWriteRepository"/>
public class WorksWriteRepository : BaseWriteRepository<Entities.Work>, IWorksWriteRepository
{
    /// <summary>
    /// ctor
    /// </summary>
    public WorksWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
