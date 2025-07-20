namespace Works.Web.Contracts.Models.Works;

/// <summary>
/// Модель работы
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Именования</param>
/// <param name="Description">Описание</param>
/// <param name="Price">Цена</param>
/// <param name="UnitOfMeasure">Единица измерения</param>
public record WorkApiModel(
    Guid Id, 
    string Name, 
    string Description, 
    decimal Price, 
    string UnitOfMeasure
);
