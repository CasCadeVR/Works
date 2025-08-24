using CasCadeVR.Works.Common;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

namespace CasCadeVR.Works.Repository.WriteRepositories;

/// <inheritdoc cref="ICustomerWriteRepository"/>
public class CustomerWriteRepository : BaseWriteRepository<Entities.Customer>, ICustomerWriteRepository
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CustomerWriteRepository"/>
    /// </summary>
    public CustomerWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider) { }
}
