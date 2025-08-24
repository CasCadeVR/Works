namespace CasCadeVR.Works.Services.Contracts;

/// <summary>
/// Сервис валидации
/// </summary>
public interface IValidateService
{
    /// <summary>
    /// Провалидировать модель
    /// </summary>
    Task Validate<TModel>(TModel model, CancellationToken cancellationToken)
        where TModel : class;
}
