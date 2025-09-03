using System.Linq.Expressions;
using CasCadeVR.Works.Entities;

namespace CasCadeVR.Works.Repository.Contracts.IReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="UnitOfMeasure"/>
/// </summary>
public interface IUnitOfMeasureReadRepository
{
    /// <summary>
    /// Возвращает true, если совпадает условие
    /// </summary>
    Task<bool> Any(Expression<Func<UnitOfMeasure, bool>> action, CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="UnitOfMeasure"/> по идентификатору
    /// </summary>
    Task<UnitOfMeasure?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="UnitOfMeasure"/>
    /// </summary>
    Task<IReadOnlyCollection<UnitOfMeasure>> GetAll(CancellationToken cancellationToken);
}

