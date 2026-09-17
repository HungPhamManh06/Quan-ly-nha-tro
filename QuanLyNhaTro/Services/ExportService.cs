using System.Text;
using ClosedXML.Excel;
using QuanLyNhaTro.Services;

namespace QuanLyNhaTro.Services;

public interface IExportService
{
    byte[] ExportExcel(string sheetName, List<string> headers, List<List<object>> rows);
    byte[] ExportCsv(string sheetName, List<string> headers, List<List<object>> rows);
}

public class ExportService : IExportService
{
    public byte[] ExportExcel(string sheetName, List<string> headers, List<List<object>> rows)
    {
        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add(string.IsNullOrWhiteSpace(sheetName) ? "Report" : sheetName);

        for (var i = 0; i < headers.Count; i++)
        {
            var cell = ws.Cell(1, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#0d6efd");
            cell.Style.Font.FontColor = XLColor.White;
        }

        for (var r = 0; r < rows.Count; r++)
        {
            for (var c = 0; c < rows[r].Count; c++)
            {
                var value = rows[r][c];
                if (value is decimal dec) ws.Cell(r + 2, c + 1).Value = dec;
                else if (value is int intVal) ws.Cell(r + 2, c + 1).Value = intVal;
                else if (value is DateTime dt) ws.Cell(r + 2, c + 1).Value = dt.ToString("dd/MM/yyyy");
                else ws.Cell(r + 2, c + 1).Value = value?.ToString() ?? "";
            }
        }

        ws.Columns().AdjustToContents();
        ws.Row(1).Height = 22;
        ws.SheetView.FreezeRows(1);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public byte[] ExportCsv(string sheetName, List<string> headers, List<List<object>> rows)
    {
        var sb = new StringBuilder();
        // BOM để Excel mở tiếng Việt đúng UTF-8
        sb.Append('\uFEFF');
        sb.AppendLine(string.Join(";", headers));
        foreach (var row in rows)
            sb.AppendLine(string.Join(";", row.Select(v =>
            {
                var s = v?.ToString() ?? "";
                return s.Contains(';') || s.Contains('"') || s.Contains('\n') ? $"\"{s.Replace("\"", "\"\"")}\"" : s;
            })));
        return Encoding.UTF8.GetBytes(sb.ToString());
    }
}
