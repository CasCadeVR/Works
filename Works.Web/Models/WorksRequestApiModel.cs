namespace Works.Web.Models;

/// <summary>
/// Модель создания работы
/// </summary>
/// <param name="Name">Именования</param>
/// <param name="Description">Описание</param>
/// <param name="Price">Цена</param>
public record WorksRequestApiModel(string Name, string Description, decimal Price);
