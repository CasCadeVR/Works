using System.Diagnostics.CodeAnalysis;

namespace Works.Context.Contracts;

/// <summary>
/// Интерфейс создания и модификации записей в контексте
/// </summary>
public interface IWriter
{
    /// <summary>
    /// Добавляет новую запись
    /// </summary>
    void Add<TEntity>([NotNull] TEntity entity) where TEntity : class;

    /// <summary>
    /// Изменить запись
    /// </summary>
    void Update<TEntity>([NotNull] TEntity entity) where TEntity : class;

    /// <summary>
    /// Удалить запись
    /// </summary>
    void Delete<TEntity>([NotNull] TEntity entity) where TEntity : class;
}