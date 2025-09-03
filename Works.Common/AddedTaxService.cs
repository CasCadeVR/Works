using CasCadeVR.Works.Common.Contracts;

namespace CasCadeVR.Works.Common;

/// <inheritdoc cref="IAddedTaxService"/>/>
public class AddedTaxService : IAddedTaxService
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AddedTaxService"/>
    /// </summary>
    public AddedTaxService() { }

    /// <inheritdoc cref="IAddedTaxService.GetNdsRate"/>/>
    public decimal GetNdsRate()
    {
        return 20;
    }
}
