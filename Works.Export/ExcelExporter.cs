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
            using (var spreadsheet = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook, autoSave: false))
            {
                var workbookPart = spreadsheet.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = CreateStylesheet();
                stylesPart.Stylesheet.Save();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();

                // Заполняем данными
                AddHeaderRow(sheetData, act);
                AddGeneralInfoRows(sheetData, act);
                AddWorksTable(sheetData, act);
                AddSummaryRow(sheetData, act);
                AddSignatures(sheetData);

                worksheetPart.Worksheet = new Worksheet(sheetData);
                worksheetPart.Worksheet.Save();

                // 4. Добавляем лист в книгу
                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                sheets.Append(new Sheet()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Акт"
                });

                workbookPart.Workbook.Save();
            }

            return memoryStream.ToArray();
        }
    }

    private Stylesheet CreateStylesheet()
        => new(
            new NumberingFormats(
                new NumberingFormat { NumberFormatId = 164, FormatCode = "#,##0.00" }
            ),
            new Fonts(
                new Font(),
                new Font(new Bold(), new FontSize { Val = 14 })
            ),
            new Fills(
                new Fill(new PatternFill { PatternType = PatternValues.None }),
                new Fill(new PatternFill { PatternType = PatternValues.Gray125 })
            ),
            new Borders(
                new Border(),
                new Border(
                    new LeftBorder { Style = BorderStyleValues.Thin },
                    new RightBorder { Style = BorderStyleValues.Thin },
                    new TopBorder { Style = BorderStyleValues.Thin },
                    new BottomBorder { Style = BorderStyleValues.Thin }
                )
            ),
            new CellFormats(
                new CellFormat(), // Default (0)
                new CellFormat { FontId = 1, Alignment = new Alignment { Horizontal = HorizontalAlignmentValues.Center } }, // Header (1)
                new CellFormat { FontId = 1, Alignment = new Alignment { Horizontal = HorizontalAlignmentValues.Right }, NumberFormatId = 164 } // Summary (2)
            )
        );

    private IEnumerable<Cell> CreateIndentCells(int count = 2, uint styleIndex = 3)
    {
        for (int i = 0; i < count; i++)
        {
            yield return CreateCell("", styleIndex);
        }
    }

    private Row CreateIndentedRow(IEnumerable<Cell> cells, int indentCount = 4, uint indentStyle = 3)
    {
        var row = new Row();
        row.Append(CreateIndentCells(indentCount, indentStyle));
        row.Append(cells);
        return row;
    }

    /// <summary>
    /// Добавление заголовка акта
    /// </summary>
    private void AddHeaderRow(SheetData sheetData, ActApiModel act)
    {
        sheetData.Append(new Row());
        var actNumber = new[]
    {
        CreateCell("АКТ №", 1),
        CreateCell(act.ActNumber.ToString(), 1),
    };

        var actDate = new[]
    {
        CreateCell($"от {act.Date:dd.MM.yyyy}", 0)
    };

        var actName1 = new[]
    {
        CreateCell("сдачи-приёмки выполненных работ", 1),
    };

        var actName2 = new[]
   {
        CreateCell("(оказания услуг)", 1),
    };

        sheetData.Append(CreateIndentedRow(actNumber));
        sheetData.Append(CreateIndentedRow(actDate));
        sheetData.Append(CreateIndentedRow(actName1));
        sheetData.Append(CreateIndentedRow(actName2));
    }

    private Row CreateRow(string[] values, uint styleIndex = 0)
    {
        return new Row(values.Select(v => CreateCell(v, styleIndex)).ToArray());
    }

    /// <summary>
    /// Добавление общей информации
    /// </summary>
    private void AddGeneralInfoRows(SheetData sheetData, ActApiModel act)
    {
        sheetData.Append(new Row());

        sheetData.Append(CreateRow(["Мы, нижеподписавшиеся:"], 1));

        sheetData.Append(CreateRow([ "представитель Исполнителя, нижеподписавшиеся:",
            $"должность {act.ExecutorOccupation}",
            $"фирма {act.ExecutorFirm}",
            $"ОГРН {act.ExecutorOGRN}",
        ], 0));
        sheetData.Append(CreateRow([ $"в лице {act.ExecutorFIO}:" ], 0));

        sheetData.Append(CreateRow([ "представитель Исполнителя, нижеподписавшиеся:",
            $"должность {act.CustomerOccupation}",
            $"фирма {act.CustomerFirm}",
            $"ИНН {act.CustomerINN}",
        ], 0));
        sheetData.Append(CreateRow([$"в лице {act.CustomerFIO}:"], 0));

        sheetData.Append(CreateRow(["составили настоящий акт о том, что Исполнителем были выполнены следующие работы"], 1));
        sheetData.Append(CreateRow([$"(оказаны следующие услуги) по договору №{act.ActNumber} от {act.Date.Date.ToShortTimeString()} {act.Date.Year.ToString()} г."], 1));
    }

    /// <summary>
    /// Добавление таблицы работ
    /// </summary>
    private void AddWorksTable(SheetData sheetData, ActApiModel act)
    {
        sheetData.Append(new Row());

        var headerRow = new Row();
        headerRow.Append(CreateCell("#", 1));
        headerRow.Append(CreateCell("Наименование работы (услуги)", 1));
        headerRow.Append(CreateCell("Ед. изм.", 1));
        headerRow.Append(CreateCell("Количество", 1));
        headerRow.Append(CreateCell("Цена", 1));
        headerRow.Append(CreateCell("Сумма", 1));
        sheetData.Append(headerRow);

        // Тела таблицы
        uint rowIndex = 1;
        foreach (var work in act.Works)
        {
            var row = new Row();
            row.Append(CreateCell(rowIndex++.ToString(), 0));
            row.Append(CreateCell(work.WorkName, 0));
            row.Append(CreateCell(work.WorkUnitOfMeasure, 0));
            row.Append(CreateCell(work.Quantity.ToString(), 0));
            row.Append(CreateCell(work.ActualPrice.ToString("N2"), 0));
            row.Append(CreateCell(work.TotalPrice.ToString("N2"), 0));
            sheetData.Append(row);
        }
    }

    /// <summary>
    /// Добавление итоговой строки
    /// </summary>
    private void AddSummaryRow(SheetData sheetData, ActApiModel act)
    {
        sheetData.Append(new Row());

        var summaryRow = new Row();
        summaryRow.Append(CreateCell("", 1));
        summaryRow.Append(CreateCell("Итого:", 1));
        summaryRow.Append(CreateCell("", 1));
        summaryRow.Append(CreateCell("", 1));
        summaryRow.Append(CreateCell("", 1));
        summaryRow.Append(CreateCell(act.TotalPrice.ToString("N2"), 1));
        sheetData.Append(summaryRow);

        var vatRow = new Row();
        vatRow.Append(CreateCell("", 1));
        vatRow.Append(CreateCell($"В тол. числе НДС ({act.NDS}%)", 1));
        vatRow.Append(CreateCell("", 1));
        vatRow.Append(CreateCell("", 1));
        vatRow.Append(CreateCell("", 1));
        vatRow.Append(CreateCell(act.TotalPriceNDS.ToString("N2"), 1));
        sheetData.Append(vatRow);

        var totalRow = new Row();
        totalRow.Append(CreateCell("", 1));
        totalRow.Append(CreateCell("Всего (с учетом НДС)", 1));
        totalRow.Append(CreateCell("", 1));
        totalRow.Append(CreateCell("", 1));
        totalRow.Append(CreateCell("", 1));
        totalRow.Append(CreateCell(act.TotalPriceNDS.ToString("N2"), 1));
        sheetData.Append(totalRow);
    }

    /// <summary>
    /// Добавление подписей исполнителя и заказчика
    /// </summary>
    private void AddSignatures(SheetData sheetData)
    {
        sheetData.Append(new Row());

        // Пустая строка для отступа перед подписями
        sheetData.Append(new Row(CreateIndentCells().ToArray()));

        // Строка с подписями
        var signatureCells = new[]
        {
        CreateCell("ИСПОЛНИТЕЛЬ", 1),
        CreateCell("ЗАКАЗЧИК", 1)
    };
        sheetData.Append(CreateIndentedRow(signatureCells));

        // Строка с М.П.
        var mpCells = new[]
        {
        CreateCell("М.П.", 1),
        CreateCell("М.П.", 1)
    };
        sheetData.Append(CreateIndentedRow(mpCells));
    }

    /// <summary>
    /// Создание ячейки с текстом
    /// </summary>
    private Cell CreateCell(string value, uint styleIndex)
    {
        return new Cell(new InlineString(new Text(value)))
        {
            DataType = CellValues.InlineString,
            StyleIndex = styleIndex
        };
    }

    /// <summary>
    /// Получение индекса стиля по имени
    /// </summary>
    private uint GetStyleIndex(string styleName)
    {
        switch (styleName)
        {
            case "Header":
                return 0; // Индекс стиля заголовка
            case "Table":
                return 1; // Индекс стиля таблицы
            case "Summary":
                return 2; // Индекс стиля итоговой строки
            default:
                return 0; // По умолчанию стиль Normal
        }
    }
}