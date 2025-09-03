using CasCadeVR.Works.Web.Models.Works;

namespace CasCadeVR.Works.Web.Models.ActWorks;

/// <summary>
/// Модель работы для акта
/// </summary>
/// <param name="Quantity">Количество</param>
/// <param name="Work">Работа</param>
public record ActWorksApiModel(
    int Quantity, 
    WorkApiModel Work
);