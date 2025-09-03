using System.Linq.Expressions;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
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

    Task<Act?> IActReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<Act>()
        .NotDeletedAt()
        .Select(x => new Act
        {
            Id = x.Id,
            ActNumber = x.ActNumber,
            Date = x.Date,
            Executor = x.Executor,
            ExecutorId = x.ExecutorId,
            Customer = x.Customer,
            CustomerId = x.CustomerId,
            ActWorks = x.ActWorks.Where(y => y.DeletedAt == null).Select(y => new ActWork
            {
                Id = y.Id,
                Quantity = y.Quantity,
                WorkId = y.WorkId,
                ActId = x.Id,
                Work = new Work
                {
                    Id = y.Work.Id,
                    Name = y.Work.Name,
                    Description = y.Work.Description,
                    Price = y.Work.Price,
                    UnitOfMeasureId = y.Work.UnitOfMeasureId,
                    UnitOfMeasure = new UnitOfMeasure
                    {
                        Id = y.Work.UnitOfMeasure.Id,
                        Name = y.Work.UnitOfMeasure.Name,
                        CreatedAt = y.Work.UnitOfMeasure.CreatedAt,
                        UpdatedAt = y.Work.UnitOfMeasure.UpdatedAt,
                        DeletedAt = y.Work.UnitOfMeasure.DeletedAt,
                    },
                    CreatedAt = y.Work.CreatedAt,
                    UpdatedAt = y.Work.UpdatedAt,
                    DeletedAt = y.Work.DeletedAt,
                },
                CreatedAt = y.CreatedAt,
                UpdatedAt = y.UpdatedAt,
                DeletedAt = y.DeletedAt,
            }).ToList(),
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
            DeletedAt = x.DeletedAt,
        })
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Act>> IActReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Act>()
        .NotDeletedAt()
        .Select(x => new Act
        {
            Id = x.Id,
            ActNumber = x.ActNumber,
            Date = x.Date,
            Executor = x.Executor,
            Customer = x.Customer,
            ActWorks = x.ActWorks.Where(y => y.DeletedAt == null).Select(y => new ActWork
            {
                Id = y.Id,
                Quantity = y.Quantity,
                WorkId = y.WorkId,
                ActId = x.Id,
                Work = new Work
                {
                    Id = y.Work.Id,
                    Name = y.Work.Name,
                    Description = y.Work.Description,
                    Price = y.Work.Price,
                    UnitOfMeasureId = y.Work.UnitOfMeasureId,
                    UnitOfMeasure = new UnitOfMeasure
                    {
                        Id = y.Work.UnitOfMeasure.Id,
                        Name = y.Work.UnitOfMeasure.Name,
                        CreatedAt = y.Work.UnitOfMeasure.CreatedAt,
                        UpdatedAt = y.Work.UnitOfMeasure.UpdatedAt,
                        DeletedAt = y.Work.UnitOfMeasure.DeletedAt,
                    },
                    CreatedAt = y.Work.CreatedAt,
                    UpdatedAt = y.Work.UpdatedAt,
                    DeletedAt = y.Work.DeletedAt,
                },
                CreatedAt = y.CreatedAt,
                UpdatedAt = y.UpdatedAt,
                DeletedAt = y.DeletedAt,
            }).ToList(),
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt,
            DeletedAt = x.DeletedAt,
        })
        .OrderBy(x => x.ActNumber)
        .ToReadOnlyCollectionAsync(cancellationToken);
}