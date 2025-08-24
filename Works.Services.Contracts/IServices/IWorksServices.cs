using CasCadeVR.Works.Services.Contracts.Models.Works;

namespace CasCadeVR.Works.Services.Contracts.IServices;

/// <summary>
/// Сервис по работе с работами
/// </summary>
public interface IWorksServices
{
    /// <summary>
    /// Возвращает <see cref="WorksModel"/> по идентификатору
    /// </summary>
    Task<WorksModel> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает список <see cref="WorksModel"/>
    /// </summary>
    Task<IReadOnlyCollection<WorksModel>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новый <see cref="WorksModel"/>
    /// </summary>
    Task<WorksModel> Create(WorksCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Редактирует существующий <see cref="WorksModel"/>
    /// </summary>
    Task<WorksModel> Update(WorksModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет существующий <see cref="WorksModel"/>
    /// </summary>
    Task Delete(Guid id, CancellationToken cancellationToken);
}