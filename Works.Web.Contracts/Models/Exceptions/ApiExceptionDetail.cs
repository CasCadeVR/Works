namespace CasCadeVR.Works.Web.Contracts.Models.Exceptions;

/// <summary>
/// Информация об ошибке работы АПИ
/// </summary>
public class ApiExceptionDetail
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ApiExceptionDetail"/>
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