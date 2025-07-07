using AutoMapper;
using Works.Context.Contracts;
using Works.Repository.Contracts.IReadRepositories;
using Works.Repository.Contracts.IWriteRepositories;
using Works.Services.Contracts.Exceptions;
using Works.Services.Contracts.IServices;
using Works.Services.Contracts.Models.Executors;

namespace Works.Services.Services
{
    /// <inheritdoc cref="IExecutorServices"/>
    public class ExecutorServices : IExecutorServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IExecutorReadRepository readRepository;
        private readonly IExecutorWriteRepository writeRepository;

        /// <summary>
        /// ctor
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
                 ?? throw new WorksNotFoundException($"Не удалось найти исполнителя с иденитификатором {id}");

            return mapper.Map<ExecutorModel>(entity);
        }

        async Task<IReadOnlyCollection<ExecutorModel>> IExecutorServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await readRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<ExecutorModel>>(items);
        }

        async Task<ExecutorModel> IExecutorServices.Create(ExecutorCreateModel model, CancellationToken cancellationToken)
        {
            var result = new Entities.Executor
            {
                Id = Guid.NewGuid(),
                FIO = model.FIO,
                Occupation = model.Occupation,
                Firm = model.Firm,
                OGRN = model.OGRN,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DeletedAt = null,
            };

            writeRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ExecutorModel>(result);
        }

        async Task<ExecutorModel> IExecutorServices.Update(ExecutorModel model, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(model.Id, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти исполнителя с иденитификатором {model.Id}");

            entity.FIO = model.FIO;
            entity.Occupation = model.Occupation;
            entity.Firm = model.Firm;
            entity.OGRN = model.OGRN;
            entity.UpdatedAt = DateTime.Now;

            writeRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ExecutorModel>(entity);
        }

        async Task IExecutorServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти исполнителя с иденитификатором {id}");

            writeRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}