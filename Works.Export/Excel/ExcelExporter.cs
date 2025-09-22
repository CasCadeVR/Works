using CasCadeVR.Works.Export.Contracts;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Services.Contracts.Models.Export;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CasCadeVR.Works.Export.Excel;

/// <summary>
/// Экспорт для Excel
/// </summary>
public class ExcelExporter : IExporter
{
    private readonly string excelFileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    ExportedData IExporter.Export(ActModel act)
    {
        ExcelCollector.CalculateFinalTotalPrices(act);

        using var memoryStream = new MemoryStream();
        
        using (var spreadsheet = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook, autoSave: false))
        {
            var workbookPart = spreadsheet.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            var stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
            stylesPart.Stylesheet = ExcelConstructor.CreateStylesheet();
            stylesPart.Stylesheet.Save();

            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

            var worksheet = new Worksheet();
            var columns = ExcelConstructor.CreateDefaultColumns();
            var sheetData = new SheetData();

            ExcelCollector.AddHeaderRow(sheetData, act);
            ExcelCollector.AddGeneralInfoRows(sheetData, act);
            ExcelCollector.AddWorksTable(sheetData, act);
            ExcelCollector.AddSummaryRow(sheetData, act);
            ExcelCollector.AddFinalRows(sheetData, act);
            ExcelCollector.AddSignatures(sheetData);

            worksheet.Append(columns);
            worksheet.Append(sheetData);

            worksheetPart.Worksheet = worksheet;
            worksheetPart.Worksheet.Save();

            var sheets = workbookPart.Workbook.AppendChild(new Sheets());
            sheets.Append(new Sheet()
            {
                Id = workbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Акт",
            });

            workbookPart.Workbook.Save();
        }

        var result = new ExportedData
        {
            ExportedMemoryStream = memoryStream,
            FileType = excelFileType,
            FileName = $"Act_{act.ActNumber}_{act.Date:yyyy-MM-dd}.xlsx"
        };

        return result;
    }
}