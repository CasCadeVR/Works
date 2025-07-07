using Works.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using Works.Repository.Contracts.IReadRepositories;

namespace Works.Repository.ReadRepositories;

/// <inheritdoc cref="IExecutorReadRepository"/>
public class ExecutorReadRepository : IExecutorReadRepository
{
    private readonly IReader reader;

    /// <summary>
    /// ctor
    /// </summary>
    public ExecutorReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<Entities.Executor?> IExecutorReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<Entities.Executor>()
        .NotDeletedAt()
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Entities.Executor>> IExecutorReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Entities.Executor>()
        .NotDeletedAt()
        .OrderBy(x => x.FIO)
        .ToReadOnlyCollectionAsync(cancellationToken);
}