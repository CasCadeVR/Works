using System.Linq.Expressions;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using Microsoft.EntityFrameworkCore;

namespace CasCadeVR.Works.Repository.ReadRepositories;

/// <inheritdoc cref="ICustomerReadRepository"/>
public class CustomerReadRepository : ICustomerReadRepository
{
    private readonly IReader reader;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CustomerReadRepository"/>
    /// </summary>
    public CustomerReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<bool> ICustomerReadRepository.Any(Expression<Func<Customer, bool>> action, CancellationToken cancellationToken)
         => reader.Read<Customer>()
        .NotDeletedAt()
        .AnyAsync(action, cancellationToken);

    Task<Customer?> ICustomerReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<Customer>()
        .NotDeletedAt()
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Customer>> ICustomerReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Customer>()
        .NotDeletedAt()
        .OrderBy(x => x.FullName)
        .ToReadOnlyCollectionAsync(cancellationToken);
}