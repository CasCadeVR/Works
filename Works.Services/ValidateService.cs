using FluentValidation;
using Works.Services.Contracts;
using Works.Services.Contracts.Exceptions;
using Works.Services.Validators;
using Works.Services.Contracts.Models.Works;
using Works.Services.Contracts.Models.Customers;
using Works.Services.Contracts.Models.Executors;
using Works.Services.Contracts.Models.ActWorks;
using Works.Services.Contracts.Models.Acts;

namespace Works.Services;

/// <inheritdoc cref="IValidateService"/>
public class ValidateService : IValidateService
{
    private readonly IDictionary<Type, IValidator> validators;

    /// <summary>
    /// ctor
    /// </summary>
    public ValidateService()
    {
        validators = new Dictionary<Type, IValidator>();
        validators.TryAdd(typeof(WorksCreateModel), new WorksCreateModelValidator());
        validators.TryAdd(typeof(CustomerCreateModel), new CustomerCreateModelValidator());
        validators.TryAdd(typeof(ExecutorCreateModel), new ExecutorCreateModelValidator());
        validators.TryAdd(typeof(ActWorksCreateModel), new ActWorksCreateModelValidator());
        validators.TryAdd(typeof(ActCreateModel), new ActCreateModelValidator());
    }
    
    async Task IValidateService.Validate<TModel>(TModel model, CancellationToken cancellationToken)
        where TModel : class
    {
        if (!validators.TryGetValue(model.GetType(), out var validator))
        {
            throw new WorksInvalidOperationException($"Не найден запрашиваемый валидатор : {model.GetType()}");
        }

        var context = new ValidationContext<TModel>(model);
        var validationResult = await validator.ValidateAsync(context, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new WorksValidationException(validationResult.Errors.Select(x => InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
        }
    }
}
