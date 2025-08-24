using CasCadeVR.Works.Web.Contracts.Models.Acts;

namespace CasCadeVR.Works.Export.Contracts;

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
