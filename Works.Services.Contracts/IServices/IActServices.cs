using CasCadeVR.Works.Services.Contracts.Models.Acts;

namespace CasCadeVR.Works.Services.Contracts.IServices;

/// <summary>
/// Сервис по работе с актом
/// </summary>
public interface IActServices
{
    /// <summary>
    /// Возвращает <see cref="ActModel"/> по идентификатору
    /// </summary>
    Task<ActModel> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает список <see cref="ActModel"/>
    /// </summary>
    Task<IReadOnlyCollection<ActModel>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новый <see cref="ActModel"/>
    /// </summary>
    Task<ActModel> Create(ActCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Редактирует существующий <see cref="ActModel"/>
    /// </summary>
    Task<ActModel> Update(ActModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет существующий <see cref="ActModel"/>
    /// </summary>
    Task Delete(Guid id, CancellationToken cancellationToken);
}