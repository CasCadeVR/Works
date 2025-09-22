namespace CasCadeVR.Works.Web.Models.Works;

/// <summary>
/// Модель создания работы
/// </summary>
public class WorkCreateRequestApiModel : WorkApiGenericModel
{
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public Guid UnitOfMeasureId { get; set; }
}
