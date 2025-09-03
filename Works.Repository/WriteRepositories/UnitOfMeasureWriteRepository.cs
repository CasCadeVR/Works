using CasCadeVR.Works.Common.Contracts;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

namespace CasCadeVR.Works.Repository.WriteRepositories;

/// <inheritdoc cref="IWorksWriteRepository"/>
public class UnitOfMeasureWriteRepository : BaseWriteRepository<UnitOfMeasure>, IUnitOfMeasureWriteRepository
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksWriteRepository"/>
    /// </summary>
    public UnitOfMeasureWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
