using System.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Works.Context.Contracts;
using Works.Entities;
using Works.Repository.Contracts.IReadRepositories;
using Works.Repository.Contracts.IWriteRepositories;
using Works.Services.Contracts.Exceptions;
using Works.Services.Contracts.IServices;
using Works.Services.Contracts.Models.Acts;

namespace Works.Services.Services
{
    /// <inheritdoc cref="IWorksServices"/>
    public class ActServices : IActServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IActReadRepository readRepository;
        private readonly IWorksReadRepository workReadRepository;
        private readonly ICustomerReadRepository customerReadRepository;
        private readonly IExecutorReadRepository executorReadRepository;
        private readonly IActWorkWriteRepository actWorkwriteRepository;
        private readonly IActWriteRepository writeRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public ActServices(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IActReadRepository readRepository,
            IWorksReadRepository workReadRepository,
            ICustomerReadRepository customerReadRepository,
            IExecutorReadRepository executorReadRepository,
            IActWorkWriteRepository actWorkwriteRepository,
            IActWriteRepository writeRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.readRepository = readRepository;
            this.workReadRepository = workReadRepository;
            this.customerReadRepository = customerReadRepository;
            this.executorReadRepository = executorReadRepository;
            this.actWorkwriteRepository = actWorkwriteRepository;
            this.writeRepository = writeRepository;
        }

        async Task<ActModel> IActServices.GetById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
                 ?? throw new WorksNotFoundException($"Не удалось найти акт с иденитификатором {id}");

            return mapper.Map<ActModel>(entity);
        }

        async Task<IReadOnlyCollection<ActModel>> IActServices.GetAll(CancellationToken cancellationToken)
        {
            var items = await readRepository.GetAll(cancellationToken);
            return mapper.Map<IReadOnlyCollection<ActModel>>(items);
        }

        async Task<ActModel> IActServices.Create(ActCreateModel model, CancellationToken cancellationToken)
        {
            var targetCustomer = await customerReadRepository.GetById(model.CustomerId, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти заказчика с иденитификатором {model.CustomerId}");

            var targetExecutor = await executorReadRepository.GetById(model.ExecutorId, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти исполнителя с иденитификатором {model.CustomerId}");

            var targetWorks = await workReadRepository.GetByIds(model.Works.Select(x => x.WorkId).ToList(), cancellationToken);

            if (model.Works.Select(x => x.WorkId).ToList().GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key).Count() != 0)
            {
                throw new WorksInvalidOperationException($"Нельзя использовать одну и ту же работу более 1 раза");
            }

            if (targetWorks.Count != model.Works.Count)
                foreach (var work in model.Works)
                    if (!targetWorks.Select(x => x.Id).Contains(work.WorkId))
                        throw new WorksNotFoundException($"Не удалось найти работы с иденитификаторами {work.WorkId}");

            var result = new Act
            {
                Id = Guid.NewGuid(),
                ActNumber = model.ActNumber,
                Date = model.Date,
                Customer = targetCustomer,
                Executor = targetExecutor,
                NDS = model.NDS,
                Works = mapper.Map<ICollection<ActWork>>(model.Works),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DeletedAt = null,
            };

            foreach (var actWork in result.Works)
            {
                actWork.Act = result;
                actWork.Work = targetWorks.First(x => x.Id == actWork.WorkId);
                actWorkwriteRepository.Add(actWork);
            }

            writeRepository.Add(result);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ActModel>(result);
        }

        async Task<ActModel> IActServices.Update(ActModel model, CancellationToken cancellationToken)
        {
            var targetCustomer = await customerReadRepository.GetById(model.Customer.Id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти заказчика с иденитификатором {model.Customer.Id}");

            var targetExecutor = await executorReadRepository.GetById(model.Executor.Id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти исполнителя с иденитификатором {model.Executor.Id}");

            var targetWorks = await workReadRepository.GetByIds(model.Works.Select(x => x.Work.Id).ToList(), cancellationToken);

            if (model.Works.Select(x => x.Work.Id).ToList().GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key).Count() != 0)
            {
                throw new WorksInvalidOperationException($"Нельзя использовать одну и ту же работу более 1 раза");
            }

            if (targetWorks.Count != model.Works.Count)
                foreach (var work in model.Works)
                    if (!targetWorks.Select(x => x.Id).Contains(work.Work.Id))
                        throw new WorksNotFoundException($"Не удалось найти работы с иденитификаторами {work.Work.Id}");

            var entity = await readRepository.GetById(model.Id, cancellationToken)
                ?? throw new WorksNotFoundException($"Не удалось найти акт с иденитификатором {model.Id}");

            entity.ActNumber = model.ActNumber;
            entity.Date = model.Date;
            entity.Customer = targetCustomer;
            entity.CustomerId = model.Customer.Id;
            entity.Executor = targetExecutor;
            entity.ExecutorId = model.Executor.Id;
            entity.NDS = model.NDS;
            entity.UpdatedAt = DateTime.Now;

            var modelWorks = mapper.Map<ICollection<ActWork>>(model.Works);

            foreach (var actWork in modelWorks)
            {
                if (entity.Works.Select(x => x.WorkId).Contains(actWork.WorkId))
                {
                    var existingActWork = entity.Works.First(w => w.WorkId == actWork.WorkId);
                    existingActWork.Quantity = actWork.Quantity;
                    existingActWork.ActualPrice = actWork.ActualPrice;
                    actWorkwriteRepository.Update(existingActWork);
                    continue;
                }

                actWork.Act = entity;
                actWork.Work = targetWorks.First(x => x.Id == actWork.WorkId);
                actWorkwriteRepository.Add(actWork);
                entity.Works.Add(actWork);
            }

            var actWorksToKeep = modelWorks.Select(w => w.WorkId);
            var actWorksToDelete = entity.Works.Where(w => !actWorksToKeep.Contains(w.WorkId)).ToList();

            foreach (var actWork in actWorksToDelete)
            {
                entity.Works.Remove(actWork);
                actWorkwriteRepository.Delete(actWork);
            }

            writeRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ActModel>(entity);
        }

        async Task IActServices.Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await readRepository.GetById(id, cancellationToken)
               ?? throw new WorksNotFoundException($"Не удалось найти акт с иденитификатором {id}");

            writeRepository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}