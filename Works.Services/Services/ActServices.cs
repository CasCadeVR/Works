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
            await ValidateActNumber(model.ActNumber, cancellationToken);

            var existingCustomer = await customerReadRepository.GetById(model.CustomerId, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти заказчика с идентификатором {model.CustomerId}");

            var existingExecutor =  await executorReadRepository.GetById(model.ExecutorId, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти исполнителя с идентификатором {model.ExecutorId}");

            var modelActWorks = mapper.Map<ICollection<ActWork>>(model.ActWorks);

            var modelWorkIds = model.ActWorks.Select(x => x.WorkId).ToList();

            var existingWorks = await workReadRepository.GetByIds(modelWorkIds, cancellationToken);

            var workIdsInDatabase = existingWorks.Select(x => x.Id);

            var missingIds = modelWorkIds.Except(workIdsInDatabase);

            if (missingIds.Any())
            {
                throw new WorksNotFoundException($"Не удалось найти работы с идентификаторами: {string.Join(", ", missingIds)}");
            }

            var existingActWorksDictionary = existingWorks.ToDictionary(x => x.Id);

            var result = new Act
            {
                ActNumber = model.ActNumber,
                Date = model.Date,
                CustomerId = model.CustomerId,
                Customer = existingCustomer,
                ExecutorId = model.ExecutorId,
                Executor = existingExecutor,
                ActWorks = modelActWorks,
            };

            foreach (var actWork in result.ActWorks)
            {
                actWork.Work = existingActWorksDictionary[actWork.WorkId];
                actWork.CapturedPrice = actWork.Work.Price;
                actWorkWriteRepository.Add(actWork);
            }

            writeRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ActModel>(result);
        }

        async Task<ActModel> IActServices.Update(Guid id, ActCreateModel model, CancellationToken cancellationToken)
        {
            var databaseResponseEntity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти акт с идентификатором {id}");

            var databaseEntity = mapper.Map<Act>(databaseResponseEntity);

            await ValidateActNumber(model.ActNumber, cancellationToken);

            var existingCustomer = await customerReadRepository.GetById(model.CustomerId, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти заказчика с идентификатором {model.CustomerId}");

            var existingExecutor = await executorReadRepository.GetById(model.ExecutorId, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти исполнителя с идентификатором {model.ExecutorId}");

            var modelActWorks = mapper.Map<ICollection<ActWork>>(model.ActWorks);

            var modelWorkIds = model.ActWorks.Select(x => x.WorkId).ToList();

            var existingWorks = await workReadRepository.GetByIds(modelWorkIds, cancellationToken);
            var existingWorksDictionary = existingWorks.ToDictionary(x => x.Id);

            var workIdsInDatabase = existingWorks.Select(x => x.Id);

            var missingIds = modelWorkIds.Except(workIdsInDatabase).ToList();

            if (missingIds.Count > 0)
            {
                throw new WorksNotFoundException($"Не удалось найти работы с идентификаторами: {string.Join(", ", missingIds)}");
            }

            databaseEntity.ActNumber = model.ActNumber;
            databaseEntity.Date = model.Date;
            databaseEntity.CustomerId = model.CustomerId;
            databaseEntity.Customer = existingCustomer;
            databaseEntity.ExecutorId = model.ExecutorId;
            databaseEntity.Executor = existingExecutor;

            var existingActWorks = databaseEntity.ActWorks;
            var existingActWorksDictionary = existingActWorks.ToDictionary(x => x.WorkId);

            foreach (var actWork in modelActWorks)
            {
                if (existingActWorksDictionary.TryGetValue(actWork.WorkId, out var foundActWork))
                {
                    foundActWork.Quantity = actWork.Quantity;
                    actWorkWriteRepository.Update(foundActWork);
                } 
                else
                {
                    actWork.ActId = databaseEntity.Id;
                    actWork.Work = existingWorksDictionary[actWork.WorkId];
                    actWork.CapturedPrice = actWork.Work.Price;
                    actWorkWriteRepository.Add(actWork);
                }
            }

            var actWorksIdsToDelete = existingActWorks.Select(x => x.WorkId).Except(modelActWorks.Select(x => x.WorkId)).ToList();

            foreach (var actWorkId in actWorksIdsToDelete)
            {
                if (existingActWorksDictionary.TryGetValue(actWorkId, out var foundActWork))
                {
                    actWorkWriteRepository.Delete(foundActWork);
                }
            }

            writeRepository.Update(databaseEntity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            foreach (var actWorkId in actWorksIdsToDelete)
            {
                if (existingActWorksDictionary.TryGetValue(actWorkId, out var foundActWork))
                {
                    existingActWorks.Remove(foundActWork);
                }
            }

            return mapper.Map<ActModel>(databaseEntity);
        }

        async Task IActServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var databaseResponseEntity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти акт с идентификатором {id}");

            var databaseEntity = mapper.Map<Act>(databaseResponseEntity);

            var existingActWorks = databaseEntity.ActWorks;

            foreach (var existingActWork in existingActWorks)
            {
                actWorkWriteRepository.Delete(existingActWork);
            }

            writeRepository.Delete(databaseEntity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ValidateActNumber(string ActNumber, CancellationToken cancellationToken)
        {
            if (await readRepository.Any(x => x.ActNumber == ActNumber, cancellationToken))
            {
                throw new WorksDuplicateException($"Акт с номером {ActNumber} уже существует");
            }
        }
    }
}