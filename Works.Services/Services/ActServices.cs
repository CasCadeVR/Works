using AutoMapper;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Export.Contracts;
using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Services.Contracts.Models.Export;

namespace CasCadeVR.Works.Services.Services
{
    /// <inheritdoc cref="IWorksServices"/>
    public class ActServices : IActServices
    {
        private readonly IMapper mapper;
        private readonly IExporter exporter;
        private readonly IUnitOfWork unitOfWork;
        private readonly IActReadRepository readRepository;
        private readonly IWorksReadRepository workReadRepository;
        private readonly ICustomerReadRepository customerReadRepository;
        private readonly IExecutorReadRepository executorReadRepository;
        private readonly IActWorkWriteRepository actWorkWriteRepository;
        private readonly IActWriteRepository writeRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ActServices"/>
        /// </summary>
        public ActServices(
            IMapper mapper,
            IExporter exporter,
            IUnitOfWork unitOfWork,
            IActReadRepository readRepository,
            IWorksReadRepository workReadRepository,
            ICustomerReadRepository customerReadRepository,
            IExecutorReadRepository executorReadRepository,
            IActWorkWriteRepository actWorkWriteRepository,
            IActWriteRepository writeRepository)
        {
            this.mapper = mapper;
            this.exporter = exporter;
            this.unitOfWork = unitOfWork;
            this.readRepository = readRepository;
            this.workReadRepository = workReadRepository;
            this.customerReadRepository = customerReadRepository;
            this.executorReadRepository = executorReadRepository;
            this.actWorkWriteRepository = actWorkWriteRepository;
            this.writeRepository = writeRepository;
        }

        async Task<ActModel> IActServices.GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
                 ?? throw new WorksNotFoundException($"Не удалось найти акт с идентификатором {id}");

            return mapper.Map<ActModel>(entity);
        }

        async Task<IReadOnlyCollection<ActModel>> IActServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await readRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<ActModel>>(items);
        }

        async Task<ExportedData> IActServices.Export(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти акт с идентификатором {id}");

            var entityModel = mapper.Map<ActModel>(entity);
            var result = exporter.Export(entityModel);
            
            return result;
        }

        async Task<ActModel> IActServices.Create(ActCreateModel model, CancellationToken cancellationToken)
        {
            await ValidateConnections(model, cancellationToken);

            var modelActWorks = mapper.Map<ICollection<ActWork>>(model.ActWorks);

            var result = new Act
            {
                ActNumber = model.ActNumber,
                Date = model.Date,
                CustomerId = model.CustomerId,
                ExecutorId = model.ExecutorId,
                ActWorks = modelActWorks,
            };

            foreach (var actWork in result.ActWorks)
            {
                actWork.ActId = result.Id;
                actWork.WorkId = actWork.WorkId;
                actWorkWriteRepository.Add(actWork);
            }

            writeRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var addedEntity = await readRepository.GetById(result.Id, cancellationToken)
                ?? throw new InvalidOperationException($"Не удалось найти акт с идентификатором {result.Id}");

            return mapper.Map<ActModel>(addedEntity);
        }

        async Task<ActModel> IActServices.Update(Guid id, ActCreateModel model, CancellationToken cancellationToken)
        {
            var databaseEntity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти акт с идентификатором {id}");

            await ValidateConnections(model, cancellationToken);

            databaseEntity.ActNumber = model.ActNumber;
            databaseEntity.Date = model.Date;
            databaseEntity.CustomerId = model.CustomerId;
            databaseEntity.ExecutorId = model.ExecutorId;

            var modelActWorks = mapper.Map<ICollection<ActWork>>(model.ActWorks);

            var existingActWorks = databaseEntity.ActWorks;
            var existingActWorksDictionary = existingActWorks.ToDictionary(x => x.WorkId);

            foreach (var actWork in modelActWorks)
            {
                if (existingActWorksDictionary.TryGetValue(actWork.WorkId, out var foundActWork))
                {
                    foundActWork.Quantity = actWork.Quantity;
                    actWorkWriteRepository.Update(foundActWork);
                    continue;
                }

                actWork.ActId = databaseEntity.Id;
                actWorkWriteRepository.Add(actWork);
            }

            var actWorksIdsToDelete = existingActWorks.Select(x => x.WorkId).Except(modelActWorks.Select(x => x.WorkId));

            foreach (var actWorkId in actWorksIdsToDelete)
            {
                actWorkWriteRepository.Delete(existingActWorksDictionary[actWorkId]);
            }

            writeRepository.Update(databaseEntity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedEntity = await readRepository.GetById(databaseEntity.Id, cancellationToken)
                ?? throw new InvalidOperationException($"Не удалось найти акт с идентификатором {databaseEntity.Id}");

            return mapper.Map<ActModel>(updatedEntity);
        }

        async Task IActServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти акт с идентификатором {id}");

            var existingActWorks = entity.ActWorks.ToList();

            foreach (var existingActWork in existingActWorks)
            {
                actWorkWriteRepository.Delete(existingActWork);
            }

            writeRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ValidateConnections(ActCreateModel model, CancellationToken cancellationToken)
        {
            if (await readRepository.Any(x => x.ActNumber == model.ActNumber, cancellationToken))
            {
                throw new WorksDuplicateException($"Акт с номером {model.ActNumber} уже существует");
            }

            if (await customerReadRepository.GetById(model.CustomerId, cancellationToken) == null)
            {
               throw new WorksNotFoundException($"Не удалось найти заказчика с идентификатором {model.CustomerId}");
            }

            if (await executorReadRepository.GetById(model.ExecutorId, cancellationToken) == null)
            {
                throw new WorksNotFoundException($"Не удалось найти исполнителя с идентификатором {model.ExecutorId}");
            }

            var modelWorkIds = model.ActWorks.Select(x => x.WorkId).ToList();

            var existingWorks = await workReadRepository.GetByIds(modelWorkIds, cancellationToken);

            var workIdsInDatabase = existingWorks.Select(x => x.Id);

            var modelWorkIdsDistinct = modelWorkIds.Distinct().ToList();

            if (modelWorkIds.Count != modelWorkIdsDistinct.Count)
            {
                throw new WorksDuplicateException($"Нельзя использовать одну и ту же работу с входящми идентификаторами: ({string.Join(", ", modelWorkIds)}) более 1 раза");
            }

            var missingIds = modelWorkIds.Except(workIdsInDatabase).ToList();

            if (missingIds.Count > 0)
            {
                throw new WorksNotFoundException($"Не удалось найти работы с идентификаторами: {string.Join(", ", missingIds)}");
            }
        }
    }
}