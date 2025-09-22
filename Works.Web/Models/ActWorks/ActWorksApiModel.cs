using CasCadeVR.Works.Web.Models.Works;

namespace CasCadeVR.Works.Web.Models.ActWorks;

/// <summary>
/// Модель работы для акта
/// </summary>
public class ActWorksApiModel : ActWorkApiGenericModel
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
    /// Работа
    /// </summary>
    public WorkApiModel Work { get; set; } = null!;
}