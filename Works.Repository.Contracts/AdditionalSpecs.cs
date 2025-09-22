using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.Models;

namespace CasCadeVR.Works.Repository.Contracts;

/// <summary>
/// Дополнительные спецификации чтения
/// </summary>
public static class AdditionalSpecs
{
    /// <summary>
    /// Получить модель <see cref="ActDbModel"/> из сущности Act
    /// </summary>
    public static IQueryable<ActDbModel> SelectActDbModel(this IQueryable<Act> query)
        => query.Select(x => new ActDbModel
        {
            Id = x.Id,
            ActNumber = x.ActNumber,
            Date = x.Date,
            Executor = x.Executor,
            Customer = x.Customer,
            ActWorks = x.ActWorks
                .Where(y => y.DeletedAt == null)
                .Select(y => new ActWorkDbModel
                {
                    Id = y.Id,
                    ActId = x.Id,
                    Quantity = y.Quantity,
                    CapturedPrice = y.CapturedPrice,
                    Work = new WorkDbModel
                    {
                        Id = y.Work.Id,
                        Name = y.Work.Name,
                        Description = y.Work.Description,
                        Price = y.Work.Price,
                        UnitOfMeasure = y.Work.UnitOfMeasure,
                    },
                }).ToList(),
        });
}