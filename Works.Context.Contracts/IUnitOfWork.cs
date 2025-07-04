namespace Works.Context.Contracts;

/// <summary>
/// Определеяет интерфейс для unit of work
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Асинхронно сохраняет все изменения
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); 
}
