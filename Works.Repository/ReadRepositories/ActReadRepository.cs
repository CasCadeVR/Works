using System.Linq.Expressions;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.Contracts.Models;
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
        .Select(x => new ActDbModel
        {
            Id = x.Id,
            ActNumber = x.ActNumber,
            Date = x.Date,
            Executor = x.Executor,
            Customer = x.Customer,
            ActWorks = x.ActWorks.Where(y => y.DeletedAt == null).Select(y => new ActWorkDbModel
            {
                Id = y.Id,
                Quantity = y.Quantity,
                Work = new WorkDbModel
                {
                    Id = y.Work.Id,
                    Name = y.Work.Name,
                    Description = y.Work.Description,
                    Price = y.Work.Price,
                    UnitOfMeasure = y.Work.UnitOfMeasure,
                },
            }) .ToList(),
        })
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<ActDbModel>> IActReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Act>()
        .NotDeletedAt()
       .Select(x => new ActDbModel
       {
           Id = x.Id,
           ActNumber = x.ActNumber,
           Date = x.Date,
           Executor = x.Executor,
           Customer = x.Customer,
           ActWorks = x.ActWorks.Where(y => y.DeletedAt == null).Select(y => new ActWorkDbModel
           {
               Id = y.Id,
               Quantity = y.Quantity,
               Work = new WorkDbModel
               {
                   Id = y.Work.Id,
                   Name = y.Work.Name,
                   Description = y.Work.Description,
                   Price = y.Work.Price,
                   UnitOfMeasure = y.Work.UnitOfMeasure,
               },
           }).ToList(),
       })
        .OrderBy(x => x.Date)
        .ToReadOnlyCollectionAsync(cancellationToken);
}