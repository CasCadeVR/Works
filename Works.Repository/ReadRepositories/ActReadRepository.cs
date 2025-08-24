using CasCadeVR.Works.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;

namespace CasCadeVR.Works.Repository.ReadRepositories;

/// <inheritdoc cref="IActReadRepository"/>
public class ActReadRepository : IActReadRepository
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ActReadRepository"/>
    /// </summary>
    public ActReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<Entities.Act?> IActReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<Entities.Act>()
        .Include(x => x.Executor)
        .Include(x => x.Customer)
        .Include(x => x.Works).ThenInclude(x => x.Work)
        .NotDeletedAt()
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Entities.Act>> IActReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Entities.Act>()
        .Include(x => x.Executor)
        .Include(x => x.Customer)
        .Include(x => x.Works).ThenInclude(x => x.Work)
        .NotDeletedAt()
        .OrderBy(x => x.ActNumber)
        .ToReadOnlyCollectionAsync(cancellationToken);
}