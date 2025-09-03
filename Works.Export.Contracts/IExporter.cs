using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Services.Contracts.Models.Export;

namespace CasCadeVR.Works.Export.Contracts;

/// <summary>
/// Интерфейс экспорта
/// </summary>
public interface IExporter
{
    /// <summary>
    /// Экспортировать
    /// </summary>
    public ExportedData Export(ActModel act);
}
