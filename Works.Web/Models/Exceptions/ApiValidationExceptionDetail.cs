using CasCadeVR.Works.Services.Contracts.Exceptions;
namespace CasCadeVR.Works.Web.Models.Exceptions;

/// <summary>
/// Информация об ошибке валидации модели АПИ
/// </summary>
public class ApiValidationExceptionDetail
{
    /// <summary>
    /// Список ошибок валидации модели АПИ
    /// </summary>
    public IEnumerable<InvalidateItemModel> Errors { get; set; } = [];
}
