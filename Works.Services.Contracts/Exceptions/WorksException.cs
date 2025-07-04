namespace Works.Services.Contracts.Exceptions;

/// <summary>
/// 
/// </summary>
public class WorksException : Exception
{
    /// <summary>
    /// 
    /// </summary>
    protected WorksException() { }
    /// <summary>
    /// 
    /// </summary>
    protected WorksException(string field)
        => new WorksException( field);
}
