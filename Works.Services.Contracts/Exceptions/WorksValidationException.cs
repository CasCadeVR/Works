namespace Works.Services.Contracts.Exceptions;

/// <summary>
/// 
/// </summary>
public class WorksValidationException : WorksException
{
    /// <summary>
    /// 
    /// </summary>
    public WorksValidationException(IEnumerable<InvalidateItemModel> errors)
    {
        Errors = errors;
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<InvalidateItemModel> Errors { get; private set; }
}
