using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;

namespace CasCadeVR.Works.Services.Contracts.IServices;

/// <summary>
/// Сервис по работе с работами
/// </summary>
public interface IUnitOfMeasureServices
{
    /// <summary>
    /// Возвращает <see cref="UnitOfMeasureModel"/> по идентификатору
    /// </summary>
    Task<UnitOfMeasureModel> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает список <see cref="UnitOfMeasureModel"/>
    /// </summary>
    Task<IReadOnlyCollection<UnitOfMeasureModel>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новый <see cref="UnitOfMeasureModel"/>
    /// </summary>
    Task<UnitOfMeasureModel> Create(UnitOfMeasureCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Редактирует существующий <see cref="UnitOfMeasureModel"/>
    /// </summary>
    Task<UnitOfMeasureModel> Update(Guid id, UnitOfMeasureCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет существующий <see cref="UnitOfMeasureModel"/>
    /// </summary>
    Task Delete(Guid id, CancellationToken cancellationToken);
}