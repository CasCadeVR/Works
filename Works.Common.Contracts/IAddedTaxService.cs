namespace CasCadeVR.Works.Common.Contracts;

/// <summary>
/// Сервис по вычислению налога на добавленную стоимость
/// </summary>
public interface IAddedTaxService
{
    /// <summary>
    /// Получить налоговую ставку
    /// </summary>
    decimal GetNdsRate();
}
