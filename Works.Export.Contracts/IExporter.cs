using Works.Web.Contracts.Models.Acts;

namespace Works.Export.Contracts;

/// <summary>
/// Интерфейс экспорта
/// </summary>
public interface IExporter
{
    /// <summary>
    /// Экспортировать
    /// </summary>
    public byte[] Export(ActApiModel act);
}
