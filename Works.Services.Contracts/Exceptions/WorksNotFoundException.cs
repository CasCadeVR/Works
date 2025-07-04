namespace Works.Services.Contracts.Exceptions;

/// <summary>
/// 
/// </summary>
public class WorksNotFoundException : WorksException
{
    /// <summary>
    /// 
    /// </summary>
    public WorksNotFoundException(string message)
        : base(message)
    {
        
    }
}
