namespace CasCadeVR.Works.Services.Contracts.Models.Export;

/// <summary>
/// Модель данных для экспорта
/// </summary>
public class ExportedData
{
    /// <summary>
    /// Экспортированные данные
    /// </summary>
    public MemoryStream ExportedMemoryStream = null!;

    /// <summary>
    /// Тип файла
    /// </summary>
    public string FileType = string.Empty;

    /// <summary>
    /// Имя файла
    /// </summary>
    public string FileName = string.Empty;
}
