using CasCadeVR.Works.Common.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CasCadeVR.Works.Common;

/// <summary>
/// Статический класс для регистрации вспомогательных сервисов
/// </summary>
public static class RegisterAnchor
{
    /// <summary>
    /// Зарегистрировать вспомогательные сервисы
    /// </summary>
    public static IServiceCollection RegisterCommonServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}