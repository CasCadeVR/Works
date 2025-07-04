using Works.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using Works.Repository.Contracts;

namespace Works.Repository;

/// <inheritdoc cref="IWorksReadRepository"/>
public class WorksReadRepository : IWorksReadRepository
{
    private readonly IReader reader;

    /// <summary>
    /// ctor
    /// </summary>
    public WorksReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<Entities.Work?> IWorksReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<Entities.Work>()
        .NotDeletedAt()
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Entities.Work>> IWorksReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Entities.Work>()
        .NotDeletedAt()
        .OrderBy(x => x.Name)
        .ToReadOnlyCollectionAsync(cancellationToken);
}