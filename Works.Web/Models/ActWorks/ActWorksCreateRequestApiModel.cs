namespace CasCadeVR.Works.Web.Models.ActWorks;

/// <summary>
/// Модель создания работы для акта
/// </summary>
public class ActWorksCreateRequestApiModel : ActWorkApiGenericModel
{
    /// <summary>
    /// Идентификатор работы
    /// </summary>
    public Guid WorkId { get; set; }
}