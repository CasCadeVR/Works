using CasCadeVR.Works.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Entities;

namespace CasCadeVR.Works.Repository.ReadRepositories;

/// <inheritdoc cref="IWorksReadRepository"/>
public class WorksReadRepository : IWorksReadRepository
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="WorksReadRepository"/>
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