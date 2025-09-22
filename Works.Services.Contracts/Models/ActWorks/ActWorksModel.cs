using CasCadeVR.Works.Services.Contracts.Models.Works;

namespace CasCadeVR.Works.Services.Contracts.Models.ActWorks;

/// <summary>
/// Модель работы для акта
/// </summary>
public class ActWorksModel :  ActWorksGenericModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Цена работы на момент создания акта
    /// </summary>
    public decimal CapturedPrice { get; set; } = 0;

    /// <summary>
    /// Объект передачи данных <see cref="WorksModel"/>
    /// </summary>
    public WorksModel Work { get; set; } = null!;
}