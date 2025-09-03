using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CasCadeVR.Works.Export.Excel;

/// <summary>
/// Класс для вспомогательных методов для Excel
/// </summary>
public static class ExcelConstructor
{
    /// <summary>
    /// Создать колонки
    /// </summary>
    public static Columns CreateDefaultColumns()
        => new(
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
            });
    
    /// <summary>
    /// Создать таблицу стилей
    /// </summary>
    public static Stylesheet CreateStylesheet()
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
                new CellFormat()
                {
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    Alignment = new Alignment() { Horizontal = HorizontalAlignmentValues.Center },
                    ApplyAlignment = true
                },
                new CellFormat()
                {
                    FontId = 1,
                    FillId = 0,
                    BorderId = 0,
                    Alignment = new Alignment() { Horizontal = HorizontalAlignmentValues.Left },
                    ApplyFont = true,
                    ApplyAlignment = true
                },
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

    /// <summary>
    /// Создать несколько ячеек со стилями с отступом
    /// </summary>
    public static IEnumerable<Cell> CreateIndentCells(int count = 2, ExcelStyleIndexes style = ExcelStyleIndexes.Normal)
    {
        for (var i = 0; i < count; i++)
        {
            yield return CreateCell("", style);
        }
    }

    /// <summary>
    /// Создать строку ячеек со стилями с отступом
    /// </summary>
    public static Row CreateIndentedRow(IEnumerable<Cell> cells, int count = 2, ExcelStyleIndexes style = ExcelStyleIndexes.Normal)
    {
        var row = new Row();
        row.Append(CreateIndentCells(count, style));
        row.Append(cells);
        return row;
    }

    /// <summary>
    /// Создать строку из ячеек со стилями
    /// </summary>
    public static Row CreateRow(string[] values, ExcelStyleIndexes style = ExcelStyleIndexes.Normal)
    {
        return new Row(values.Select(v => CreateCell(v, style)).ToArray<OpenXmlElement>());
    }

    /// <summary>
    /// Создать ячейку
    /// </summary>
    public static Cell CreateCell(string value, ExcelStyleIndexes style = ExcelStyleIndexes.Normal)
    {
        return new Cell(new InlineString(new Text(value)))
        {
            DataType = CellValues.InlineString,
            StyleIndex = (uint)style
        };
    }
}