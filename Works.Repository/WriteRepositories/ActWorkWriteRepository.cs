using CasCadeVR.Works.Common.Contracts;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

namespace CasCadeVR.Works.Repository.WriteRepositories;

/// <inheritdoc cref="IActWorkWriteRepository"/>
public class ActWorkWriteRepository : BaseWriteRepository<Entities.ActWork>, IActWorkWriteRepository
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ActWriteRepository"/>
    /// </summary>
    public ActWorkWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
