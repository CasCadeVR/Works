using AutoMapper;
using Works.Context.Contracts;
using Works.Repository.Contracts.IReadRepositories;
using Works.Repository.Contracts.IWriteRepositories;
using Works.Services.Contracts.Exceptions;
using Works.Services.Contracts.IServices;
using Works.Services.Contracts.Models.Works;

namespace Works.Services.Services
{
    /// <inheritdoc cref="IWorksServices"/>
    public class WorksServices : IWorksServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWorksReadRepository readRepository;
        private readonly IWorksWriteRepository writeRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public WorksServices(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IWorksReadRepository readRepository,
            IWorksWriteRepository writeRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
        }

        async Task<WorksModel> IWorksServices.GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
                 ?? throw new WorksNotFoundException($"Не удалось найти работу с иденитификатором {id}");

            return mapper.Map<WorksModel>(entity);
        }

        async Task<IReadOnlyCollection<WorksModel>> IWorksServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await readRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<WorksModel>>(items);
        }

        async Task<WorksModel> IWorksServices.Create(WorksCreateModel model, CancellationToken cancellationToken)
        {
            var result = new Entities.Work
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                UnitOfMeasure = model.UnitOfMeasure,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DeletedAt = null,
            };

            writeRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<WorksModel>(result);
        }

        async Task<WorksModel> IWorksServices.Update(WorksModel model, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(model.Id, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти работу с иденитификатором {model.Id}");

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.UnitOfMeasure = model.UnitOfMeasure;
            entity.UpdatedAt = DateTime.Now;

            writeRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<WorksModel>(entity);
        }

        async Task IWorksServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти работу с иденитификатором {id}");

            writeRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}