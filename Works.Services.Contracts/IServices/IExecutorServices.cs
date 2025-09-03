using CasCadeVR.Works.Services.Contracts.Models.Executors;

namespace CasCadeVR.Works.Services.Contracts.IServices;

/// <summary>
/// Сервис по работе с исполнителями
/// </summary>
public interface IExecutorServices
{
    /// <summary>
    /// Возвращает <see cref="ExecutorModel"/> по идентификатору
    /// </summary>
    Task<ExecutorModel> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает список <see cref="ExecutorModel"/>
    /// </summary>
    Task<IReadOnlyCollection<ExecutorModel>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новый <see cref="ExecutorModel"/>
    /// </summary>
    Task<ExecutorModel> Create(ExecutorCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Редактирует существующий <see cref="ExecutorModel"/>
    /// </summary>
    Task<ExecutorModel> Update(Guid id, ExecutorCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет существующий <see cref="ExecutorModel"/>
    /// </summary>
    Task Delete(Guid id, CancellationToken cancellationToken);
}