using CasCadeVR.Works.Common;

namespace CasCadeVR.Works.Web.Contracts.Infrastructure;

/// <summary>
/// <inheritdoc cref="IDateTimeProvider"/>
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    DateTimeOffset IDateTimeProvider.UtcNow()
    => DateTimeOffset.UtcNow;

    DateTimeOffset IDateTimeProvider.Now()
        => DateTimeOffset.Now;
}
