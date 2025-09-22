namespace CasCadeVR.Works.Services.Contracts.Models.ActWorks;

/// <summary>
/// Модель создания работы для акта
/// </summary>
public class ActWorksCreateModel : ActWorksGenericModel
{
    /// <summary>
    /// Идентификатор работы
    /// </summary>
    public Guid WorkId { get; set; }
}