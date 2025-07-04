using FluentValidation;
using Works.Services.Contracts;
using Works.Services.Contracts.Exceptions;
using Works.Services.Contracts.Models;
using Works.Services.Validators;

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
        validators.TryAdd(typeof(WorksModel), new WorksModelValidator());
    }
    
    async Task IValidateService.Validate<TModel>(TModel model, CancellationToken cancellationToken)
        where TModel : class
    {
        if (!validators.TryGetValue(model.GetType(), out var validator))
        {
            throw new WorksInvalideOperationException($"Не найден запрашиваемый валидатор : {model.GetType()}");
        }

        var context = new ValidationContext<TModel>(model);
        var validationResult = await validator.ValidateAsync(context, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new WorksValidationException(validationResult.Errors.Select(x => InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
        }
    }
}
