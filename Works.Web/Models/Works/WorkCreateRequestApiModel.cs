namespace CasCadeVR.Works.Web.Models.Works;

/// <summary>
/// Модель создания работы
/// </summary>
/// <param name="Name">Наименование</param>
/// <param name="Description">Описание</param>
/// <param name="Price">Цена</param>
/// <param name="UnitOfMeasureId">Идентификатор единицы измерения</param>
public record WorkCreateRequestApiModel(
    string Name,
    string Description,
    decimal Price,
    Guid UnitOfMeasureId
);
