using System.Linq.Expressions;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using Microsoft.EntityFrameworkCore;

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

    Task<bool> IExecutorReadRepository.Any(Expression<Func<Executor, bool>> action, CancellationToken cancellationToken)
         => reader.Read<Executor>()
        .NotDeletedAt()
        .AnyAsync(action, cancellationToken);

    Task<Executor?> IExecutorReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<Executor>()
        .NotDeletedAt()
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Executor>> IExecutorReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Executor>()
        .NotDeletedAt()
        .OrderBy(x => x.FullName)
        .ToReadOnlyCollectionAsync(cancellationToken);
}