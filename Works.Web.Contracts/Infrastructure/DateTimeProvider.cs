using Works.Common;

namespace Works.Web.Contracts.Infrastructure;

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
