using AutoMapper;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
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
                 ?? throw new WorksNotFoundException($"Не удалось найти заказчика с идентификатором {id}");

            return mapper.Map<CustomerModel>(entity);
        }

        async Task<IReadOnlyCollection<CustomerModel>> ICustomerServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await readRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<CustomerModel>>(items);
        }

        async Task<CustomerModel> ICustomerServices.Create(CustomerCreateModel model, CancellationToken cancellationToken)
        {
            if (await readRepository.Any(x => x.TaxPayerId == model.TaxPayerId, cancellationToken))
            {
                throw new WorksDuplicateException($"Заказчик с номером {model.TaxPayerId} уже существует");
            }

            var result = mapper.Map<Customer>(model);

            writeRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CustomerModel>(result);
        }

        async Task<CustomerModel> ICustomerServices.Update(Guid id, CustomerCreateModel model, CancellationToken cancellationToken)
        {
            if (await readRepository.Any(x => x.TaxPayerId == model.TaxPayerId, cancellationToken))
            {
                throw new WorksDuplicateException($"Заказчик с номером {model.TaxPayerId} уже существует");
            }

            var entity = await readRepository.GetById(id, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти заказчика с идентификатором {id}");

            entity.FullName = model.FullName;
            entity.Occupation = model.Occupation;
            entity.Firm = model.Firm;
            entity.TaxPayerId = model.TaxPayerId;

            writeRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<CustomerModel>(entity);
        }

        async Task ICustomerServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти заказчика с идентификатором {id}");

            writeRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}