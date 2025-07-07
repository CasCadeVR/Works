using Works.Common;
using Works.Context.Contracts;
using Works.Repository.Contracts.IWriteRepositories;

namespace Works.Repository.WriteRepositories;

/// <inheritdoc cref="ICustomerWriteRepository"/>
public class CustomerWriteRepository : BaseWriteRepository<Entities.Customer>, ICustomerWriteRepository
{
    /// <summary>
    /// ctor
    /// </summary>
    public CustomerWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
