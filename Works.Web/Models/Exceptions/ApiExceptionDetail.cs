namespace Works.Web.Models.Exceptions;

/// <summary>
/// Информация об ошибке работы АПИ
/// </summary>
public class ApiExceptionDetail
{
    /// <summary>
    /// ctor
    /// </summary>
    public ApiExceptionDetail(string message)
    {
        Message = message;
    }
    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    public string Message { get; }
}