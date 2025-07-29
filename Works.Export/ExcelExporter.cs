using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Works.Export;
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
            using (var spreadsheet = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook, autoSave: false))
            {
                var workbookPart = spreadsheet.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = CreateStylesheet();
                stylesPart.Stylesheet.Save();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                var worksheet = new Worksheet();
                var columns = CreateDefaultColumns();
                var sheetData = new SheetData();

                AddHeaderRow(sheetData, act);
                AddGeneralInfoRows(sheetData, act);
                AddWorksTable(sheetData, act);
                AddSummaryRow(sheetData, act);
                AddFinalRow(sheetData, act);
                AddSignatures(sheetData);

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

            return memoryStream.ToArray();
        }
    }

    private Columns CreateDefaultColumns()
    {
        return new Columns(
            new Column
            {
                Min = 1,
                Max = 1,
                Width = 8,
                CustomWidth = true
            },
            new Column
            {
                Min = 2,
                Max = 2,
                Width = 24,
                CustomWidth = true
            },
            new Column
            {
                Min = 3,
                Max = 3,
                Width = 12,
                CustomWidth = true
            },
            new Column
            {
                Min = 4,
                Max = 6,
                Width = 15,
                CustomWidth = true
            }
        );
    }

    private Stylesheet CreateStylesheet()
        => new(
            new NumberingFormats(
                new NumberingFormat { NumberFormatId = 164, FormatCode = "#,##0.00" }
            ),
            new Fonts(
                new Font(
                    new FontSize() { Val = 14 },
                    new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                    new FontName() { Val = "TimesNewRoman" }
                ),
                new Font(
                    new Bold(),
                    new FontSize() { Val = 14 },
                    new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                    new FontName() { Val = "TimesNewRoman" }
                ),
                new Font(
                    new Bold(),
                    new FontSize() { Val = 16 },
                    new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
                    new FontName() { Val = "TimesNewRoman" }
                )
            ),
            new Fills(
                new Fill(new PatternFill() { PatternType = PatternValues.None })
            ),
            new Borders(
                new Border(
                    new LeftBorder(),
                    new RightBorder(),
                    new TopBorder(),
                    new BottomBorder(),
                    new DiagonalBorder()
                ),
                new Border(
                    new LeftBorder() { Style = BorderStyleValues.Medium },
                    new RightBorder() { Style = BorderStyleValues.Medium },
                    new TopBorder() { Style = BorderStyleValues.Medium },
                    new BottomBorder() { Style = BorderStyleValues.Medium }
                )
            ),
            new CellFormats(
                // Index 0
                new CellFormat()
                {
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    Alignment = new Alignment() { Horizontal = HorizontalAlignmentValues.Center },
                    ApplyAlignment = true
                },

                // Index 1
                new CellFormat()
                {
                    FontId = 1,
                    FillId = 0,
                    BorderId = 0,
                    Alignment = new Alignment() { Horizontal = HorizontalAlignmentValues.Left },
                    ApplyFont = true,
                    ApplyAlignment = true
                },

                // Index 2
                new CellFormat()
                {
                    FontId = 0,
                    FillId = 0,
                    BorderId = 1,
                    NumberFormatId = 164,
                    Alignment = new Alignment() { Horizontal = HorizontalAlignmentValues.Center },
                    ApplyFont = true,
                    ApplyBorder = true,
                    ApplyNumberFormat = true,
                    ApplyAlignment = true
                },

                // Index 3
                new CellFormat()
                {
                    FontId = 2,
                    FillId = 0,
                    BorderId = 0,
                    NumberFormatId = 164,
                    Alignment = new Alignment() { Horizontal = HorizontalAlignmentValues.Left },
                    ApplyFont = true,
                    ApplyNumberFormat = true,
                    ApplyAlignment = true
                }
            )
        );

    private IEnumerable<Cell> CreateIndentCells(int count = 2, ExcelStyleIndexes style = ExcelStyleIndexes.Normal)
    {
        for (int i = 0; i < count; i++)
        {
            yield return CreateCell("", style);
        }
    }

    private Row CreateIndentedRow(IEnumerable<Cell> cells, int count = 2, ExcelStyleIndexes style = ExcelStyleIndexes.Normal)
    {
        var row = new Row();
        row.Append(CreateIndentCells(count, style));
        row.Append(cells);
        return row;
    }

    private void AddHeaderRow(SheetData sheetData, ActApiModel act)
    {
        sheetData.Append(CreateIndentedRow(
           [
                CreateCell("АКТ №", ExcelStyleIndexes.Header),
                CreateCell(act.ActNumber.ToString(), ExcelStyleIndexes.Header),
           ])
        );

        sheetData.Append(CreateIndentedRow(
          [
               CreateCell("от ", ExcelStyleIndexes.Header),
             CreateCell($"{act.Date:dd.MM.yyyy}", ExcelStyleIndexes.Header),
          ])
        );

        sheetData.Append(CreateIndentedRow([CreateCell("сдачи-приёмки выполненных работ", ExcelStyleIndexes.Header)]) );
        sheetData.Append(CreateIndentedRow([CreateCell("(оказания услуг)", ExcelStyleIndexes.Header)]));
    }

    private Row CreateRow(string[] values, ExcelStyleIndexes style = ExcelStyleIndexes.Normal)
    {
        return new Row(values.Select(v => CreateCell(v, style)).ToArray());
    }

    private void AddGeneralInfoRows(SheetData sheetData, ActApiModel act)
    {
        sheetData.Append(new Row());

        sheetData.Append(CreateRow(["Мы, нижеподписавшиеся:"]));

        sheetData.Append(CreateRow([$"представитель Исполнителя: должность {act.ExecutorOccupation} фирма {act.ExecutorFirm} ОГРН {act.ExecutorOGRN}",
        ]));
        sheetData.Append(CreateRow([$"в лице {act.ExecutorFIO}" ]));

        sheetData.Append(CreateRow([$"представитель Заказчика: должность {act.CustomerOccupation} фирма {act.CustomerFirm} ИНН {act.CustomerINN}",
        ]));
        sheetData.Append(CreateRow([$"в лице {act.CustomerFIO}"]));

        sheetData.Append(new Row());

        sheetData.Append(CreateRow(["составили настоящий акт о том, что Исполнителем были выполнены следующие работы"]));
        sheetData.Append(CreateRow([$"(оказаны следующие услуги) по договору №{act.ActNumber} от {act.Date.Date.ToShortTimeString()} {act.Date.Year.ToString()} г."]));
    }

    private void AddWorksTable(SheetData sheetData, ActApiModel act)
    {
        sheetData.Append(new Row());

        sheetData.Append(CreateRow(["№", "Наименование работы (услуги)", "Ед. изм.", "Количество", "Цена", "Сумма"], ExcelStyleIndexes.NormalBorder));

        int rowIndex = 1;
        foreach (var work in act.Works)
        {
            sheetData.Append(CreateRow([
                rowIndex++.ToString(), 
                work.WorkName,
                work.WorkUnitOfMeasure,
                work.Quantity.ToString(),
                work.ActualPrice.ToString("N2"),
                work.TotalPrice.ToString("N2")
                ], ExcelStyleIndexes.NormalBorder));
        }
    }

    private void AddSummaryRow(SheetData sheetData, ActApiModel act)
    {
        sheetData.Append(CreateIndentedRow(
            [
                CreateCell("Итого:", ExcelStyleIndexes.NormalBold),
                CreateCell(act.TotalPrice.ToString("N2"), ExcelStyleIndexes.NormalBorder),
            ], 4)
        );

        sheetData.Append(CreateIndentedRow(
            [
                CreateCell($"В тол. числе НДС ({act.NDS}%)", ExcelStyleIndexes.NormalBold),
                CreateCell(act.PriceNDS.ToString("N2"), ExcelStyleIndexes.NormalBorder),
            ], 4)
        );

        sheetData.Append(CreateIndentedRow(
            [
                CreateCell("Всего (с учетом НДС)", ExcelStyleIndexes.NormalBold),
                CreateCell(act.TotalPriceNDS.ToString("N2"), ExcelStyleIndexes.NormalBorder),
            ], 4)
        );
    }

    private void AddFinalRow(SheetData sheetData, ActApiModel act)
    {
        sheetData.Append(CreateRow([$"Всего выполнено работ (услуг) на сумму: {act.TotalPrice.ToString("N2")} рублей."]));
        sheetData.Append(CreateRow([$"в т.ч. включая НДС: {act.TotalPriceNDS.ToString("N2")} рублей."]));

        sheetData.Append(CreateRow(["Вышеперечисленные работы (услуги) выполнены полностью и в срок. Заказчик"], ExcelStyleIndexes.NormalBold));
        sheetData.Append(CreateRow(["претензий по обьёму, качеству и срокам оказания услуг не имеет."], ExcelStyleIndexes.NormalBold));
    }

    private void AddSignatures(SheetData sheetData)
    {
        sheetData.Append(new Row());

        var CustomerExecutorRow = new Row();
        CustomerExecutorRow.Append(CreateIndentCells(2));
        CustomerExecutorRow.Append(CreateCell("ИСПОЛНИТЕЛЬ", ExcelStyleIndexes.Header));
        CustomerExecutorRow.Append(CreateCell("ЗАКАЗЧИК", ExcelStyleIndexes.Header));
        sheetData.Append(CustomerExecutorRow);

        sheetData.Append(new Row());

        var MPROW = new Row();
        MPROW.Append(CreateIndentCells(2));
        MPROW.Append(CreateCell("М.П.", ExcelStyleIndexes.Header));
        MPROW.Append(CreateCell("М.П.", ExcelStyleIndexes.Header));
        sheetData.Append(MPROW);
    }

    private Cell CreateCell(string value, ExcelStyleIndexes style = ExcelStyleIndexes.Normal)
    {
        return new Cell(new InlineString(new Text(value)))
        {
            DataType = CellValues.InlineString,
            StyleIndex = (uint)style
        };
    }
}