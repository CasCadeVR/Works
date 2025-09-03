using CasCadeVR.Works.Repository.Contracts.IReadRepositories;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;
using CasCadeVR.Works.Repository.ReadRepositories;
using CasCadeVR.Works.Repository.WriteRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace CasCadeVR.Works.Repository;

/// <summary>
/// Статический класс для регистрации репозиториев
/// </summary>
public static class RegisterAnchor
{
    /// <summary>
    /// Зарегистрировать репозитории
    /// </summary>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfMeasureWriteRepository, UnitOfMeasureWriteRepository>();
        services.AddScoped<IUnitOfMeasureReadRepository, UnitOfMeasureReadRepository>();

        services.AddScoped<IWorksReadRepository, WorksReadRepository>();
        services.AddScoped<IWorksWriteRepository, WorksWriteRepository>();

        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

        services.AddScoped<IExecutorReadRepository, ExecutorReadRepository>();
        services.AddScoped<IExecutorWriteRepository, ExecutorWriteRepository>();

        services.AddScoped<IActReadRepository, ActReadRepository>();
        services.AddScoped<IActWriteRepository, ActWriteRepository>();


        services.AddScoped<IActWorkWriteRepository, ActWorkWriteRepository>();

        return services;
    }
}
