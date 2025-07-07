using Works.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using Works.Repository.Contracts.IReadRepositories;

namespace Works.Repository.ReadRepositories;

/// <inheritdoc cref="ICustomerReadRepository"/>
public class CustomerReadRepository : ICustomerReadRepository
{
    private readonly IReader reader;

    /// <summary>
    /// ctor
    /// </summary>
    public CustomerReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<Entities.Customer?> ICustomerReadRepository.GetById(Guid id, CancellationToken cancellationToken)
         => reader.Read<Entities.Customer>()
        .NotDeletedAt()
        .ById(id)
        .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<Entities.Customer>> ICustomerReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<Entities.Customer>()
        .NotDeletedAt()
        .OrderBy(x => x.FIO)
        .ToReadOnlyCollectionAsync(cancellationToken);
}