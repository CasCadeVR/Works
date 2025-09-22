using CasCadeVR.Works.Web.Models.UnitOfMeasure;

namespace CasCadeVR.Works.Web.Models.Works;

/// <summary>
/// Модель работы
/// </summary>
public class WorkApiModel : WorkApiGenericModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Объект передачи данных <see cref="UnitOfMeasureApiModel"/>
    /// </summary>
    public UnitOfMeasureApiModel UnitOfMeasure { get; set; } = null!;
}