using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CasCadeVR.Works.Services;

/// <summary>
/// Статический класс для регистрации репозиториев
/// </summary>
public static class RegisterRepositories
{
    /// <summary>
    /// Зарегистрировать репозитории
    /// </summary>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfMeasureServices, UnitOfMeasureServices>();
        services.AddScoped<IWorksServices, WorksServices>();
        services.AddScoped<ICustomerServices, CustomerService>();
        services.AddScoped<IExecutorServices, ExecutorServices>();
        services.AddScoped<IActServices, ActServices>();

        return services;
    }
}
