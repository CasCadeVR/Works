using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;

namespace CasCadeVR.Works.Services.Contracts.Models.Works;

/// <summary>
/// Модель работы
/// </summary>
public class WorksModel : WorksGenericModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Объект передачи данных <see cref="UnitOfMeasureModel"/>
    /// </summary>
    public UnitOfMeasureModel UnitOfMeasure { get; set; } = null!;
}