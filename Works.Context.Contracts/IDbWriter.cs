using System.Diagnostics.CodeAnalysis;

namespace CasCadeVR.Works.Context.Contracts;

/// <summary>
/// Интерфейс создания и модификации записей в контексте базы данных
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
