using CasCadeVR.Works.Common.Contracts;

namespace CasCadeVR.Works.Common;

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
