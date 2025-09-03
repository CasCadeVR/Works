using AutoMapper;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.Executors;

namespace CasCadeVR.Works.Services.Services
{
    /// <inheritdoc cref="IExecutorServices"/>
    public class ExecutorServices : IExecutorServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IExecutorReadRepository readRepository;
        private readonly IExecutorWriteRepository writeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ExecutorServices"/>
        /// </summary>
        public ExecutorServices(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IExecutorReadRepository readRepository,
            IExecutorWriteRepository writeRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
        }

        async Task<ExecutorModel> IExecutorServices.GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
                 ?? throw new WorksNotFoundException($"Не удалось найти исполнителя с идентификатором {id}");

            return mapper.Map<ExecutorModel>(entity);
        }

        async Task<IReadOnlyCollection<ExecutorModel>> IExecutorServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await readRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<ExecutorModel>>(items);
        }

        async Task<ExecutorModel> IExecutorServices.Create(ExecutorCreateModel model, CancellationToken cancellationToken)
        {
            if (await readRepository.Any(x => x.RegistrationNumber == model.RegistrationNumber, cancellationToken))
            {
                throw new WorksDuplicateException($"Исполнитель с регистрационным номером {model.RegistrationNumber} уже существует");
            }

            var result = mapper.Map<Executor>(model);

            writeRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ExecutorModel>(result);
        }

        async Task<ExecutorModel> IExecutorServices.Update(Guid id, ExecutorCreateModel model, CancellationToken cancellationToken)
        {
            if (await readRepository.Any(x => x.RegistrationNumber == model.RegistrationNumber, cancellationToken))
            {
                throw new WorksDuplicateException($"Исполнитель с регистрационным номером {model.RegistrationNumber} уже существует");
            }

            var entity = await readRepository.GetById(id, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти исполнителя с идентификатором {id}");

            entity.FullName = model.FullName;
            entity.Occupation = model.Occupation;
            entity.Firm = model.Firm;
            entity.RegistrationNumber = model.RegistrationNumber;

            writeRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ExecutorModel>(entity);
        }

        async Task IExecutorServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти исполнителя с идентификатором {id}");

            writeRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}