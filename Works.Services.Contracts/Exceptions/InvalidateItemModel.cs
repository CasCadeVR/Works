namespace Works.Services.Contracts.Exceptions;

/// <summary>
/// 
/// </summary>
public class InvalidateItemModel
{
    /// <summary>
    /// 
    /// </summary>
    public static InvalidateItemModel New(string field, string message)
        => new InvalidateItemModel( field,  message);

    /// <summary>
    /// 
    /// </summary>
    public InvalidateItemModel(string field, string message)
    {
        Field = field;
        Message = message;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Field { get; } =string.Empty;

    /// <summary>
    /// 
    /// </summary>
    public string Message { get; } = string.Empty;
}
