using AutoMapper;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;

namespace CasCadeVR.Works.Services.Services
{
    /// <inheritdoc cref="IUnitOfMeasureServices"/>
    public class UnitOfMeasureServices : IUnitOfMeasureServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUnitOfMeasureReadRepository readRepository;
        private readonly IUnitOfMeasureWriteRepository writeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UnitOfMeasureServices"/>
        /// </summary>
        public UnitOfMeasureServices(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUnitOfMeasureReadRepository readRepository,
            IUnitOfMeasureWriteRepository writeRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
        }

        async Task<UnitOfMeasureModel> IUnitOfMeasureServices.GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
                 ?? throw new WorksNotFoundException($"Не удалось найти единицу измерения с идентификатором {id}");

            return mapper.Map<UnitOfMeasureModel>(entity);
        }

        async Task<IReadOnlyCollection<UnitOfMeasureModel>> IUnitOfMeasureServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await readRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<UnitOfMeasureModel>>(items);
        }

        async Task<UnitOfMeasureModel> IUnitOfMeasureServices.Create(UnitOfMeasureCreateModel model, CancellationToken cancellationToken)
        {
            if (await readRepository.Any(x => x.Name == model.Name, cancellationToken))
            {
                throw new WorksDuplicateException($"Единица измерения с именем {model.Name} уже существует");
            }

            var result = mapper.Map<UnitOfMeasure>(model);

            writeRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<UnitOfMeasureModel>(result);
        }

        async Task<UnitOfMeasureModel> IUnitOfMeasureServices.Update(Guid id, UnitOfMeasureCreateModel model, CancellationToken cancellationToken)
        {
            if (await readRepository.Any(x => x.Name == model.Name, cancellationToken))
            {
                throw new WorksDuplicateException($"Единица измерения с именем {model.Name} уже существует");
            }

            var entity = await readRepository.GetById(id, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти единицу измерения с идентификатором {id}");

            entity.Name = model.Name;

            writeRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<UnitOfMeasureModel>(entity);
        }

        async Task IUnitOfMeasureServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти единицу измерения с идентификатором {id}");

            writeRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}