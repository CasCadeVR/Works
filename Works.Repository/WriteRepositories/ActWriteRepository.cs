using CasCadeVR.Works.Common;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

namespace CasCadeVR.Works.Repository.WriteRepositories;

/// <inheritdoc cref="IActWriteRepository"/>
public class ActWriteRepository : BaseWriteRepository<Act>, IActWriteRepository
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ActWriteRepository"/>
    /// </summary>
    public ActWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
