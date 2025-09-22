namespace CasCadeVR.Works.Services.Contracts.Models.Works;

/// <summary>
/// Модель создания работы
/// </summary>
public class WorksCreateModel : WorksGenericModel
{
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public Guid UnitOfMeasureId { get; set; }
}