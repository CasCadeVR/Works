namespace CasCadeVR.Works.Web.Models.UnitOfMeasure;

/// <summary>
/// Модель создания единицы измерения
/// </summary>
public class UnitOfMeasureCreateRequestApiModel
{
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = string.Empty;
}