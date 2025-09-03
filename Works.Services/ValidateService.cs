using FluentValidation;
using CasCadeVR.Works.Services.Contracts;
using CasCadeVR.Works.Services.Contracts.Exceptions;
using CasCadeVR.Works.Services.Validators;
using CasCadeVR.Works.Services.Contracts.Models.Works;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;

namespace CasCadeVR.Works.Services;

/// <inheritdoc cref="IValidateService"/>
public class ValidateService : IValidateService
{
    private readonly IDictionary<Type, IValidator> validators;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ValidateService"/>
    /// </summary>
    public ValidateService()
    {
        validators = new Dictionary<Type, IValidator>();
        validators.TryAdd(typeof(UnitOfMeasureCreateModel), new UnitOfMeasureCreateModelValidator());
        validators.TryAdd(typeof(WorksCreateModel), new WorksCreateModelValidator());
        validators.TryAdd(typeof(CustomerCreateModel), new CustomerCreateModelValidator());
        validators.TryAdd(typeof(ExecutorCreateModel), new ExecutorCreateModelValidator());
        validators.TryAdd(typeof(ActCreateModel), new ActCreateModelValidator());
    }
    
    async Task IValidateService.Validate<TModel>(TModel model, CancellationToken cancellationToken)
        where TModel : class
    {
        if (!validators.TryGetValue(model.GetType(), out var validator))
        {
            throw new InvalidOperationException($"Не найден запрашиваемый валидатор : {model.GetType()}");
        }

        var context = new ValidationContext<TModel>(model);
        var validationResult = await validator.ValidateAsync(context, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new WorksValidationException(validationResult.Errors.Select(x => new InvalidateItemModel(x.PropertyName, x.ErrorMessage)));
        }
    }
}
