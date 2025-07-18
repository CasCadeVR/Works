using Works.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using Works.Repository.Contracts.IReadRepositories;
using Works.Entities;

namespace Works.Repository.ReadRepositories;

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

    Task<Work?> IWorksReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<Work>()
        .NotDeletedAt()
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Work>> IWorksReadRepository.GetByIds(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    => reader.Read<Work>()
        .NotDeletedAt()
        .ByIds(ids)
        .OrderBy(x => x.Name)
        .ToReadOnlyCollectionAsync(cancellationToken);

    Task<IReadOnlyCollection<Work>> IWorksReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Work>()
        .NotDeletedAt()
        .OrderBy(x => x.Name)
        .ToReadOnlyCollectionAsync(cancellationToken);

}