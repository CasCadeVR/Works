namespace Works.Web.Contracts.Models.Works;

/// <summary>
/// Модель создания работы
/// </summary>
/// <param name="Name">Именования</param>
/// <param name="Description">Описание</param>
/// <param name="Price">Цена</param>
/// <param name="UnitOfMeasure">Единица измерения</param>
public record WorksRequestApiModel(
    string Name, 
    string Description, 
    decimal Price, 
    string UnitOfMeasure
);
