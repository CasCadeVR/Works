using Works.Common;
using Works.Context.Contracts;
using Works.Entities;
using Works.Repository.Contracts.IWriteRepositories;

namespace Works.Repository.WriteRepositories;

/// <inheritdoc cref="IActWriteRepository"/>
public class ActWriteRepository : BaseWriteRepository<Act>, IActWriteRepository
{
    /// <summary>
    /// ctor
    /// </summary>
    public ActWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
