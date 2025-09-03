using System.Linq.Expressions;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using Microsoft.EntityFrameworkCore;

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

    Task<bool> IWorksReadRepository.Any(Expression<Func<Work, bool>> action, CancellationToken cancellationToken)
         => reader.Read<Work>()
        .NotDeletedAt()
        .AnyAsync(action, cancellationToken);

    Task<Work?> IWorksReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<Work>()
        .NotDeletedAt()
        .Select(x => new Work
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            UnitOfMeasure = x.UnitOfMeasure,
            UnitOfMeasureId = x.UnitOfMeasureId,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
            DeletedAt = x.DeletedAt,
        })
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Work>> IWorksReadRepository.GetByIds(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    => reader.Read<Work>()
        .NotDeletedAt()
        .Select(x => new Work
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            UnitOfMeasure = x.UnitOfMeasure,
            UnitOfMeasureId = x.UnitOfMeasureId,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
            DeletedAt = x.DeletedAt,
        })
        .ByIds(ids)
        .OrderBy(x => x.Name)
        .ToReadOnlyCollectionAsync(cancellationToken);

    Task<IReadOnlyCollection<Work>> IWorksReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Work>()
        .NotDeletedAt()
        .Select(x => new Work
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            UnitOfMeasure = x.UnitOfMeasure,
        })
        .OrderBy(x => x.Name)
        .ToReadOnlyCollectionAsync(cancellationToken);

}