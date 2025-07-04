using Works.Services.Contracts.Models;

namespace Works.Services.Contracts;

/// <summary>
/// Сервис по работе с работами
/// </summary>
public interface IWorksServices
{
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
}
