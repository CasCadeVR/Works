using CasCadeVR.Works.Common;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CasCadeVR.Works.Export.Excel;

/// <summary>
/// Класс для сборки акта для Excel
/// </summary>
public static class ExcelCollector
{
    private static decimal actTotalPrice;
    private static decimal actPriceAddedTax;
    private static decimal actTotalPriceWithAddedTax;

    /// <summary>
    /// Вычислить итоговые данные
    /// </summary>
    public static void CalculateFinalTotalPrices(ActModel act)
    {
        actTotalPrice = act.ActWorks.Sum(x => x.Quantity * x.CapturedPrice);
        actPriceAddedTax = actTotalPrice * TaxConstants.TaxRate / 100;
        actTotalPriceWithAddedTax = actTotalPrice + actPriceAddedTax;
    }

    /// <summary>
    /// Создать заголовок
    /// </summary>
    public static void AddHeaderRow(SheetData sheetData, ActModel act)
    {
        sheetData.Append(ExcelConstructor.CreateIndentedRow(
            [
                ExcelConstructor.CreateCell("АКТ №", ExcelStyleIndexes.Header),
                ExcelConstructor.CreateCell(act.ActNumber.ToString(), ExcelStyleIndexes.Header),
            ])
        );

        sheetData.Append(ExcelConstructor.CreateIndentedRow(
            [
                ExcelConstructor.CreateCell("от ", ExcelStyleIndexes.Header),
                ExcelConstructor.CreateCell($"{act.Date:dd.MM.yyyy}", ExcelStyleIndexes.Header),
            ])
        );

        sheetData.Append(ExcelConstructor.CreateIndentedRow([ExcelConstructor.CreateCell("сдачи-приёмки выполненных работ", ExcelStyleIndexes.Header)]));
        sheetData.Append(ExcelConstructor.CreateIndentedRow([ExcelConstructor.CreateCell("(оказания услуг)", ExcelStyleIndexes.Header)]));
    }

    /// <summary>
    /// Создать строки о заказчике и исполнителе
    /// </summary>
    public static void AddGeneralInfoRows(SheetData sheetData, ActModel act)
    {
        sheetData.Append(new Row());

        sheetData.Append(ExcelConstructor.CreateRow(["Мы, нижеподписавшиеся:"]));
        sheetData.Append(ExcelConstructor.CreateRow([$"представитель Исполнителя: должность {act.Executor.Occupation} фирма {act.Executor.Firm} ОГРН {act.Executor.RegistrationNumber}",]));
        sheetData.Append(ExcelConstructor.CreateRow([$"в лице {act.Executor.FullName}"]));
        sheetData.Append(ExcelConstructor.CreateRow([$"представитель Заказчика: должность {act.Customer.Occupation} фирма {act.Customer.Firm} ИНН {act.Customer.TaxPayerId}",]));
        sheetData.Append(ExcelConstructor.CreateRow([$"в лице {act.Customer.FullName}"]));
        sheetData.Append(new Row());

        sheetData.Append(ExcelConstructor.CreateRow(["составили настоящий акт о том, что Исполнителем были выполнены следующие работы"]));
        sheetData.Append(ExcelConstructor.CreateRow([$"(оказаны следующие услуги) по договору №{act.ActNumber} от {act.Date.ToShortDateString()} {act.Date.Year} г."]));
    }

    /// <summary>
    /// Создать таблицу работ
    /// </summary>
    public static void AddWorksTable(SheetData sheetData, ActModel act)
    {
        sheetData.Append(new Row());

        sheetData.Append(ExcelConstructor.CreateRow(["№", "Наименование работы (услуги)", "Ед. изм.", "Количество", "Цена", "Сумма"], ExcelStyleIndexes.NormalBorder));

        var rowIndex = 1;
        foreach (var work in act.ActWorks)
        {
            sheetData.Append(ExcelConstructor.CreateRow([
                rowIndex++.ToString(),
                work.Work.Name,
                work.Work.UnitOfMeasure.Name,
                work.Quantity.ToString(),
                work.Work.Price.ToString("N2"),
                (work.Quantity * work.Work.Price).ToString("N2")
                ], ExcelStyleIndexes.NormalBorder));
        }
    }
    
    /// <summary>
    /// Создать строку итоговых данных
    /// </summary>
    public static void AddSummaryRow(SheetData sheetData, ActModel act)
    {
        sheetData.Append(ExcelConstructor.CreateIndentedRow(
            [
                ExcelConstructor.CreateCell("Итого:", ExcelStyleIndexes.NormalBold),
                ExcelConstructor.CreateCell(actTotalPrice.ToString("N2"), ExcelStyleIndexes.NormalBorder),
            ], 4)
        );

        sheetData.Append(ExcelConstructor.CreateIndentedRow(
            [
                ExcelConstructor.CreateCell($"В тол. числе НДС ({TaxConstants.TaxRate}%)", ExcelStyleIndexes.NormalBold),
                ExcelConstructor.CreateCell(actPriceAddedTax.ToString("N2"), ExcelStyleIndexes.NormalBorder),
            ], 4)
        );

        sheetData.Append(ExcelConstructor.CreateIndentedRow(
            [
                ExcelConstructor.CreateCell("Всего (с учетом НДС)", ExcelStyleIndexes.NormalBold),
                ExcelConstructor.CreateCell(actTotalPriceWithAddedTax.ToString("N2"), ExcelStyleIndexes.NormalBorder),
            ], 4)
        );
    }

    /// <summary>
    /// Создать строки итогов
    /// </summary>
    public static void AddFinalRows(SheetData sheetData, ActModel act)
    {
        sheetData.Append(ExcelConstructor.CreateRow([$"Всего выполнено работ (услуг) на сумму: {actTotalPrice:N2} рублей."]));
        sheetData.Append(ExcelConstructor.CreateRow([$"в т.ч. включая НДС: {actTotalPriceWithAddedTax:N2} рублей."]));

        sheetData.Append(ExcelConstructor.CreateRow(["Вышеперечисленные работы (услуги) выполнены полностью и в срок. Заказчик"], ExcelStyleIndexes.NormalBold));
        sheetData.Append(ExcelConstructor.CreateRow(["претензий по обьёму, качеству и срокам оказания услуг не имеет."], ExcelStyleIndexes.NormalBold));
    }

    /// <summary>
    /// Создать строки места печати
    /// </summary>
    public static void AddSignatures(SheetData sheetData)
    {
        sheetData.Append(new Row());

        var customerExecutorRow = new Row();
        customerExecutorRow.Append(ExcelConstructor.CreateIndentCells(2));
        customerExecutorRow.Append(ExcelConstructor.CreateCell("ИСПОЛНИТЕЛЬ", ExcelStyleIndexes.Header));
        customerExecutorRow.Append(ExcelConstructor.CreateCell("ЗАКАЗЧИК", ExcelStyleIndexes.Header));
        sheetData.Append(customerExecutorRow);

        sheetData.Append(new Row());

        var signaturePlaceholderRow = new Row();
        signaturePlaceholderRow.Append(ExcelConstructor.CreateIndentCells(2));
        signaturePlaceholderRow.Append(ExcelConstructor.CreateCell("М.П.", ExcelStyleIndexes.Header));
        signaturePlaceholderRow.Append(ExcelConstructor.CreateCell("М.П.", ExcelStyleIndexes.Header));
        sheetData.Append(signaturePlaceholderRow);
    }
}