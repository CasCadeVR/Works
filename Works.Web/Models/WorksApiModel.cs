namespace Works.Web.Models;

/// <summary>
/// Модель работы
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Именования</param>
/// <param name="Description">Описание</param>
/// <param name="Price">Цена</param>
public record WorksApiModel(Guid Id, string Name, string Description, decimal Price);
