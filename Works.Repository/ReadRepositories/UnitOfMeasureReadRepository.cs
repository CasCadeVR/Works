using System.Linq.Expressions;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using Microsoft.EntityFrameworkCore;

namespace CasCadeVR.Works.Repository.ReadRepositories;

/// <inheritdoc cref="IUnitOfMeasureReadRepository"/>
public class UnitOfMeasureReadRepository : IUnitOfMeasureReadRepository
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UnitOfMeasureReadRepository"/>
    /// </summary>f
    public UnitOfMeasureReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<bool> IUnitOfMeasureReadRepository.Any(Expression<Func<UnitOfMeasure, bool>> action, CancellationToken cancellationToken)
         => reader.Read<UnitOfMeasure>()
        .NotDeletedAt()
        .AnyAsync(action, cancellationToken);

    Task<UnitOfMeasure?> IUnitOfMeasureReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<UnitOfMeasure>()
        .NotDeletedAt()
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<UnitOfMeasure>> IUnitOfMeasureReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<UnitOfMeasure>()
        .NotDeletedAt()
        .OrderBy(x => x.Name)
        .ToReadOnlyCollectionAsync(cancellationToken);

}