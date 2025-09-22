using System.Linq.Expressions;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.Contracts.Models;
using CasCadeVR.Works.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

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

    Task<bool> IActReadRepository.Any(Expression<Func<Act, bool>> action, CancellationToken cancellationToken)
         => reader.Read<Act>()
        .NotDeletedAt()
        .AnyAsync(action, cancellationToken);

    Task<ActDbModel?> IActReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<Act>()
        .NotDeletedAt()
        .ById(id)
        .SelectActDbModel()
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<ActDbModel>> IActReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Act>()
        .NotDeletedAt()
        .SelectActDbModel()
        .OrderByDescending(x => x.Date)
        .ToReadOnlyCollectionAsync(cancellationToken);
}