using System.Diagnostics.CodeAnalysis;

namespace Works.Context.Contracts;

/// <summary>
/// 
/// </summary>
public interface IDBWriter<in TEntity> where TEntity : class
{
    /// <summary>
    /// Добавляет новую запись
    /// </summary>
    void Add([NotNull] TEntity entity);

    /// <summary>
    /// Изменить запись
    /// </summary>
    void Update([NotNull] TEntity entity);

    /// <summary>
    /// Удалить запись
    /// </summary>
    void Delete([NotNull] TEntity entity);
}
