using CasCadeVR.Works.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;

namespace CasCadeVR.Works.Repository.ReadRepositories;

/// <inheritdoc cref="IExecutorReadRepository"/>
public class ExecutorReadRepository : IExecutorReadRepository
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ExecutorReadRepository"/>
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