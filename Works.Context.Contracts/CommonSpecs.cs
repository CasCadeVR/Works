using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace Works.Context.Contracts;

/// <summary>
/// Общие спецификации чтения
/// </summary>
public static class CommonSpecs
{
    /// <summary>
    /// Активные. Нe удалённые.
    /// </summary>
    public static IQueryable<TEntity> NotDeletedAt<TEntity>(this IQueryable<TEntity> query)
        where TEntity : class, IEntitySoftDeleted
        => query.Where(x => x.DeletedAt == null);

    /// <summary>
    /// По идентификатору
    /// </summary>
    public static IQueryable<TEntity> ById<TEntity>(this IQueryable<TEntity> query, Guid id)
        where TEntity : class, IEntityWithId
        => query.Where(x => x.Id == id);

    /// <summary>
    /// По идентификаторам
    /// </summary>
    public static IQueryable<TEntity> ByIds<TEntity>(this IQueryable<TEntity> query, IReadOnlyCollection<Guid> ids)
        where TEntity : class, IEntityWithId
    {
        var qty = ids.Count;
        return qty switch
        {
            0 => query.Where(x => false),
            1 => query.ById(ids.First()),
            _ => query.Where(x => ids.Contains(x.Id))
        };
    }

    /// <summary>
    /// Возвращает <see cref="IReadOnlyCollection{TEntity}"/>
    /// </summary>
    public static Task<IReadOnlyCollection<TEntity>> ToReadOnlyCollectionAsync<TEntity>(this IQueryable<TEntity> query,
        CancellationToken cancellationToken)
        => query.ToListAsync(cancellationToken)
            .ContinueWith(x => new ReadOnlyCollection<TEntity>(x.Result) as IReadOnlyCollection<TEntity>,
                cancellationToken);
}