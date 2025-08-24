using AutoMapper;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.Customers;

namespace CasCadeVR.Works.Services.Services
{
    /// <inheritdoc cref="ICustomerServices"/>
    public class CustomerService : ICustomerServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICustomerReadRepository readRepository;
        private readonly ICustomerWriteRepository writeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CustomerService"/>
        /// </summary>
        public CustomerService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICustomerReadRepository readRepository,
            ICustomerWriteRepository writeRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
        }

        async Task<CustomerModel> ICustomerServices.GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
                 ?? throw new WorksNotFoundException($"Не удалось найти заказчика с иденитификатором {id}");

            return mapper.Map<CustomerModel>(entity);
        }

        async Task<IReadOnlyCollection<CustomerModel>> ICustomerServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await readRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<CustomerModel>>(items);
        }

        async Task<CustomerModel> ICustomerServices.Create(CustomerCreateModel model, CancellationToken cancellationToken)
        {
            var result = new Entities.Customer
            {
                Id = Guid.NewGuid(),
                FIO = model.FIO,
                Occupation = model.Occupation,
                Firm = model.Firm,
                INN = model.INN,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DeletedAt = null,
            };

            writeRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CustomerModel>(result);
        }

        async Task<CustomerModel> ICustomerServices.Update(CustomerModel model, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(model.Id, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти заказчика с иденитификатором {model.Id}");

            entity.FIO = model.FIO;
            entity.Occupation = model.Occupation;
            entity.Firm = model.Firm;
            entity.INN = model.INN;
            entity.UpdatedAt = DateTime.Now;

            writeRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<CustomerModel>(entity);
        }

        async Task ICustomerServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти заказчика с иденитификатором {id}");

            writeRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}