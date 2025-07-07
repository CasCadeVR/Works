using Works.Services.Contracts.Exceptions;
using Works.Web.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Works.Web.Infrastructure;

/// <summary>
/// Фильтр обработки ошибок
/// </summary>
public class WorksExceptionFilter : IExceptionFilter
{
    void IExceptionFilter.OnException(ExceptionContext context)
    {
        if (context.Exception is not WorksException exception)
        {
            return;
        }

        switch (exception)
        {
            case WorksNotFoundException ex:
                SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail(ex.Message))
                {
                    StatusCode = StatusCodes.Status404NotFound,
                }, context);
                break;

            case WorksInvalidOperationException ex:
                SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(ex.Message))
                {
                    StatusCode = StatusCodes.Status406NotAcceptable,
                }, context);
                break;

            case WorksValidationException ex:
                SetDataToContext(new BadRequestObjectResult(new ApiValidationExceptionDetail()
                {
                    Errors = ex.Errors
                })
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                }, context);
                break;

            default:
                SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(exception.Message)), context);
                break;
        }
    }

    private static void SetDataToContext(ObjectResult data, ExceptionContext context)
    {
        context.ExceptionHandled = true;
        var response = context.HttpContext.Response;
        response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
        context.Result = data;
    }
}