using AutoMapper;
using Works.Context.Contracts;
using Works.Repository.Contracts;
using Works.Services.Contracts;
using Works.Services.Contracts.Exceptions;
using Works.Services.Contracts.Models;

namespace Works.Services
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
                ?? throw new WorksNotFoundException($"Не удалось найти товар с иденитификатором {model.Id}");

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.UpdatedAt = DateTime.Now;

            writeRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<WorksModel>(entity);
        }
    }
}