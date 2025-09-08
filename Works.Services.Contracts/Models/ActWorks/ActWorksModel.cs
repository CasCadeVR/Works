using CasCadeVR.Works.Services.Contracts.Models.Works;

namespace CasCadeVR.Works.Services.Contracts.Models.ActWorks;

/// <summary>
/// Модель работы для акта
/// </summary>
public class ActWorksModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Объект передачи данных <see cref="WorksModel"/>
    /// </summary>
    public WorksModel Work { get; set; } = null!;

    /// <summary>
    /// Количество
    /// </summary>
    public int Quantity { get; set; }
}