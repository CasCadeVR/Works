namespace CasCadeVR.Works.Services.Contracts.Models.Executors;

/// <summary>
/// Модель исполнителя
/// </summary>
public class ExecutorModel : ExecutorCreateModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}