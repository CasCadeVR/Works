namespace CasCadeVR.Works.Web.Models.UnitOfMeasure;

/// <summary>
/// Модель единицы измерения
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Имя единицы измерения</param>
public record UnitOfMeasureApiModel(
    Guid Id, 
    string Name
) : UnitOfMeasureCreateRequestApiModel(Name);
