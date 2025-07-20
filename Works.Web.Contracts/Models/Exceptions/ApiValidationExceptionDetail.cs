using Works.Services.Contracts.Exceptions;

namespace Works.Web.Contracts.Models.Exceptions;

/// <summary>
/// 
/// </summary>
public class ApiValidationExceptionDetail
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<InvalidateItemModel> Errors { get; set; } = Array.Empty<InvalidateItemModel>();
}
