namespace CasCadeVR.Works.Web.Models.Executors;

/// <summary>
/// Модель исполнителя
/// </summary>
public class ExecutorApiModel : ExecutorCreateRequestApiModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}
