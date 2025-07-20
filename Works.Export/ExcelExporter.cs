using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Works.Export.Contracts;
using Works.Web.Contracts.Models.Acts;

/// <summary>
/// Экспорт для Excel
/// </summary>
public class ExcelExporter : IExporter
{
    byte[] IExporter.Export(ActApiModel act)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var document = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());

                // Лист 1: Общая информация
                AddGeneralInfoSheet(workbookPart, sheets, act);

                // Лист 2: Таблица работ
                AddWorksSheet(workbookPart, sheets, act);

                // Лист 3: Итоговая сумма
                AddSummarySheet(workbookPart, sheets, act);

                workbookPart.Workbook.Save();
            }

            return memoryStream.ToArray();
        }
    }

    private void AddGeneralInfoSheet(WorkbookPart workbookPart, Sheets sheets, ActApiModel act)
    {
        var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        var sheetData = new SheetData();

        // Заголовок
        sheetData.AppendChild(CreateRow("Информация по акту"));

        sheetData.AppendChild(CreateRow("Поле", "Значение"));

        sheetData.AppendChild(CreateRow("Номер акта", act.ActNumber));
        sheetData.AppendChild(CreateRow("Дата", act.Date.ToString("yyyy-MM-dd HH:mm")));

        sheetData.AppendChild(CreateRow("Исполнитель"));
        sheetData.AppendChild(CreateRow("ФИО", act.ExecutorFIO));
        sheetData.AppendChild(CreateRow("Должность", act.ExecutorOccupation));
        sheetData.AppendChild(CreateRow("Фирма", act.ExecutorFirm));
        sheetData.AppendChild(CreateRow("ОГРН", act.ExecutorOGRN));

        sheetData.AppendChild(CreateRow("Заказчик"));
        sheetData.AppendChild(CreateRow("ФИО", act.CustomerFIO));
        sheetData.AppendChild(CreateRow("Должность", act.CustomerOccupation));
        sheetData.AppendChild(CreateRow("Фирма", act.CustomerFirm));
        sheetData.AppendChild(CreateRow("ИНН", act.CustomerINN));

        var worksheet = new Worksheet(sheetData);
        worksheetPart.Worksheet = worksheet;
        worksheet.Save();

        var sheet = new Sheet()
        {
            Id = workbookPart.GetIdOfPart(worksheetPart),
            SheetId = 1,
            Name = "Общая информация"
        };

        sheets.Append(sheet);
    }

    private void AddWorksSheet(WorkbookPart workbookPart, Sheets sheets, ActApiModel act)
    {
        var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        var sheetData = new SheetData();

        // Заголовок таблицы
        sheetData.AppendChild(CreateRow("Наименование", "Кол-во", "Цена", "Сумма", "Ед. изм."));

        foreach (var work in act.Works)
        {
            sheetData.AppendChild(CreateRow(
                work.WorkName,
                work.Quantity.ToString(),
                work.ActualPrice.ToString(),
                work.TotalPrice.ToString(),
                work.WorkUnitOfMeasure
            ));
        }

        var worksheet = new Worksheet(sheetData);
        worksheetPart.Worksheet = worksheet;
        worksheet.Save();

        var sheet = new Sheet()
        {
            Id = workbookPart.GetIdOfPart(worksheetPart),
            SheetId = 2,
            Name = "Работы"
        };

        sheets.Append(sheet);
    }

    private void AddSummarySheet(WorkbookPart workbookPart, Sheets sheets, ActApiModel act)
    {
        var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        var sheetData = new SheetData();

        sheetData.AppendChild(CreateRow("Итоговая сумма"));
        sheetData.AppendChild(CreateRow("Полная сумма", act.TotalPrice.ToString()));
        sheetData.AppendChild(CreateRow("НДС (%)", act.NDS.ToString()));
        sheetData.AppendChild(CreateRow("Сумма с НДС", act.TotalPriceNDS.ToString()));

        var worksheet = new Worksheet(sheetData);
        worksheetPart.Worksheet = worksheet;
        worksheet.Save();

        var sheet = new Sheet()
        {
            Id = workbookPart.GetIdOfPart(worksheetPart),
            SheetId = 3,
            Name = "Итоги"
        };

        sheets.Append(sheet);
    }

    private Row CreateRow(params string[] values)
    {
        return new Row(values.Select(v => new Cell(new InlineString(new Text(v))) { DataType = CellValues.InlineString }).ToArray());
    }
}