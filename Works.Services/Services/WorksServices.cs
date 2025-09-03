using AutoMapper;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.Works;

namespace CasCadeVR.Works.Services.Services
{
    /// <inheritdoc cref="IWorksServices"/>
    public class WorksServices : IWorksServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUnitOfMeasureReadRepository unitOfMeasureReadRepository;
        private readonly IWorksReadRepository readRepository;
        private readonly IWorksWriteRepository writeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="WorksServices"/>
        /// </summary>
        public WorksServices(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUnitOfMeasureReadRepository unitOfMeasureReadRepository,
            IWorksReadRepository readRepository,
            IWorksWriteRepository writeRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.unitOfMeasureReadRepository = unitOfMeasureReadRepository;
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
        }

        async Task<WorksModel> IWorksServices.GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
                 ?? throw new WorksNotFoundException($"Не удалось найти работу с идентификатором {id}");

            return mapper.Map<WorksModel>(entity);
        }

        async Task<IReadOnlyCollection<WorksModel>> IWorksServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await readRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<WorksModel>>(items);
        }

        async Task<WorksModel> IWorksServices.Create(WorksCreateModel model, CancellationToken cancellationToken)
        {
            if (await readRepository.Any(x => x.Name == model.Name, cancellationToken))
            {
                throw new WorksDuplicateException($"Работа с наименованием {model.Name} уже существует");
            }

            var exisitngUnitOfMeasure = await unitOfMeasureReadRepository.GetById(model.UnitOfMeasureId, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти единицу измерения с идентификатором {model.UnitOfMeasureId}");

            var result = mapper.Map<Work>(model);
            result.UnitOfMeasureId = model.UnitOfMeasureId;
            result.UnitOfMeasure = exisitngUnitOfMeasure;

            writeRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<WorksModel>(result);
        }

        async Task<WorksModel> IWorksServices.Update(Guid id, WorksCreateModel model, CancellationToken cancellationToken)
        {
            if (await readRepository.Any(x => x.Name == model.Name, cancellationToken))
            {
                throw new WorksDuplicateException($"Работа с наименованием {model.Name} уже существует");
            }

            var exisitngUnitOfMeasure = await unitOfMeasureReadRepository.GetById(model.UnitOfMeasureId, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти единицу измерения с идентификатором {model.UnitOfMeasureId}");

            var entity = await readRepository.GetById(id, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти работу с идентификатором {id}");

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.UnitOfMeasureId = model.UnitOfMeasureId;
            entity.UnitOfMeasure = exisitngUnitOfMeasure;

            writeRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<WorksModel>(entity);
        }

        async Task IWorksServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти работу с идентификатором {id}");

            writeRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}