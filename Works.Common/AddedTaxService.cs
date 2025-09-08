using CasCadeVR.Works.Common.Contracts;

namespace CasCadeVR.Works.Common;

/// <inheritdoc cref="IAddedTaxService"/>/>
public class AddedTaxService : IAddedTaxService
{
    private decimal givenTaxRate;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AddedTaxService"/>
    /// </summary>
    public AddedTaxService(decimal givenTaxRate)
    {
        this.givenTaxRate = givenTaxRate;
    }

    /// <inheritdoc cref="IAddedTaxService.GetNdsRate"/>/>
    public decimal GetNdsRate()
    {
        return givenTaxRate;
    }
}
