using CasCadeVR.Works.Common.Contracts;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

namespace CasCadeVR.Works.Repository.WriteRepositories;

/// <inheritdoc cref="IWorksWriteRepository"/>
public class WorksWriteRepository : BaseWriteRepository<Entities.Work>, IWorksWriteRepository
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksWriteRepository"/>
    /// </summary>
    public WorksWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
