namespace CasCadeVR.Works.Context.Contracts;

/// <summary>
/// Определеяет интерфейс для unit of work
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Асинхронно сохраняет все изменения
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); 
}
