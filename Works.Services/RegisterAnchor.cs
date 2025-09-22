using CasCadeVR.Works.Services.Contracts;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CasCadeVR.Works.Services;

/// <summary>
/// Статический класс для регистрации сервисов
/// </summary>
public static class RegisterAnchor
{
    /// <summary>
    /// Зарегистрировать сервисы
    /// </summary>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfMeasureServices, UnitOfMeasureServices>();
        services.AddScoped<IWorksServices, WorksServices>();
        services.AddScoped<ICustomerServices, CustomerService>();
        services.AddScoped<IExecutorServices, ExecutorServices>();
        services.AddScoped<IActServices, ActServices>();

        services.AddScoped<IValidateService, ValidateService>();

        return services;
    }
}
